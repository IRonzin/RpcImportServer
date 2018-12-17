using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenDataViewerOld.Controllers
{
    public static class Utilities
    {
        public static string GetInfoFromUrl(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = "";
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            return html;
        }

        public static FullDocumentInfo ConvertToFullDocInfo(Document doc)
        {
            var fullDoc = new FullDocumentInfo()
            {
                Id = doc.Identifier,
                Title = doc.Title,
                Organization = doc.Organization,
                Topic = doc.Topic,
                Data = doc?.File.Data,
                Source = doc?.File.Source,
                Format = doc?.File.Format,
            };
            return fullDoc;
        }

        //public static void FillDatabase(string token)
        //{
        //    //убрать, заменить
        //    var html = GetInfoFromUrl(query + token);
        //    //var html = System.IO.File.ReadAllText(@"C:\Users\The Sanctuary\source\repos\repos2\OpenDataViewer\OpenDataViewer\json.json");

        //    //_documentContext.Database.ExecuteSqlCommand("TRUNCATE TABLE \"Documents\"");

        //    int counter = 0;
        //    var listOfFiles = JsonConvert.DeserializeObject<List<Json1>>(html);
        //    foreach (var json1 in listOfFiles)
        //    {
        //        if (_documentContext.Documents.Select(x => x.Id).ToList().Contains(json1.Identifier))
        //            continue;

        //        counter++;
        //        try
        //        {
        //            var json2 = JsonConvert.DeserializeObject<Json2>(GetInfoFromUrl(query + json1.Identifier + token));
        //            Thread.Sleep(1000);
        //            var json3 = JsonConvert.DeserializeObject<List<Json3>>(GetInfoFromUrl(query + json2.Identifier + @"/version/" + json2.Modified + token)).FirstOrDefault();

        //            if (json3 == null)
        //                continue;

        //            byte[] file = DownloadFile(json3.Source);

        //            var doc = new Document()
        //            {
        //                Title = json1.Title,
        //                Organization = json1.Organization_name,
        //                Topic = json1.Topic,
        //                Id = json1.Identifier,
        //                File = new File()
        //                {
        //                    Data = file,
        //                    Source = json3.Source,
        //                    Format = json3.Format,
        //                },
        //            };
        //            _documentContext.Add(doc);
        //            _documentContext.SaveChanges();
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "counter: " + counter);
        //        }
        //    }
        //}
    }
}
