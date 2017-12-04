using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Competencia
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public Evento evento { get; set; }
        public List<Participante> participantes { get; set; }
        public Disciplina disciplina{ get; set; }
        public Ubicacion ubicacion { get; set; }

    }
}
