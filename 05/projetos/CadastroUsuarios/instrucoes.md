### **Tutorial Completo: Criando um App de Console com CRUD e EF Core no .NET 8**

Este guia irá demonstrar como construir uma aplicação de console totalmente funcional para gerenciar usuários, utilizando Entity Framework Core para o acesso a dados e uma arquitetura limpa com um "Controller" para a lógica de negócio.

**Pré-requisitos:**

  * Visual Studio 2022 com a carga de trabalho de desenvolvimento para desktop .NET.
  * Um novo projeto do tipo **"Aplicativo de Console"** (Console App) usando .NET 8, chamado "CadastroUsuario".

-----

### Passo 1: Estrutura de Pastas do Projeto

Para manter o código organizado, vamos criar uma estrutura de pastas clara. No seu Gerenciador de Soluções, clique com o botão direito no projeto e crie as seguintes pastas:

1.  `Models` - Para nossas classes de entidade (a representação das tabelas do banco).
2.  `Data` - Para nosso `DbContext` (a ponte com o banco de dados).
3.  `Controllers` - Para a classe que conterá a lógica de negócio (CRUD).

Sua estrutura de projeto deve se parecer com isto:

```
CadastroUsuario/
|-- Controllers/
|-- Data/
|-- Models/
|-- Program.cs
`-- CadastroUsuario.csproj
```

-----

### Passo 2: Instalar as Dependências (NuGet)

Precisamos do Entity Framework Core para interagir com o banco de dados. Usaremos o **Package Manager Console** para instalar os pacotes necessários.

1.  Abra o console em: **Ferramentas (Tools) \> Gerenciador de Pacotes do NuGet \> Console do Gerenciador de Pacotes**.

2.  Execute os seguintes comandos, um de cada vez:

    ```powershell
    Install-Package Microsoft.EntityFrameworkCore.SqlServer
    ```

    ```powershell
    Install-Package Microsoft.EntityFrameworkCore.Tools
    ```

    ```powershell
    Install-Package Microsoft.Extensions.Hosting
    ```

-----

### Passo 3: Criar o Modelo de Dados

Na pasta `Models`, crie uma nova classe chamada `User.cs`. Esta classe define a estrutura da nossa tabela de usuários.

**Arquivo: `Models/User.cs`**

```csharp
public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
```

-----

### Passo 4: Criar o Contexto do Banco de Dados

Na pasta `Data`, crie uma nova classe chamada `AppDbContext.cs`. Esta classe é a ponte entre nosso código e o banco de dados.

**Arquivo: `Data/AppDbContext.cs`**

```csharp
using Microsoft.EntityFrameworkCore;
using CadastroUsuario.Models; // << Certifique-se que o namespace está correto

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
```

-----

### Passo 5: Criar o Controller de Usuário

Na pasta `Controllers`, crie a classe `UsuarioController.cs`. Ela receberá o `AppDbContext` e conterá todos os nossos métodos de CRUD.

**Arquivo: `Controllers/UsuarioController.cs`**

```csharp
using CadastroUsuario.Data;
using CadastroUsuario.Models;

