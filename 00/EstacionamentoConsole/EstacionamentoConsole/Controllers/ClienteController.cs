using EstacionamentoConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoConsole.Controllers
{
    internal class ClienteController
    {
        EstacionamentoDbContext _context;

        public ClienteController(EstacionamentoDbContext context)
        {
            _context = context;
        }

        public void ListarClientes()
        {
            Console.Clear(); // Limpa a tela do console
            var clientes = _context.Clientes.ToList();

            foreach(var cliente in clientes)
            {
                Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}");
            }
            Console.WriteLine("\nPressione qualquer tecla para retornar.");
            Console.ReadKey();
        }
    }
}
