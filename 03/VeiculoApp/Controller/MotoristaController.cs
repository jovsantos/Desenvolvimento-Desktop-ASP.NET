using VeiculoApp.Models;
using VeiculoApp.Data;
using Microsoft.Extensions.Logging.EventLog;

namespace VeiculoApp.Controller
{
    public class MotoristaController
    {
        private readonly AppDbContext _context;
        public MotoristaController(AppDbContext context)
        {
            _context = context;
        }

        public void AdicionarMotorista()
        {
            #region Pedir dados do motorista e criar objeto Motorista
            Console.Clear();
            Console.WriteLine("=== Adicionar Motorista ===");

            Console.WriteLine("Digite o nome do motorista:  ");
            string nome = Console.ReadLine() ?? "";

            Console.WriteLine("Digite o CPF do motorista:  ");
            string cpf = Console.ReadLine() ?? "";

            var motorista = new Motorista
            {
                Nome = nome,
                Cpf = cpf
            };

            _context.Motoristas.Add(motorista);
            _context.SaveChanges();

            Console.WriteLine("Motorista adicionado com sucesso!");

            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void ListarMotorista()
        {
            #region Listar motoristas cadastrados e verificar se há motoristas na lista
            Console.Clear();
            Console.WriteLine("=== Lista de Motoristas ===");
            var motoristas = _context.Motoristas.ToList();
            if (!motoristas.Any())
            {
                Console.WriteLine("Nenhum motorista cadastrado! ");
            }
            else
            {
                foreach (var motorista in motoristas)
                {
                    Console.WriteLine($"ID: {motorista.Id} | Nome: {motorista.Nome} | CPF: {motorista.Cpf}");
                }

            }
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    } 
}
