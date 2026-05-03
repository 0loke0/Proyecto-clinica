using System.ComponentModel.DataAnnotations;

namespace ClinicaSaludAPI.Models
{
    public class Paciente
    {
        [Key]
        public int IdPaciente { get; set; }
        
        [Required]
        [MaxLength(15)]
        public string Identificacion { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;
        
        [Required]
        [Range(0, 120)]
        public int Edad { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Telefono { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? EPS { get; set; }
        
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}