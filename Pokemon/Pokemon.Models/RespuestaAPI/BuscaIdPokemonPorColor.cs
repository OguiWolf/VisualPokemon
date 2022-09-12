using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models.RespuestaAPI
{
    public class BuscaIdPokemonPorColor
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public List<ColoresPokemon> Results { get; set; }

        [JsonProperty("pokemon_species")]
        public List<EspeciePokemon> PokemonSpecies { get; set; }
    }
}
