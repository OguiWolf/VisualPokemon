using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokemon.Models
{
    public class EntrenadorPokebola
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntrenadorPokebola { get; set; }
        public int? IdEntrenador { get; set; }
        public int? IdPokebola { get; set; }
        public int? IdPokemon { get; set; }
    }
}
