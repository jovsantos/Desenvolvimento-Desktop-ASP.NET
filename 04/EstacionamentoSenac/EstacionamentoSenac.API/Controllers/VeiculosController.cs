using EstacionamentoSenac.API.Data;
using EstacionamentoSenac.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoSenac.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Veiculo>> GetVeiculos()
        {
            return Ok(_context.Veiculos.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Veiculo> GetVeiculoById(int id)
        {
            //_context.Veiculos.FirstOrDefault(v => v.Id == id);
            var veiculo = _context.Veiculos.Find(id);

            if (veiculo == null)
                return NotFound();

            return Ok(veiculo);
        }

        [HttpPost]
        public ActionResult<Veiculo> PostVeiculo(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            _context.SaveChanges();

            return Created();
        }

        [HttpPut("{id}")]
        public ActionResult<Veiculo> PutVeiculo(int id, Veiculo veiculoNovo)
        {
            if (id != veiculoNovo.Id) 
                return BadRequest("Veiculo informado na URL diferente do objeto JSON");

            var veiculoExistente = _context.Veiculos.Find(id);
            if (veiculoExistente == null) return NotFound();

            // Atualizar de fato o veículo
            veiculoExistente.Marca = veiculoNovo.Marca;
            veiculoExistente.Modelo = veiculoNovo.Modelo;
 
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Veiculo> DeleteVeiculo(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null) return NotFound();

            _context.Veiculos.Remove(veiculo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
