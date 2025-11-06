using System;
using System.Collections.Generic;

namespace EstacionamentoConsole.Models;

public partial class RegistrosEstacionamento
{
    public int Id { get; set; }

    public int VeiculoId { get; set; }

    public int VagaId { get; set; }

    public DateTime DataHoraEntrada { get; set; }

    public DateTime? DataHoraSaida { get; set; }

    public decimal? ValorFinal { get; set; }

    public virtual Vaga Vaga { get; set; } = null!;

    public virtual Veiculo Veiculo { get; set; } = null!;
}
