using System.ComponentModel.DataAnnotations;

namespace ClinicaSaludAPI.Models
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string NombreEspecialidad { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
    }
}