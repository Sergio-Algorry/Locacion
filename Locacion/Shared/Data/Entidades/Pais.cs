using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locacion.Comunes.Data.Entidades
{
    [Index(nameof(CodPais), Name = "UQ_Pais_CodPais", IsUnique =true)]
    public class Pais
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código del pais es obligatorio.")]
        [MaxLength(2, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string CodPais { get; set; }

        [Required(ErrorMessage = "El Nombre del pais es obligatorio.")]
        [MaxLength(120, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string NombrePais { get; set; }

        public List<Provincia> Provincias { get; set; }
    }
}
