using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace atlasws
{
    public class Categorias
    {
        [JsonProperty("submarcas")]
        public Submarca[] Submarcas { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}
