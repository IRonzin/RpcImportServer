﻿using System;
using System.IO;
using System.Net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using OpenDataViewerOld.Controllers;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using File = OpenDataViewerOld.Controllers.File;

namespace ImportServer
{
    class Program
    {
        DocumentContext documentContext= new DocumentContext(new DbContextOptionsBuilder());
        public static void Main()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                DeclareQueueToMethod(channel, "getTokenQueue", s=> GetTokenMethod(s));
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        private static void DeclareQueueToMethod(IModel channel, string queueName, Func<string, string> method)
        {
            channel.QueueDeclare(queue: queueName, durable: false,
                exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName,
                autoAck: false, consumer: consumer);
            Console.WriteLine(" [x] Awaiting RPC requests");

            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body;
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                    response = method(message);
                    Console.WriteLine(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                        basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                        multiple: false);
                }
            };
        }

        /// <summary>
        /// Получить начальную информацию о всех документах.
        /// </summary>
        /// <param name="url">Строка url</param>
        /// <returns></returns>
        private static string GetTokenMethod(string url)
        {
            var html = Utilities.GetInfoFromUrl(url);
            return html;
        }

        private static string GetIdTokenMethod(DocumentContext context, string parameters)
        {
            // Проверка на наличие документа в кеше из БД
            var document = context.Documents.FirstOrDefault(x => x.Identifier == id);
            if (document != null)
            {
                document.File = context.Files.FirstOrDefault(x => x.DocumentId == document.DocumentId);
                return JsonConvert.SerializeObject(Utilities.ConvertToFullDocInfo(document),
                    new JsonSerializerSettings() { Formatting = Formatting.Indented });
            }

            var json1 = JsonConvert.DeserializeObject<List<Json1>>(
                                Utilities.GetInfoFromUrl(query + tokenPrefix + token))
                            .FirstOrDefault(x => x.Identifier == id)
                        ?? throw new Exception("No such document"); // Документа нет на сайте

            var json2 = JsonConvert.DeserializeObject<Json2>(Utilities.GetInfoFromUrl(query + id + tokenPrefix + token));

            var json3 = JsonConvert.DeserializeObject<List<Json3>>(
                                Utilities.GetInfoFromUrl(query + json2.Identifier + @"/version/" + json2.Modified + tokenPrefix + token))
                            .FirstOrDefault()
                        ?? throw new Exception("No file available"); // Для документа нет доступного файла

            string file = Utilities.GetInfoFromUrl(json3.Source);
            var doc = CacheDocument(json1, json3, file);
            return JsonConvert.SerializeObject(Utilities.ConvertToFullDocInfo(doc),
                new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }

        /// <summary>
        /// Кеширование.
        /// </summary>
        /// <param name="json1"></param>
        /// <param name="json3"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private Document CacheDocument(Json1 json1, Json3 json3, string data)
        {
            var doc = new Document()
            {
                Title = json1.Title,
                Organization = json1.Organization_name,
                Topic = json1.Topic,
                Identifier = json1.Identifier,
                File = new File()
                {
                    Data = data,
                    Source = json3.Source,
                    Format = json3.Format,
                },
            };
            documentContext.Add(doc);
            documentContext.SaveChanges();
            return doc;
        }

    }
}