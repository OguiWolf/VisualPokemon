using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokemon.Models
{
    public class MovimientoPokemon
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovimientoPokemon { get; set; }
        public int IdMovimiento { get; set; }
        public int IdPokemon { get; set; }
    }
}
