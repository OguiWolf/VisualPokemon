using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Pokemon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Pokemon.Models.RespuestaAPI;

namespace Pokemon.Job
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            /*Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            var currentDate = DateTime.Now;
            Console.WriteLine($"{Environment.NewLine}Hello, {name}, on {currentDate:d} at {currentDate:t}!");
            Console.Write($"{Environment.NewLine}Press any key to exit...");
            Console.ReadKey(true);*/

            string metadataPokemon = "https://pokeapi.co/api/v2/pokemon",
                   metadataTipo = "https://pokeapi.co/api/v2/type",
                   metadataMovimiento = "https://pokeapi.co/api/v2/move",
                   metadataPokebola = "https://pokeapi.co/api/v2/item",
                   metadataColor = "https://pokeapi.co/api/v2/pokemon-color";

            using (var context = new PokemonJobContext())
            {
                await PokebolaAsync(metadataPokebola, context);
                await TipoAsync(metadataTipo, context);
                await MovimientoAsync(metadataMovimiento, context);
                await ColorAsync(metadataColor, context);
                await PokemonAsync(metadataPokemon, context);
            }
            Console.ReadLine();
        }

        public static async Task PokebolaAsync (string metadataPokebola, PokemonJobContext contexto)
        {
            var httpClient = new HttpClient();

            var responsePokebola = await httpClient.GetAsync(metadataPokebola + "?limit=" + 16);
            string jsonDataPokebola = await responsePokebola.Content.ReadAsStringAsync();
            var resultPokebola = JsonConvert.DeserializeObject<SearchTypePokemonResult>(jsonDataPokebola);

            foreach (var respuestaPokebola in resultPokebola.Results)
            {

                var tipoPokebola = await httpClient.GetAsync(respuestaPokebola.Url);
                string jsonPokebola = await tipoPokebola.Content.ReadAsStringAsync();
                var pokebolas = JsonConvert.DeserializeObject<Models.RespuestaAPI.MovimientosTiposPokemon>(jsonPokebola);

                var pokebola = new Pokemon.Models.Pokebola
                {
                    IdPokebola = pokebolas.Id,
                    Nombre = pokebolas.Name
                };
                if (contexto.Pokebola.Find(pokebolas.Id) == null)
                {
                    contexto.Pokebola.Add(pokebola);
                }
                else
                {
                    contexto.Pokebola.Find(pokebolas.Id).Nombre = pokebola.Nombre;
                }
                contexto.SaveChanges();
            }
        }

        public static async Task TipoAsync(string metadataTipo, PokemonJobContext contexto)
        {
            var httpClient = new HttpClient();

            var responseType = await httpClient.GetAsync(metadataTipo);
            string jsonDataType = await responseType.Content.ReadAsStringAsync();
            var resultType = JsonConvert.DeserializeObject<SearchTypePokemonResult>(jsonDataType);

            foreach (var respuestaTipo in resultType.Results)
            {

                var respuestaTipoPokemon = await httpClient.GetAsync(respuestaTipo.Url);
                string jsonDetallePokemon = await respuestaTipoPokemon.Content.ReadAsStringAsync();
                var tipos = JsonConvert.DeserializeObject<Models.RespuestaAPI.MovimientosTiposPokemon>(jsonDetallePokemon);

                var tipo = new Pokemon.Models.Tipo
                {
                    IdTipo = tipos.Id,
                    Nombre = tipos.Name
                };
                if (contexto.Tipo.Find(tipos.Id) == null)
                {
                    contexto.Tipo.Add(tipo);
                }
                else
                {
                    contexto.Pokebola.Find(tipos.Id).Nombre = tipo.Nombre;
                }
                contexto.SaveChanges();
            }
        }

        public static async Task MovimientoAsync(string metadataMovimiento, PokemonJobContext contexto)
        {
            var httpClient = new HttpClient();

            var respuestaMovimiento = await httpClient.GetAsync(metadataMovimiento + "?limit=" + 844);
            string jsonDataMovimiento = await respuestaMovimiento.Content.ReadAsStringAsync();
            var resultadoMovimiento = JsonConvert.DeserializeObject<SearchTypePokemonResult>(jsonDataMovimiento);

            foreach (var Movimiento in resultadoMovimiento.Results)
            {
                var respuestaMovimientoPokemon = await httpClient.GetAsync(Movimiento.Url);
                string jsonDetallePokemon = await respuestaMovimientoPokemon.Content.ReadAsStringAsync();
                var movimientos = JsonConvert.DeserializeObject<Models.RespuestaAPI.MovimientosTiposPokemon>(jsonDetallePokemon);

                var movimiento = new Pokemon.Models.Movimiento
                {
                    IdMovimiento = movimientos.Id,
                    Nombre = movimientos.Name
                };
                if (contexto.Movimiento.Find(movimientos.Id) == null)
                {
                    contexto.Movimiento.Add(movimiento);
                }
                else
                {
                    contexto.Movimiento.Find(movimientos.Id).Nombre = movimiento.Nombre;
                }
                contexto.SaveChanges();
            }
        }

        public static async Task ColorAsync(string metadataColor, PokemonJobContext contexto)
        {
            var httpClient = new HttpClient();

            var responseColor = await httpClient.GetAsync(metadataColor);
            string jsonColoresPokemon = await responseColor.Content.ReadAsStringAsync();
            var pokemonColor = JsonConvert.DeserializeObject<Models.RespuestaAPI.SearchColores>(jsonColoresPokemon);

            foreach (ColoresPokemon urlColores in pokemonColor.Results)
            {
                var respuestaColorPokemon = await httpClient.GetAsync(urlColores.Url);
                string jsonIdColoresPokemon = await respuestaColorPokemon.Content.ReadAsStringAsync();
                var pokemonIdColor = JsonConvert.DeserializeObject<Models.RespuestaAPI.ColoresPokemon>(jsonIdColoresPokemon);

                foreach (var ubicacionPokemon in pokemonIdColor.PokemonSpecies)
                {
                    string[] respuestaIdPokemonColor = ubicacionPokemon.Url.Split('/');

                    var caracteristicaDelPokemon = new Pokemon.Models.Caracteristica
                    {
                        Nombre = "Color",
                        Valor = urlColores.Name,
                        IdPokemon = Int32.Parse(respuestaIdPokemonColor[6])
                    };
                    contexto.Caracteristica.Add(caracteristicaDelPokemon);
                }
                contexto.SaveChanges();
            }
        }

        public static async Task PokemonAsync(string metadataPokemon, PokemonJobContext contexto)
        {
            var httpClient = new HttpClient();

            var responsePokemon = await httpClient.GetAsync(metadataPokemon + "?offset=" + 0 + "&limit=" + 1154);
            string jsonDataPokemon = await responsePokemon.Content.ReadAsStringAsync();
            var resultPokemon = JsonConvert.DeserializeObject<SearchPokemonResult>(jsonDataPokemon);

            foreach (var respuesta in resultPokemon.Resultados)
            {
                var respuestaDetallePokemon = await httpClient.GetAsync(respuesta.Url);
                string jsonDetallePokemon = await respuestaDetallePokemon.Content.ReadAsStringAsync();
                var caracteristicas = JsonConvert.DeserializeObject<Models.RespuestaAPI.CaracteristicasPokemon>(jsonDetallePokemon);

                var pokemon = new Pokemon.Models.Pokemon
                {
                    IdPokemon = caracteristicas.Id,
                    Nombre = caracteristicas.Name,
                    Peso = (int)caracteristicas.Weight,
                    Altura = (int)caracteristicas.Height
                };
                var consulta = contexto.Pokemon.Find(caracteristicas.Id);
                if (consulta == null)
                {
                    contexto.Pokemon.Add(pokemon);
                }
                else
                {
                    consulta.Nombre = pokemon.Nombre;
                    consulta.Peso = pokemon.Peso;
                    consulta.Altura = pokemon.Altura;
                }
                contexto.SaveChanges();

                foreach (Movimientos urlMovimiento in caracteristicas.Moves)
                {
                    var respuestaIdMovimientosPokemon = await httpClient.GetAsync(urlMovimiento.MoveMove.Url);
                    string jsonIdMovimientosPokemon = await respuestaIdMovimientosPokemon.Content.ReadAsStringAsync();
                    var idMovimientos = JsonConvert.DeserializeObject<Models.RespuestaAPI.Movimientos>(jsonIdMovimientosPokemon);

                    var movimientoPokemon = new Pokemon.Models.MovimientoPokemon
                    {
                        IdPokemon = caracteristicas.Id,
                        IdMovimiento = idMovimientos.Id
                    };

                    contexto.MovimientoPokemon.Add(movimientoPokemon);

                }
                contexto.SaveChanges();

                foreach (TypeElement urlTipo in caracteristicas.Types)
                {
                    var respuestaIdTiposPokemon = await httpClient.GetAsync(urlTipo.Type.Url);
                    string jsonIdTiposPokemon = await respuestaIdTiposPokemon.Content.ReadAsStringAsync();
                    var idTipos = JsonConvert.DeserializeObject<Models.RespuestaAPI.TypeElement>(jsonIdTiposPokemon);

                    var tipoPokemon = new Pokemon.Models.TipoPokemon
                    {
                        IdPokemon = caracteristicas.Id,
                        IdTipo = idTipos.Id
                    };
                    contexto.TipoPokemon.Add(tipoPokemon);
                }
                contexto.SaveChanges();
            }
        }

        
    }
}
