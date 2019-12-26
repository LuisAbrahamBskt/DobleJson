using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace atlasws
{
    [DataContract]
    public class Data
    {
        [JsonProperty("data")]
        public DatOS data { get; set; }
        [JsonProperty("operacion")]
        public Operacion operacion { get; set; }
    }
}
