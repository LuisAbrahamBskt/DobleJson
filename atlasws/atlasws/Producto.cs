using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace atlasws
{
    public class Producto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}
