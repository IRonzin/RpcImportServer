using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDataViewerOld.Controllers
{
    /// <summary>
    /// /api/dataset/identifier
    /// </summary>
    public class Json2
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("creator")]
        public string Creator;

        [JsonProperty("created")]
        public string Created;

        [JsonProperty("modified")]
        public string Modified;

        [JsonProperty("format")]
        public string Format;

        [JsonProperty("subject")]
        public string Subject;
    }
}
