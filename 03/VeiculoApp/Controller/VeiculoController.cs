using VeiculoApp.Models;
using VeiculoApp.Data;

namespace VeiculoApp.Controller
{
    public class VeiculoController
    {
        private readonly AppDbContext _context;
        public VeiculoController(AppDbContext context)
        {
            _context = context;
        }
        public void AdicionarVeiculo()
        {
            #region Pedir dados do veículo e criar objeto Veiculo
            Console.Clear();
            Console.WriteLine("=== Adicionar Veículo ===");

            Console.Write("Marca: ");
            string marca = Console.ReadLine() ?? "";

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine() ?? "";
             
            var veiculo = new Veiculo
            {
                Marca = marca,
                Modelo = modelo
            };

            _context.Veiculos.Add(veiculo);
            _context.SaveChanges();

            Console.WriteLine("Veículo adicionado com sucesso!");
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void ListarVeiculo()
        {
            #region Listar veículos cadastrados e verificar se há veículos na lista
            Console.Clear();
            Console.WriteLine("=== Lista de Veículos ===");
            var veiculos = _context.Veiculos.ToList();

            if (!veiculos.Any())
            {
                Console.WriteLine("Nenhum veículo cadastrado.");     
            }
            else
            {
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"ID: {veiculo.Id} | Marca: {veiculo.Marca} | Modelo: {veiculo.Modelo}");
                }
            }
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void DetalhesVeiculo()
        {
            #region Mostrar detalhes de um veículo específico e validar ID
            Console.Clear();
            Console.WriteLine("=== Detalhes do Veículo ===");

            Console.Write("Digite o ID do veículo que deseja ver os detalhes: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\n Id Inválido! ");
            }
            else
            {
                var veiculo = _context.Veiculos.
                    FirstOrDefault(veiculo => veiculo.Id == id);
                if (veiculo == null)
                {
                    Console.WriteLine("\n Veículo não encontrado.");
                }
                else
                {
                    Console.WriteLine("\n === Dados do Veículo ===");
                    Console.WriteLine($"ID: {veiculo.Id}");
                    Console.WriteLine($"Marca: {veiculo.Marca}");
                    Console.WriteLine($"Modelo: {veiculo.Modelo}");
                }
                
            }
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();

        }
        public void AtualizarVeiculo()
        {
            #region Atualizar dados de um veículo específico e validar ID
            Console.Clear();
            Console.WriteLine("=== Atualizar Veículo ===");
            Console.Write("Digite o ID do veículo que deseja atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
              var veiculoParaAtualizar = _context.Veiculos.
              FirstOrDefault (veiculoToUpdate => veiculoToUpdate.Id == id);

                if (veiculoParaAtualizar != null)
                {
                  Console.Write("Nova Marca: ");
                    string novaMarca = Console.ReadLine() ?? "";
                  Console.Write("Novo Modelo: ");
                    string novoModelo = Console.ReadLine() ?? "";

                  veiculoParaAtualizar.Marca = novaMarca;
                  veiculoParaAtualizar.Modelo = novoModelo;

                  Console.WriteLine("Veículo atualizado com sucesso!");
                }
                else
                {
                  Console.WriteLine("Veículo não encontrado.");
                }
                  _context.Veiculos.Update(veiculoParaAtualizar);
                  _context.SaveChanges();
            }
            else
            {
              Console.WriteLine("Id Inválido!");
            }
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        public void DeletarVeiculo()
        {
            #region Deletar veículo específico e validar ID
            Console.Clear();
            Console.WriteLine("=== Deletar Veículo ====");
            Console.WriteLine("Digite o ID do veículo que deseja deletar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var veiculoParaDeletar = _context.Veiculos.FirstOrDefault
                    (veiculoParaDeletar => veiculoParaDeletar.Id == id);

                if (veiculoParaDeletar != null)
                {
                    _context.Veiculos.Remove(veiculoParaDeletar);
                    _context.SaveChanges();
                    Console.WriteLine("Veículo deletado com sucesso!");
                }
                else
                {
                 Console.WriteLine("Veículo não encontrado.");
                }
            }
            else
            {
              Console.WriteLine("Id Inválido!");
            }
            #endregion
            Console.WriteLine("\n Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
