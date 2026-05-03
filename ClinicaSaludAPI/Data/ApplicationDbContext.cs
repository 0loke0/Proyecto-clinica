using Microsoft.EntityFrameworkCore;
using ClinicaSaludAPI.Models;

namespace ClinicaSaludAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany()
                .HasForeignKey(m => m.IdEspecialidad);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany()
                .HasForeignKey(c => c.IdPaciente);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Medico)
                .WithMany()
                .HasForeignKey(c => c.IdMedico);

            modelBuilder.Entity<Recomendacion>()
                .HasOne(r => r.Cita)
                .WithOne()
                .HasForeignKey<Recomendacion>(r => r.IdCita);

            // Índices únicos
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.CodigoMedico)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.Email)
                .IsUnique();

            // Sembrar datos iniciales
            modelBuilder.Entity<Especialidad>().HasData(
                new Especialidad { IdEspecialidad = 1, NombreEspecialidad = "Medicina General", Descripcion = "Atención primaria y consultas generales para toda la familia." },
                new Especialidad { IdEspecialidad = 2, NombreEspecialidad = "Pediatría", Descripcion = "Cuidado especializado para bebés, niños y adolescentes." },
                new Especialidad { IdEspecialidad = 3, NombreEspecialidad = "Odontología", Descripcion = "Salud oral, limpiezas, tratamientos y estética dental." },
                new Especialidad { IdEspecialidad = 4, NombreEspecialidad = "Cardiología", Descripcion = "Diagnóstico y tratamiento de enfermedades del corazón." },
                new Especialidad { IdEspecialidad = 5, NombreEspecialidad = "Ginecología", Descripcion = "Salud femenina, control prenatal y seguimiento especializado." },
                new Especialidad { IdEspecialidad = 6, NombreEspecialidad = "Traumatología", Descripcion = "Lesiones musculares, articulaciones y rehabilitación." }
            );

            modelBuilder.Entity<Medico>().HasData(
                // Medicina General
                new Medico { IdMedico = 1, CodigoMedico = "MG-001", NombreCompleto = "Dr. Andrés Gómez", IdEspecialidad = 1, HorarioAtencion = "Lun–Vie 8am–4pm", Estado = "disponible" },
                new Medico { IdMedico = 2, CodigoMedico = "MG-002", NombreCompleto = "Dra. Laura Herrera", IdEspecialidad = 1, HorarioAtencion = "Lun–Sáb 9am–5pm", Estado = "disponible" },
                // Pediatría
                new Medico { IdMedico = 3, CodigoMedico = "PD-001", NombreCompleto = "Dra. Sofía Ramírez", IdEspecialidad = 2, HorarioAtencion = "Lun–Vie 7am–3pm", Estado = "disponible" },
                new Medico { IdMedico = 4, CodigoMedico = "PD-002", NombreCompleto = "Dr. Carlos Medina", IdEspecialidad = 2, HorarioAtencion = "Mar–Sáb 10am–6pm", Estado = "ocupado" },
                // Odontología
                new Medico { IdMedico = 5, CodigoMedico = "OD-001", NombreCompleto = "Dr. Felipe Torres", IdEspecialidad = 3, HorarioAtencion = "Lun–Vie 8am–5pm", Estado = "disponible" },
                new Medico { IdMedico = 6, CodigoMedico = "OD-002", NombreCompleto = "Dra. Valentina Cruz", IdEspecialidad = 3, HorarioAtencion = "Lun–Sáb 9am–4pm", Estado = "disponible" },
                // Cardiología
                new Medico { IdMedico = 7, CodigoMedico = "CD-001", NombreCompleto = "Dr. Roberto Nieto", IdEspecialidad = 4, HorarioAtencion = "Lun–Jue 8am–4pm", Estado = "disponible" },
                new Medico { IdMedico = 8, CodigoMedico = "CD-002", NombreCompleto = "Dra. Marcela Ríos", IdEspecialidad = 4, HorarioAtencion = "Mié–Sáb 9am–5pm", Estado = "disponible" },
                // Ginecología
                new Medico { IdMedico = 9, CodigoMedico = "GN-001", NombreCompleto = "Dra. Patricia Lozano", IdEspecialidad = 5, HorarioAtencion = "Lun–Vie 7am–3pm", Estado = "disponible" },
                new Medico { IdMedico = 10, CodigoMedico = "GN-002", NombreCompleto = "Dra. Isabel Morales", IdEspecialidad = 5, HorarioAtencion = "Mar–Sáb 10am–6pm", Estado = "ocupado" },
                // Traumatología
                new Medico { IdMedico = 11, CodigoMedico = "TR-001", NombreCompleto = "Dr. Javier Pedraza", IdEspecialidad = 6, HorarioAtencion = "Lun–Vie 8am–4pm", Estado = "disponible" },
                new Medico { IdMedico = 12, CodigoMedico = "TR-002", NombreCompleto = "Dr. Santiago Vargas", IdEspecialidad = 6, HorarioAtencion = "Lun–Sáb 9am–5pm", Estado = "disponible" }
            );
        }
    }
}