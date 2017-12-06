using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Noticia
    {
        public int id { get; set; }
        public string  titulo { get; set; }
        public string fecha { get; set; }
        public string noticia { get; set; }
        public string imagen { get; set; }
        public Evento evento { get; set; }
    }
}
