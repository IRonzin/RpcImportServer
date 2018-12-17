using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenDataViewerOld.Controllers
{
    /// <summary>
    /// /api/dataset/identifier/version/
    /// </summary>
    public class Json3
    {
        [JsonProperty("created")]
        public string Created;

        [JsonProperty("source")]
        public string Source;

        [JsonProperty("source_id")]
        public string Source_id;

        [JsonProperty("provenance")]
        public string Provenance;

        [JsonProperty("format")]
        public string Format;

        [JsonProperty("updated")]
        public string Updated;
    }
}
