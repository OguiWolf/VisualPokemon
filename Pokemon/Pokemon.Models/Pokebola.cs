using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pokemon.Models
{
    public class Pokebola
    {
        [Key]
        public int IdPokebola { get; set; }
        public string Nombre { get; set; }
    }
}
