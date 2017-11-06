using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Actividad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string fecha_hora { get; set; }        
        public string descripcion { get; set; }
        public Ubicacion ubicacion { get; set; }
        public string tipo { get; set; }
    }
}
