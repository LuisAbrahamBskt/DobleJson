using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace atlasws
{
    public class codigopost
    {
        public string Codigo { get; set; }
        public string ciudad { get; set; }
        public string colonia { get; set; }
        public int idestado { get; set; }
        public int idasentamiento { get; set; }
        public codigopost()
        {
            String codigo = Codigo;
            String Ciudad = ciudad;
            int Idestado = idestado;
            int Idasentamiento = idasentamiento;
        }
    }
  
}
