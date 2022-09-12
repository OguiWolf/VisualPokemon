using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokemon.Models
{
    [Table("Caracteristica")]
    public class Caracteristica
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCaracteristica { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public int IdPokemon { get; set; }
    }
}
