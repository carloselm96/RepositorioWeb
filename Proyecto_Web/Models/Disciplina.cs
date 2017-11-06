using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Disciplina
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public Disciplina(int i, string nom)
        {
            id = i;
            nombre = nom;
        }
        public Disciplina()
        {            
        }
    }
}
