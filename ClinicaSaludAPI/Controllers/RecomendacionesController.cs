using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaSaludAPI.Data;
using ClinicaSaludAPI.Models;

namespace ClinicaSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecomendacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/recomendaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recomendacion>>> GetRecomendaciones()
        {
            return await _context.Recomendaciones
                .Include(r => r.Cita)
                .ToListAsync();
        }

        // GET: api/recomendaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recomendacion>> GetRecomendacion(int id)
        {
            var recomendacion = await _context.Recomendaciones
                .Include(r => r.Cita)
                .FirstOrDefaultAsync(r => r.IdRecomendacion == id);

            if (recomendacion == null)
            {
                return NotFound();
            }

            return recomendacion;
        }

        // GET: api/recomendaciones/paciente/5
        [HttpGet("paciente/{idPaciente}")]
        public async Task<ActionResult<IEnumerable<Recomendacion>>> GetRecomendacionesByPaciente(int idPaciente)
        {
            return await _context.Recomendaciones
                .Where(r => r.Cita != null && r.Cita.IdPaciente == idPaciente)
                .Include(r => r.Cita)
                    .ThenInclude(c => c != null ? c.Medico : null)
                .ToListAsync();
        }

        // POST: api/recomendaciones
        [HttpPost]
        public async Task<ActionResult<Recomendacion>> PostRecomendacion([FromBody] Recomendacion recomendacion)
        {
            _context.Recomendaciones.Add(recomendacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecomendacion), new { id = recomendacion.IdRecomendacion }, recomendacion);
        }

        // PUT: api/recomendaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendacion(int id, [FromBody] Recomendacion recomendacion)
        {
            if (id != recomendacion.IdRecomendacion)
            {
                return BadRequest();
            }

            _context.Entry(recomendacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/recomendaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecomendacion(int id)
        {
            var recomendacion = await _context.Recomendaciones.FindAsync(id);
            if (recomendacion == null)
            {
                return NotFound();
            }

            _context.Recomendaciones.Remove(recomendacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecomendacionExists(int id)
        {
            return _context.Recomendaciones.Any(e => e.IdRecomendacion == id);
        }
    }
}
