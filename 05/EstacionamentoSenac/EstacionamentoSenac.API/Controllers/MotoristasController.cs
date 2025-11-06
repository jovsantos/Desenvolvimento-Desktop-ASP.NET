using EstacionamentoSenac.API.Data;
using EstacionamentoSenac.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoSenac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoristasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Motoristas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motorista>>> GetMotoristas()
            => await _context.Motoristas.Include(m => m.Veiculos).ToListAsync();

        // GET: api/Motoristas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motorista>> GetMotorista(int id)
        {
            var motorista = await _context.Motoristas.Include(m => m.Veiculos).FirstOrDefaultAsync(m => m.Id == id);

            if (motorista == null) return NotFound();

            return motorista;
        }

        // POST: api/Motoristas
        [HttpPost]
        public async Task<ActionResult<Motorista>> PostMotorista(Motorista motorista)
        {
            _context.Motoristas.Add(motorista);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMotorista), new { id = motorista.Id }, motorista);
        }

        // PUT: api/Motoristas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorista(int id, Motorista motorista)
        {
            if (id != motorista.Id) return BadRequest();

            var motoristaExistente = await _context.Motoristas.FindAsync(id);
            if (motoristaExistente == null) return NotFound();

            motoristaExistente.Nome = motorista.Nome;
            motoristaExistente.Cpf = motorista.Cpf;
            motoristaExistente.Telefone = motorista.Telefone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Motoristas.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Motoristas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorista(int id)
        {
            var motorista = await _context.Motoristas.FindAsync(id);
            if (motorista == null)
                return NotFound();

            _context.Motoristas.Remove(motorista);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
