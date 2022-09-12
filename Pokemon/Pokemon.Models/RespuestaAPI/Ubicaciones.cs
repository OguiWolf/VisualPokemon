using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models.RespuestaAPI
{
    public class Ubicaciones
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location_area_encounters")]
        public string LocationAreaEncounters { get; set; }
    }
}
