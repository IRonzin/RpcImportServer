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
    }
}
