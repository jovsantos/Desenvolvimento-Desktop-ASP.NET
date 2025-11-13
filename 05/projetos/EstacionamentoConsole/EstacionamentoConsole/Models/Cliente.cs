using System;
using System.Collections.Generic;

namespace EstacionamentoConsole.Models;

public partial class Cliente
{
    public Cliente(string nome, string cpf, string? telefone)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
    }

    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string? Telefone { get; set; }

    public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
