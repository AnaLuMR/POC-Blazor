using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Shared.Requests;

public record AtualizarStatusEntregaRequest(StatusEntrega NovoStatus, string? Observacoes);
