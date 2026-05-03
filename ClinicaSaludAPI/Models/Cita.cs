using System.ComponentModel.DataAnnotations;

namespace ClinicaSaludAPI.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }
        
        [Required]
        public int IdPaciente { get; set; }
        
        [Required]
        public int IdMedico { get; set; }
        
        [Required]
        public DateTime FechaCita { get; set; }
        
        [Required]
        public TimeSpan HoraCita { get; set; }
        
        [Required]
        public string MotivoConsulta { get; set; } = string.Empty;
        
        [Required]
        public string Estado { get; set; } = "agendada"; // 'agendada', 'completada', 'cancelada'
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        // Navigation properties
        public Paciente? Paciente { get; set; }
        public Medico? Medico { get; set; }
    }
}