using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Evento
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_final { get; set; }
        public int imagen_id { get; set; }
        public string url_imagen { get; set; }
    }
}
