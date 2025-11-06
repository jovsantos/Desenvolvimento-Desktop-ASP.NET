using VeiculoApp.Controller;
using VeiculoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ConsoleSimpleDb;Trusted_Connection=True;"));

        services.AddTransient<VeiculoController>();
    })
    .Build();

var veiculoController = host.Services.GetRequiredService<VeiculoController>();

MenuPrincipal();

void MenuPrincipal()
{
    #region Menu principal com opções para gerenciar veículos ou sair do programa

    bool sair = false;
    while (!sair)
    {
        Console.Clear();
        Console.WriteLine("=== Menu Principal ===");
        Console.WriteLine("1. Gerenciar Veículos");
        Console.WriteLine("0. Sair");
        Console.Write("\n Escolha uma opção: ");

        string? opcao = Console.ReadLine();

        if (opcao == "1")
        {
            MenuVeiculos();
        }
        else if (opcao == "0")
        {
            sair = true;
        }
    }
}
#endregion

void MenuVeiculos()
{
    #region Menu de veículos com opções para adicionar, listar, ver detalhes, atualizar ou deletar veículos

    bool sair = false;
    while (!sair)
    {
        Console.Clear();
        Console.WriteLine("=== Menu Veículos ===");
        Console.WriteLine("1. Adicionar Veículo");
        Console.WriteLine("2. Listar Veículos");
        Console.WriteLine("3. Detalhes do Veículo ");
        Console.WriteLine("4. Deletar Veículo");
        Console.WriteLine("5. Atualizar Veículo");
        Console.WriteLine("0. Voltar ao Menu Principal");
        Console.Write("\n Escolha uma opção: ");

        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                veiculoController.AdicionarVeiculo();
                break;
            case "2":
                veiculoController.ListarVeiculo();
                break;
            case "3":
                veiculoController.DetalhesVeiculo();
                break;
            case "4":
                veiculoController.DeletarVeiculo();
                break;
            case "5":
                veiculoController.AtualizarVeiculo();
                break;
            case "0":
                sair = true;
                break;

            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                Console.WriteLine("\n Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
        }
    }
}
#endregion