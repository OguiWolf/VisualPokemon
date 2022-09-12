using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models
{
    public class PokemonJobContext : DbContext
    {
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Caracteristica> Caracteristica { get; set; }
        public DbSet<Entrenador> Entrenador { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }
        public DbSet<MovimientoPokemon> MovimientoPokemon { get; set; }
        public DbSet<TipoPokemon> TipoPokemon { get; set; }
        public DbSet<EntrenadorPokebola> EntrenadorPokebola { get; set; }
        public DbSet<Pokebola> Pokebola { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Pokemon;User Id=sa;Password=sugus;TrustServerCertificate=True;");
        }
    }
}
