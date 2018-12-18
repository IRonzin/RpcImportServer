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
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
