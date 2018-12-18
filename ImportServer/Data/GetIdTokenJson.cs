using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDataViewerOld.Controllers
{
    /// <summary>
    /// Json для запроса к дочерним серверам в очередь GetIdTokenQueue
    /// </summary>
    public class GetIdTokenJson
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("token_prefix")]
        public string TokenPrefix { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
