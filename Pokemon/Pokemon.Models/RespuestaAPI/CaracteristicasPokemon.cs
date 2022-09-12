using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models.RespuestaAPI
{
    public class CaracteristicasPokemon
    {
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set;              }

        public long Weight { get; set; }

        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public List<Movimientos> Moves { get; set; }

        public List<TypeElement> Types { get; set; }

        public List<Habilidades> Abilities { get; set; }

        public List<Ubicaciones> Ubicacion { get; set; }

        public List<ColoresPokemon> Color { get; set; }
    }
}
