using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Shared.Dtos;

public record EntregaDto(
    int Id,
    string Codigo,
    string Destinatario,
    string EnderecoDestino,
    StatusEntrega Status,
    int? MotoristaId,
    string? MotoristaNome,
    int? VeiculoId,
    string? VeiculoPlaca,
    int? RotaId,
    string? RotaNome,
    DateTime DataCriacao,
    DateTime DataPrevista,
    DateTime? DataConclusao,
    string? Observacoes
);
