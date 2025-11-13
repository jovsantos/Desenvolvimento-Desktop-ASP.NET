using EstacionamentoSenac.API.Data;
using EstacionamentoSenac.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoSenac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosEstacionamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosEstacionamentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistrosEstacionamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroEstacionamento>>> GetRegistrosEstacionamento() =>
            await _context.RegistrosEstacionamento.Include(r => r.Veiculo).Include(r => r.Vaga).ToListAsync();

        // GET: api/RegistrosEstacionamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroEstacionamento>> GetRegistroEstacionamento(int id)
        {
            var registroEstacionamento = await _context.RegistrosEstacionamento.Include(r => r.Veiculo).Include(r => r.Vaga).FirstOrDefaultAsync(r => r.Id == id);

            if (registroEstacionamento == null)
                return NotFound();

            return registroEstacionamento;
        }

        // POST: api/RegistrosEstacionamento
        [HttpPost]
        public async Task<ActionResult<RegistroEstacionamento>> PostRegistroEstacionamento(RegistroEstacionamento registroEstacionamento)
        {
            _context.RegistrosEstacionamento.Add(registroEstacionamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistroEstacionamento), new { id = registroEstacionamento.Id }, registroEstacionamento);
        }

        // PUT: api/RegistrosEstacionamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroEstacionamento(int id, RegistroEstacionamento registroEstacionamento)
        {
            if (id != registroEstacionamento.Id)
                return BadRequest();

            var registroExistente = await _context.RegistrosEstacionamento.FindAsync(id);
            if (registroExistente == null)
                return NotFound();

            registroExistente.VeiculoId = registroEstacionamento.VeiculoId;
            registroExistente.VagaId = registroEstacionamento.VagaId;
            registroExistente.DataHoraEntrada = registroEstacionamento.DataHoraEntrada;
            registroExistente.DataHoraSaida = registroEstacionamento.DataHoraSaida;
            registroExistente.ValorFinal = registroEstacionamento.ValorFinal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RegistrosEstacionamento.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RegistrosEstacionamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroEstacionamento(int id)
        {
            var registroEstacionamento = await _context.RegistrosEstacionamento.FindAsync(id);
            if (registroEstacionamento == null)
                return NotFound();

            _context.RegistrosEstacionamento.Remove(registroEstacionamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
