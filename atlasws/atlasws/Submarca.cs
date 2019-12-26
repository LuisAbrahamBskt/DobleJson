using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace atlasws
{
    public class Submarca
    {
        //[JsonProperty("estado")]
        //public string Estado { get; set; }

        //[JsonProperty("municipio")]
        //public string Municipio { get; set; }

        //[JsonProperty("id")]
        //public long Id { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        //[JsonProperty("submarca")]
        //public string Submarca { get; set; }

        //[JsonProperty("claveBanorte")]
        //public string ClaveBanorte { get; set; }

        //[JsonProperty("anio")]
        //public long Anio { get; set; }
    }
}