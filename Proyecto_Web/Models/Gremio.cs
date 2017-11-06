using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Gremio
    {
        public int id { get; set; }
        public string localidad { get; set; }
        public Estado estado { get; set; }
    }
}
