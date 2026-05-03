using System.ComponentModel.DataAnnotations;

namespace ClinicaSaludAPI.Models
{
    public class Recomendacion
    {
        [Key]
        public int IdRecomendacion { get; set; }
        
        [Required]
        public int IdCita { get; set; }
        
        [Required]
        public string Diagnostico { get; set; } = string.Empty;
        
        public string? Medicamentos { get; set; }
        
        [Required]
        public string RecomendacionesCuidados { get; set; } = string.Empty;
        
        public DateTime? ProximaCita { get; set; }
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Navigation property
        public Cita? Cita { get; set; }
    }
}