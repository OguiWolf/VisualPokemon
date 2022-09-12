using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Models.RespuestaAPI
{
    public class Movimientos
    {
        public int Id { get; set; }

        [JsonProperty("move")]
        public Species MoveMove { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
