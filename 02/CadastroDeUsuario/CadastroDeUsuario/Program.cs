using CadastroDeUsuario.Controller;
using CadastroDeUsuario.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options => 
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UsuariosDB;Trusted_Connection=True;"));

        services.AddTransient<UsuarioController>();
    }).Build();

var usuarioController = host.Services.
    GetRequiredService<UsuarioController>();

MenuPrincipal();
void MenuPrincipal()
 {
    bool sair = false;
    while (!sair)
    {
        Console.Clear();
        Console.WriteLine("=== Menu Principal ===");
        Console.WriteLine("1. Gerenciar Usuários");
        Console.WriteLine("0. Sair");

        string opcao = Console.ReadLine();

        if (opcao == "1")
        {
            MenuUsuarios();
        }
        else if (opcao == "0")
        {
            sair = true;
        }
    }
}
          void MenuUsuarios()
          {
              bool voltar = false;
              while (!voltar)
              {
                Console.Clear();
                Console.WriteLine("=== Gerenciar de Usuários ===");
                Console.WriteLine("1. Listar Usuário");
                Console.WriteLine("2. Cadatrar Usuário ");
                Console.WriteLine("0. Voltar");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                  case "1":
                usuarioController.Listar();
                break;
                  case "2":
                usuarioController.Adicionar();
                break;
                  case "0":
                   voltar = true;
                   break;
                }

              }
          }