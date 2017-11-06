using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Equipo
    {
        public int id { get; set; }      
        public string nombre { get; set; }       
        public Disciplina disciplina { get; set; }        
        public string localidad { get; set; }        
        public Gremio gremio { get; set; }


    }
}
