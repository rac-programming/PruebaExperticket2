using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaExperticker.Shared
{
    public class Persona
    {

        [Required]
        public string DNI { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string Sexo { get; set; }


        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }
        public string Pais { get; set; }
        public int CodigoPostal { get; set; }
    }
}
