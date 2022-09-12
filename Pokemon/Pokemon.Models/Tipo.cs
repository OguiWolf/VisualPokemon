using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pokemon.Models
{
    public class Tipo
    {
        [Key]
        public int IdTipo { get; set; }
        public string Nombre { get; set; }
    }
}
