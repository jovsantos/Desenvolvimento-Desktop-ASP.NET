using EstacionamentoSenac.API.Data;
using EstacionamentoSenac.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoSenac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos() =>
            await _context.Veiculos.Include(v => v.Motorista).ToListAsync();

        // GET: api/Veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.Include(v => v.Motorista).FirstOrDefaultAsync(v => v.Id == id);

            if (veiculo == null)
                return NotFound();

            return veiculo;
        }

        // POST: api/Veiculos
        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.Id }, veiculo);
        }

        // PUT: api/Veiculos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return BadRequest();

            var veiculoExistente = await _context.Veiculos.FindAsync(id);
            if (veiculoExistente == null)
                return NotFound();

            veiculoExistente.Placa = veiculo.Placa;
            veiculoExistente.Modelo = veiculo.Modelo;
            veiculoExistente.Cor = veiculo.Cor;
            veiculoExistente.MotoristaId = veiculo.MotoristaId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Veiculos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return NotFound();

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
