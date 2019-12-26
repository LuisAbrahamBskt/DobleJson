using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace atlasws
{
    public class Operacion
    {
        [JsonProperty("mensaje")]
        public string Mensaje { get; set; }

        [JsonProperty("codigoOperacion")]
        public long CodigoOperacion { get; set; }

    }
}
