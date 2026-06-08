namespace LogisticaPoc.Shared.Requests;

public record CriarEntregaRequest(
    string Destinatario,
    string EnderecoDestino,
    DateTime DataPrevista,
    int? MotoristaId,
    int? VeiculoId,
    int? RotaId,
    string? Observacoes
);
