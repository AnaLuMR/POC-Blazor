using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Requests;

namespace LogisticaPoc.Client.Services;

public interface ILogisticaService
{
    Task<DashboardDto?> GetDashboardAsync();
    Task<IEnumerable<EntregaDto>> GetEntregasAsync();
    Task<EntregaDto?> GetEntregaAsync(int id);
    Task<EntregaDto?> CriarEntregaAsync(CriarEntregaRequest request);
    Task<EntregaDto?> AtualizarStatusAsync(int id, AtualizarStatusEntregaRequest request);
    Task<IEnumerable<MotoristaDto>> GetMotoristasAsync();
    Task<MotoristaDto?> CriarMotoristaAsync(CriarMotoristaRequest request);
    Task<IEnumerable<VeiculoDto>> GetVeiculosAsync();
    Task<VeiculoDto?> CriarVeiculoAsync(CriarVeiculoRequest request);
    Task<IEnumerable<RotaDto>> GetRotasAsync();
    Task<RotaDto?> CriarRotaAsync(CriarRotaRequest request);
}
