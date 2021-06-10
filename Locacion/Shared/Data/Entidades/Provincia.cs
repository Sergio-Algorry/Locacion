using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locacion.Comunes.Data.Entidades
{
    [Index(nameof(CodProvincia), Name = "UQ_Provincia_CodProvincia", IsUnique = true)]
    public class Provincia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código de la provincia es obligatorio.")]
        [MaxLength(1, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string CodProvincia { get; set; }

        [Required(ErrorMessage = "El Nombre de la provincia es obligatorio.")]
        [MaxLength(120, ErrorMessage = "El campo tiene como máximo {1} caracteres.")]
        public string NombreProvincia { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}
