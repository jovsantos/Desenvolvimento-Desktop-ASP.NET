using CadastroDeUsuario.Data;
using CadastroDeUsuario.Models;

namespace CadastroDeUsuario.Controller
{
    internal class UsuarioController
    {
       private AppDbContext _context;
       public UsuarioController(AppDbContext context)
       {
            _context = context;
       }
       public void Adicionar()
       {
            #region Pedir dados
            Console.WriteLine("Primeiro Nome: ");
            string primeiroNome = Console.ReadLine();

            Console.WriteLine("Sobrenome: ");
            string sobrenome = Console.ReadLine();

            Console.WriteLine("Data de Nascimento: ");
            DateOnly nascimento = DateOnly.Parse(Console.ReadLine());
            #endregion

            var novousuario = new Usuario()

            {
                DataNascimento = nascimento,
                PrimeiroNome = primeiroNome,
                Sobrenome = sobrenome
            };

            _context.Usuarios.Add(novousuario);
            _context.SaveChanges();

            Console.WriteLine($"Usuário {novousuario.PrimeiroNome} adicionado com sucesso!");
            Console.ReadKey();

       }
        
       public void Listar()
       {
            var usuarios = _context.Usuarios.ToList();
            
            if(!usuarios.Any())
            {
                Console.WriteLine("Nenhum usuário cadastrado!");
                Console.ReadKey();
                return;
            }
            else
            {
                foreach(var usuario in usuarios)
                {
                   Console.WriteLine($"Id: {usuario.Id} | Nome: {usuario.PrimeiroNome} | Sobrenome: {usuario.Sobrenome} | Data de Nascimento: {usuario.DataNascimento}");
                }
            }

            Console.WriteLine("\nPressione qualquer teclas para continuar...");
            Console.ReadKey();
       }
    }
}
