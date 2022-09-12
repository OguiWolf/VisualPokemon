using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pokemon.Models
{
    public class Entrenador
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntrenador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Ciudad { get; set; }
    }
}
