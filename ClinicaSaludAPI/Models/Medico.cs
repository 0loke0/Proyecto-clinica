using System.ComponentModel.DataAnnotations;

namespace ClinicaSaludAPI.Models
{
    public class Medico
    {
        [Key]
        public int IdMedico { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string CodigoMedico { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;
        
        [Required]
        public int IdEspecialidad { get; set; }
        
        [MaxLength(10)]
        public string? Telefono { get; set; }
        
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }
        
        [MaxLength(100)]
        public string? HorarioAtencion { get; set; }
        
        [Required]
        public string Estado { get; set; } = "disponible"; // 'disponible' u 'ocupado'
        
        public bool Activo { get; set; } = true;
        
        // Navigation property
        public Especialidad? Especialidad { get; set; }
    }
}