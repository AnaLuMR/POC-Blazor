using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Dtos;

namespace LogisticaPoc.Api.Endpoints;

public static class EntregaMapper
{
    public static EntregaDto ToDto(EntregaEntity e) => new(
        Id: e.Id,
        Codigo: e.Codigo,
        Destinatario: e.Destinatario,
        EnderecoDestino: e.EnderecoDestino,
        Status: e.Status,
        MotoristaId: e.MotoristaId,
        MotoristaNome: e.Motorista?.Nome,
        VeiculoId: e.VeiculoId,
        VeiculoPlaca: e.Veiculo?.Placa,
        RotaId: e.RotaId,
        RotaNome: e.Rota?.Nome,
        DataCriacao: e.DataCriacao,
        DataPrevista: e.DataPrevista,
        DataConclusao: e.DataConclusao,
        Observacoes: e.Observacoes
    );
}
