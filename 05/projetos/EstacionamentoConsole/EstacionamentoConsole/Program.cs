using EstacionamentoConsole.Controllers;
using EstacionamentoConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<EstacionamentoDbContext>(opt =>
            opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EstacionamentoDB;Trusted_Connection=True;TrustServerCertificate=True;"));

        services.AddTransient<ClienteController>();
    })
    .Build();

var clientesController = host.Services.GetRequiredService<ClienteController>();
bool sair = false;

while (!sair)
{
    Console.Clear();
    Console.WriteLine("===== Sistema de Estacionamento =====");
    Console.WriteLine("1. Gerenciar Clientes");
    Console.WriteLine("2. Gerenciar Vagas");
    Console.WriteLine("3. (A FAZER) Gerenciar Veículos");
    Console.WriteLine("4. (A FAZER) Gerenciar Registros");
    Console.WriteLine("0. Sair");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            MenuClientes();
            break;
        case "2":
            MenuVagas();
            break;
        case "3":
            Console.WriteLine("Menu Veículos");
            break;
        case "4":
            Console.WriteLine("Menu Registros");
            break;
        case "0":
            sair = true;
            break;
        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            Console.ReadKey();
            break;
    }
}


static void MenuClientes()
{
    Console.Clear();
    Console.WriteLine("===== Gerenciamento de Clientes =====");
    Console.WriteLine("1. Listar Clientes");
    Console.WriteLine("2. Adicionar Clientes");
    Console.WriteLine("3. Ver detalhes do Cliente");
    Console.WriteLine("4. Atualizar Cliente");
    Console.WriteLine("5. Remover Cliente");
    Console.WriteLine("0. Sair");

    Console.ReadKey();
}

static void MenuVagas()
{
    Console.Clear();
    Console.WriteLine("Chamou o menu de Vagas");
    Console.WriteLine("Pressione qualquer tecla para retornar");
    Console.ReadKey();
}

Console.WriteLine("Encerrando o sistema. Até logo!");