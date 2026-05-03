using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaSaludAPI.Data;
using ClinicaSaludAPI.Models;

namespace ClinicaSaludAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/medicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            return await _context.Medicos.Include(m => m.Especialidad).ToListAsync();
        }

        // GET: api/medicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedico(int id)
        {
            var medico = await _context.Medicos.Include(m => m.Especialidad)
                .FirstOrDefaultAsync(m => m.IdMedico == id);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        // GET: api/medicos/especialidad/1
        [HttpGet("especialidad/{idEspecialidad}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicosByEspecialidad(int idEspecialidad)
        {
            return await _context.Medicos
                .Where(m => m.IdEspecialidad == idEspecialidad)
                .Include(m => m.Especialidad)
                .ToListAsync();
        }

        // POST: api/medicos
        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico([FromBody] Medico medico)
        {
            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedico), new { id = medico.IdMedico }, medico);
        }

        // PUT: api/medicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedico(int id, [FromBody] Medico medico)
        {
            if (id != medico.IdMedico)
            {
                return BadRequest();
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(id))
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

        // DELETE: api/medicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedico(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.IdMedico == id);
        }
    }
}
