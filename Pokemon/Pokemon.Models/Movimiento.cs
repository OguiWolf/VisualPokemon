using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pokemon.Models
{
    public class Movimiento
    {
        [Key]
        public int IdMovimiento { get; set; }
        public string Nombre { get; set; }
    }
}
