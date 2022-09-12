using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokemon.Models
{
    public class TipoPokemon
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoPokemon { get; set; }
        public int IdTipo { get; set; }
        public int IdPokemon { get; set; }
    }
}
