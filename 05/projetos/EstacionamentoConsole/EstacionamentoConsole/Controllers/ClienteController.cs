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

        public void AdicionarCliente()
        {
            Console.Clear();
            Console.WriteLine("===== Adicionar Novo Cliente =====");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();           

            Console.Write("CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("Telefone (Opcional): ");
            string telefone = Console.ReadLine();

            Cliente c1 = new Cliente(nome, cpf, telefone);
            _context.Clientes.Add(c1);
            _context.SaveChanges();

            Console.WriteLine("Cliente adicionado com sucesso! Pressione qualquer tecla para retornar.");
            Console.ReadKey();
        }

        public void VerDetalhesCliente()
        {
            Console.Clear();
            Console.WriteLine("===== Detalhes do Cliente =====");
            Console.Write("Digite o ID do cliente: ");
            var clienteId = int.Parse(Console.ReadLine());

            // Buscar o cliente no banco de dados
            var cliente = _context.Clientes
                .FirstOrDefault(cliente => cliente.Id == clienteId);

            if (cliente == null)
                Console.WriteLine("Cliente não encontrado.");
            else
            {
                Console.WriteLine($"ID: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"CPF: {cliente.Cpf}");
                Console.WriteLine($"Telefone: {cliente.Telefone}");
            }
            Console.WriteLine("\nPressione qualquer tecla para retornar.");
            Console.ReadKey();
        }
    }
}
