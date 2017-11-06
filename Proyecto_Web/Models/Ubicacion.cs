using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Ubicacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string longitud { get; set; }
        public string latitud { get; set; }
    }
}
