using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Models.RespuestaAPI
{
    public class TypeElement
    {
        public int Id { get; set; }

        [JsonProperty("type")]
        public Species Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}

