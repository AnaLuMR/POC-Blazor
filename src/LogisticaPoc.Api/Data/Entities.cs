using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Api.Data;

public class EntregaEntity
{
    public int Id { get; set; }
    public string Codigo { get; set; } = "";
    public string Destinatario { get; set; } = "";
    public string EnderecoDestino { get; set; } = "";
    public StatusEntrega Status { get; set; }
    public int? MotoristaId { get; set; }
    public MotoristaEntity? Motorista { get; set; }
    public int? VeiculoId { get; set; }
    public VeiculoEntity? Veiculo { get; set; }
    public int? RotaId { get; set; }
    public RotaEntity? Rota { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataPrevista { get; set; }
    public DateTime? DataConclusao { get; set; }
    public string? Observacoes { get; set; }
}

public class MotoristaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string CPF { get; set; } = "";
    public string Telefone { get; set; } = "";
    public DisponibilidadeMotorista Disponibilidade { get; set; }
    public DateTime DataAdmissao { get; set; }
    public ICollection<EntregaEntity> Entregas { get; set; } = [];
}

public class VeiculoEntity
{
    public int Id { get; set; }
    public string Placa { get; set; } = "";
    public string Modelo { get; set; } = "";
    public int Ano { get; set; }
    public decimal CapacidadeKg { get; set; }
    public StatusVeiculo Status { get; set; }
    public ICollection<EntregaEntity> Entregas { get; set; } = [];
}

public class RotaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Origem { get; set; } = "";
    public string Destino { get; set; } = "";
    public double DistanciaKm { get; set; }
    public int TempoPrevistoMinutos { get; set; }
    public ICollection<EntregaEntity> Entregas { get; set; } = [];
}
