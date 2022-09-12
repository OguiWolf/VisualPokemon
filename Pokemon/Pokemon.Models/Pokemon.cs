using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    [Table("Pokemon")]
    public class Pokemon
    {
        [Key]
        public int IdPokemon { get; set; }
        public string Nombre { get; set; }
        public int Peso { get; set; }
        public int Altura { get; set; }
        [NotMapped]
        public List<string> Movimientos { get; set; }
        [NotMapped]
        public List<string> Tipos { get; set; }
    }
}