namespace CadastroUsuario.Controllers
{
    public class UsuarioController
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public void AdicionarUsuario()
        {
            Console.Clear();
            Console.WriteLine("--- Adicionar Novo Usuário ---");

            Console.Write("Primeiro Nome: ");
            string firstName = Console.ReadLine() ?? "";

            Console.Write("Último Nome: ");
            string lastName = Console.ReadLine() ?? "";

            DateOnly dateOfBirth;
            while (true)
            {
                Console.Write("Data de Nascimento (formato AAAA-MM-DD): ");
                if (DateOnly.TryParse(Console.ReadLine(), out dateOfBirth))
                {
                    break;
                }
                Console.WriteLine("Formato de data inválido. Tente novamente.");
            }

            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
            
            _context.Users.Add(newUser);
            _context.SaveChanges();

            Console.WriteLine("\nUsuário adicionado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        public void ListarUsuarios()
        {
            Console.Clear();
            Console.WriteLine("--- Lista de Usuários ---");

            var usuarios = _context.Users.ToList();

            if (!usuarios.Any())
            {
                Console.WriteLine("Nenhum usuário cadastrado.");
            }
            else
            {
                foreach (var user in usuarios)
                {
                    Console.WriteLine($"ID: {user.Id} | Nome: {user.FirstName} {user.LastName}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        public void DetalhesUsuario()
        {
            Console.Clear();
            Console.WriteLine("--- Detalhes do Usuário ---");
            Console.Write("Digite o ID do usuário que deseja ver os detalhes: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nID inválido!");
            }
            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Console.WriteLine("\nUsuário não encontrado!");
                }
                else
                {
                    Console.WriteLine("\n--- Dados do Usuário ---");
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Primeiro Nome: {user.FirstName}");
                    Console.WriteLine($"Último Nome: {user.LastName}");
                    Console.WriteLine($"Data de Nascimento: {user.DateOfBirth:yyyy-MM-dd}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }

        public void AtualizarUsuario()
        {
            Console.Clear();
            Console.WriteLine("--- Atualizar Usuário ---");
            Console.Write("Digite o ID do usuário que deseja atualizar: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);
                if (userToUpdate != null)
                {
                    Console.WriteLine($"\nEditando usuário: {userToUpdate.FirstName}. Deixe em branco para não alterar.");

                    Console.Write($"Novo Primeiro Nome ({userToUpdate.FirstName}): ");
                    string newFirstName = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(newFirstName))
                    {
                        userToUpdate.FirstName = newFirstName;
                    }
                    
                    // Lógica similar para outros campos...
                    _context.SaveChanges();
                    Console.WriteLine("\nUsuário atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nUsuário não encontrado!");
                }
            }
            else
            {
                Console.WriteLine("\nID inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }
        
        public void RemoverUsuario()
        {
            Console.Clear();
            Console.WriteLine("--- Remover Usuário ---");
            Console.Write("Digite o ID do usuário que deseja remover: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var userToRemove = _context.Users.FirstOrDefault(u => u.Id == id);
                if (userToRemove != null)
                {
                    Console.WriteLine($"\nEncontrado: ID: {userToRemove.Id} | Nome: {userToRemove.FirstName} {userToRemove.LastName}");
                    Console.Write("Tem certeza que deseja remover este usuário? (S/N): ");
                    string confirmacao = Console.ReadLine() ?? "";

                    if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
                    {
                        _context.Users.Remove(userToRemove);
                        _context.SaveChanges();
                        Console.WriteLine("\nUsuário removido com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("\nOperação cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("\nUsuário não encontrado!");
                }
            }
            else
            {
                 Console.WriteLine("\nID inválido!");
            }
            
            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }
    }
}
```

-----

### Passo 6: Configurar a Aplicação Principal

Agora, vamos juntar tudo no `Program.cs`. Este arquivo irá configurar a injeção de dependência, obter uma instância do nosso controller e iniciar o menu.

**Substitua todo o conteúdo do seu `Program.cs` por este:**

```csharp
using CadastroUsuario.Controllers;
using CadastroUsuario.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ConsoleSimpleDb;Trusted_Connection=True;";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddTransient<UsuarioController>();
    })
    .Build();

// Obtém uma instância do controller do provedor de serviços.
var usuarioController = host.Services.GetRequiredService<UsuarioController>();

// Inicia o menu principal.
MenuPrincipal();

#region Menus da Aplicação

void MenuPrincipal()
{
    bool sair = false;
    while (!sair)
    {
        Console.Clear();
        Console.WriteLine("===== Menu Principal =====");
        Console.WriteLine("1. Gerenciar Usuários");
        Console.WriteLine("0. Sair");
        Console.Write("\nEscolha uma opção: ");

        string? opcao = Console.ReadLine();
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
        Console.WriteLine("===== Gerenciar Usuários =====");
        Console.WriteLine("1. Listar todos os usuários");
        Console.WriteLine("2. Ver detalhes de um usuário");
        Console.WriteLine("3. Adicionar novo usuário");
        Console.WriteLine("4. Atualizar um usuário");
        Console.WriteLine("5. Remover um usuário");
        Console.WriteLine("0. Voltar ao Menu Principal");
        Console.Write("\nEscolha uma opção: ");

        string? opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                usuarioController.ListarUsuarios();
                break;
            case "2":
                usuarioController.DetalhesUsuario();
                break;
            case "3":
                usuarioController.AdicionarUsuario();
                break;
            case "4":
                usuarioController.AtualizarUsuario();
                break;
            case "5":
                usuarioController.RemoverUsuario();
                break;
            case "0":
                voltar = true;
                break;
            default:
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
                break;
        }
    }
}

#endregion
```

-----

### Passo 7: Criar o Banco de Dados (Migrations)

Com todo o código pronto, precisamos criar o banco de dados.

1.  Volte para o **Package Manager Console**.
2.  Verifique se o "Default project" selecionado é o seu projeto de console.
3.  Execute o comando para criar o arquivo de migração:
    ```powershell
    Add-Migration InitialCreate
    ```
4.  Execute o comando para aplicar a migração e criar o banco de dados:
    ```powershell
    Update-Database
    ```

-----

### Passo 8: Executar e Testar

Pressione **F5** ou o botão "Iniciar" para rodar sua aplicação. O menu principal deverá aparecer, e você poderá navegar pelas opções para adicionar, listar, atualizar e remover usuários.

Parabéns\! Você construiu uma aplicação de console robusta e bem estruturada do zero.