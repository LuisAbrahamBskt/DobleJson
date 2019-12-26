using Newtonsoft.Json;

namespace atlasws
{
    public class DatOS
    {
        [JsonProperty("categorias")]
        public Categorias[] categorias { get; set; }

    }
}
