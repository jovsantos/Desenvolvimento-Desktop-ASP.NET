using EstacionamentoSenac.API.Data;
using EstacionamentoSenac.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EstacionamentoSenac.API.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Aplica as migrations pendentes. Cria o banco se não existir.
            context.Database.Migrate();

            // Verifica se já existem dados no banco
            if (context.Motoristas.Any() || context.Veiculos.Any() || context.Vagas.Any())
            {
                return;   // O banco de dados já foi populado
            }

            var motoristas = new Motorista[]
            {
                new Motorista { Nome = "Ana Silva", Cpf = "111.222.333-44", Telefone = "11 98765-4321" },
                new Motorista { Nome = "Bruno Costa", Cpf = "222.333.444-55", Telefone = "21 91234-5678" },
                new Motorista { Nome = "Carla Dias", Cpf = "333.444.555-66", Telefone = "31 98888-7777" },
                new Motorista { Nome = "Daniel Farias", Cpf = "444.555.666-77", Telefone = "41 99999-8888" },
                new Motorista { Nome = "Elisa Borges", Cpf = "555.666.777-88", Telefone = "51 97654-3210" }
            };
            context.Motoristas.AddRange(motoristas);
            context.SaveChanges();

            var veiculos = new Veiculo[]
            {
                new Veiculo { Placa = "ABC-1234", Modelo = "Onix", Cor = "Preto", MotoristaId = motoristas[0].Id },
                new Veiculo { Placa = "DEF-5678", Modelo = "HB20", Cor = "Branco", MotoristaId = motoristas[1].Id },
                new Veiculo { Placa = "GHI-9012", Modelo = "Kwid", Cor = "Prata", MotoristaId = motoristas[2].Id },
                new Veiculo { Placa = "JKL-3456", Modelo = "Mobi", Cor = "Vermelho", MotoristaId = motoristas[3].Id },
                new Veiculo { Placa = "MNO-7890", Modelo = "Argo", Cor = "Azul", MotoristaId = motoristas[4].Id },
                new Veiculo { Placa = "PQR-1122", Modelo = "Polo", Cor = "Cinza", MotoristaId = motoristas[0].Id }
            };
            context.Veiculos.AddRange(veiculos);
            context.SaveChanges();

            var vagas = new Vaga[]
            {
                new Vaga { Localizacao = "A01", TipoVaga = "Normal" },
                new Vaga { Localizacao = "A02", TipoVaga = "Normal" },
                new Vaga { Localizacao = "B01", TipoVaga = "Grande" },
                new Vaga { Localizacao = "C01", TipoVaga = "Idoso" },
                new Vaga { Localizacao = "D01", TipoVaga = "Deficiente" }
            };
            context.Vagas.AddRange(vagas);
            context.SaveChanges();

            var registros = new RegistroEstacionamento[]
            {
                // Registro pendente
                new RegistroEstacionamento { VeiculoId = veiculos[0].Id, VagaId = vagas[0].Id, DataHoraEntrada = DateTime.Now.AddHours(-2) },
                // Registro concluído
                new RegistroEstacionamento { VeiculoId = veiculos[1].Id, VagaId = vagas[1].Id, DataHoraEntrada = DateTime.Now.AddDays(-1).AddHours(-4), DataHoraSaida = DateTime.Now.AddDays(-1).AddHours(-2), ValorFinal = 20.00m },
                // Registro pendente
                new RegistroEstacionamento { VeiculoId = veiculos[2].Id, VagaId = vagas[2].Id, DataHoraEntrada = DateTime.Now.AddMinutes(-30) },
                // Registro concluído
                new RegistroEstacionamento { VeiculoId = veiculos[3].Id, VagaId = vagas[3].Id, DataHoraEntrada = DateTime.Now.AddHours(-8), DataHoraSaida = DateTime.Now.AddHours(-1), ValorFinal = 70.00m }
            };
            context.RegistrosEstacionamento.AddRange(registros);
            context.SaveChanges();
        }
    }
}
