using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models.RespuestaAPI
{
    public class ColoresPokemon
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("pokemon_species")]
        public List<EspeciePokemon> PokemonSpecies { get; set; }

        public List<int> idPokemon { get; set; }
    }
}

