using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace atlasws
{
    class FutureVisitsModel
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string idSM { get; set; }
        public string nombreSM { get; set; }


        public FutureVisitsModel()
        {
            String _id = id;
            String _nombre = nombre;
            String _idSM = idSM;
            String _nombreSM = nombreSM;


        }
    }

}
