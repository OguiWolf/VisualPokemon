using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokemon.Models;
using Pokemon.Models.RespuestaAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonJobContext _context;

        public PokemonController()
        {
            _context = new PokemonJobContext();
        }

        // GET: api/Pokemon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Pokemon>>> GetPokemon([FromQuery] int limit, [FromQuery] int offset)
        {
            List<Models.Pokemon> listPokemon = new List<Models.Pokemon>();

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon"+"?offset="+offset+"&limit="+limit);   
            
            if (respuesta.IsSuccessStatusCode)
            {
                string jsonData = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<SearchPokemonResult>(jsonData);
 
                foreach (var informacionPokemonAPI in resultado.Resultados)
                {               
                    var respuestaDetallePokemon = await httpClient.GetAsync(informacionPokemonAPI.Url);
                    string jsonDetallePokemon = await respuestaDetallePokemon.Content.ReadAsStringAsync();
                    var caracteristicas = JsonConvert.DeserializeObject<Models.RespuestaAPI.CaracteristicasPokemon>(jsonDetallePokemon);

                    var pokemon = new Models.Pokemon()
                    {
                        IdPokemon = caracteristicas.Id,
                        Nombre = caracteristicas.Name,
                        Peso = (int)caracteristicas.Height,
                        Altura = (int)caracteristicas.Weight
                    };

                    pokemon.Tipos = new List<string>();
                    foreach (TypeElement tipo in caracteristicas.Types)
                        pokemon.Tipos.Add(tipo.Type.Name);

                    pokemon.Movimientos = new List<string>();
                    foreach (Movimientos movimiento in caracteristicas.Moves)
                        pokemon.Movimientos.Add(movimiento.MoveMove.Name);

                    listPokemon.Add(pokemon);
                }
            }
            return listPokemon;
        }

        // GET: api/Pokemon/Capturados/2
        [HttpGet("Capturados/{idEntrenador}")]
        public async Task<ActionResult<Models.EntrenadorPokebola>> CapturaPokemon(int idEntrenador)
        {
            var entrenador = _context.Entrenador.Find(idEntrenador);
            if (entrenador == null)
                return BadRequest();

            int pokemonesCapturados = _context.EntrenadorPokebola.Count(x => x.IdEntrenador == idEntrenador && x.IdPokemon != null);
            int pokebolasVacias = _context.EntrenadorPokebola.Count(x => x.IdEntrenador == idEntrenador && x.IdPokemon == null);

            return Ok(new
            {
                mensaje = "El entrenador siguinete cuenta con:",
                entrenador = entrenador,
                pokemonesCapturados = pokemonesCapturados,
                pokebolasVacias = pokebolasVacias
            }) ;
           
        }

        // POST: api/Pokemon
        [HttpPost]
        public async Task<ActionResult<Models.Entrenador>> AltaEntrenador(Entrenador entrenadores)
        {
            var busqueda = _context.Entrenador.Where(p => p.Nombre == entrenadores.Nombre && p.Apellido == entrenadores.Apellido).ToList();
            if (busqueda.Count == 0)
                return BadRequest();

            _context.Entrenador.Add(entrenadores);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPokemon), new { id = entrenadores.IdEntrenador }, entrenadores);
        }

        // POST: api/Pokemon/Entrenador/1/AgregarPokebola/5
        [HttpPost("Entrenador/{idEntrenador}/AgregarPokebola/{idPokebola}")]
        public async Task<ActionResult<Models.EntrenadorPokebola>> AsignacionPokebolaEntrenador(int idEntrenador, int idPokebola )
        {
            var entrenador = _context.Entrenador.Find(idEntrenador);
            var pokebola = _context.Pokebola.Find(idPokebola);
            var relacionEntrenadorPokebola = new Models.EntrenadorPokebola();

            if (entrenador == null || pokebola==null)
                return BadRequest();

            relacionEntrenadorPokebola.IdEntrenador = idEntrenador;
            relacionEntrenadorPokebola.IdPokebola = idPokebola;

            _context.EntrenadorPokebola.Add(relacionEntrenadorPokebola);
            _context.SaveChanges();

            return Ok(new
            {
                mensaje = "Pokebola adquirida.",
                entrenador = entrenador,
                pokebola = pokebola
            });
        }

        // POST: api/Pokemon/Entrenador/1/Pokebola/5/Pokemon/25
        [HttpPost("Entrenador/{idEntrenador}/Pokebola/{idPokebola}/Pokemon/{idPokemon}")]
        public async Task<ActionResult<Models.EntrenadorPokebola>> CapturaPokemon(int idEntrenador, int idPokebola, int idPokemon)
        {
            var entrenador = _context.Entrenador.Find(idEntrenador);
            var pokemon = _context.Pokemon.Find(idPokemon);
            var pokebola = _context.Pokebola.Find(idPokebola);
            var entrenadorPokebola = _context.EntrenadorPokebola.Where(p => p.IdEntrenador == idEntrenador && p.IdPokebola == idPokebola).FirstOrDefault();

            if (entrenadorPokebola == null)
                return BadRequest(new 
                {
                    Mensaje = "El entrenador no cuenta con la pokebola, favor de conseguirla o el entrenador no existe.",
                    entrenador = entrenador,
                    pokebola = pokebola
                });

            if (entrenadorPokebola.IdPokemon != null)              
                return BadRequest(new
                {
                    Mensaje = "El siguiente entrenador ya tiene un pokemon atrapado con esta pokebola.",
                    entrenador = entrenador,
                    pokemon = pokemon
                });

            entrenadorPokebola.IdPokemon = idPokemon;
            _context.SaveChanges();

            return Ok(new 
            { 
                Mensaje = "Un pokemon fue capturado.",
                Entrenador = entrenador,
                Pokebola = pokebola,
                Pokemon = pokemon
            });
        }
    }
}
