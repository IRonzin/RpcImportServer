using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDataViewerOld.Controllers
{
    /// <summary>
    /// /api/dataset(array element)
    /// </summary>
    public class Json1
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("organization")]
        public string Organization;

        [JsonProperty("organization_name")]
        public string Organization_name;

        [JsonProperty("topic")]
        public string Topic;
    }
}
