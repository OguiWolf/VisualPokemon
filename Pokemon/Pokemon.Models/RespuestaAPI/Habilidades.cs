using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models.RespuestaAPI
{
    public class Habilidades
    {
        [JsonProperty("ability")]
        public Species AbilityAbility { get; set; }
    }
}
