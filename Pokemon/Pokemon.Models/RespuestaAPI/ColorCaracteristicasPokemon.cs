using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models.RespuestaAPI
{
    public class ColorCaracteristicasPokemon
    {
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set;              }

        [JsonProperty("url")]
        public string Url { get; set; }

        public List<ColoresPokemon> Color { get; set; }
    }
}
