using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Web.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "Required.")]
        [DataType ( DataType.EmailAddress)]
        public string correo { get; set; }

        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.Password)]        
        public string password { get; set; }
    }
}
