using System.Net.Http;
using System.Net.Http.Json;
using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Requests;

namespace LogisticaPoc.Client.Services;

public class LogisticaService(IHttpClientFactory httpClientFactory) : ILogisticaService
{
    private HttpClient Api => httpClientFactory.CreateClient("LogisticaApi");

    public Task<DashboardDto?> GetDashboardAsync()
        => Api.GetFromJsonAsync<DashboardDto>("api/dashboard");

    public async Task<IEnumerable<EntregaDto>> GetEntregasAsync()
        => await Api.GetFromJsonAsync<IEnumerable<EntregaDto>>("api/entregas") ?? [];

    public Task<EntregaDto?> GetEntregaAsync(int id)
        => Api.GetFromJsonAsync<EntregaDto>($"api/entregas/{id}");

    public async Task<EntregaDto?> CriarEntregaAsync(CriarEntregaRequest request)
    {
        var resp = await Api.PostAsJsonAsync("api/entregas", request);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<EntregaDto>();
    }

    public async Task<EntregaDto?> AtualizarStatusAsync(int id, AtualizarStatusEntregaRequest request)
    {
        var resp = await Api.PutAsJsonAsync($"api/entregas/{id}/status", request);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<EntregaDto>();
    }

    public async Task<IEnumerable<MotoristaDto>> GetMotoristasAsync()
        => await Api.GetFromJsonAsync<IEnumerable<MotoristaDto>>("api/motoristas") ?? [];

    public async Task<MotoristaDto?> CriarMotoristaAsync(CriarMotoristaRequest request)
    {
        var resp = await Api.PostAsJsonAsync("api/motoristas", request);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<MotoristaDto>();
    }

    public async Task<IEnumerable<VeiculoDto>> GetVeiculosAsync()
        => await Api.GetFromJsonAsync<IEnumerable<VeiculoDto>>("api/veiculos") ?? [];

    public async Task<VeiculoDto?> CriarVeiculoAsync(CriarVeiculoRequest request)
    {
        var resp = await Api.PostAsJsonAsync("api/veiculos", request);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<VeiculoDto>();
    }

    public async Task<IEnumerable<RotaDto>> GetRotasAsync()
        => await Api.GetFromJsonAsync<IEnumerable<RotaDto>>("api/rotas") ?? [];

    public async Task<RotaDto?> CriarRotaAsync(CriarRotaRequest request)
    {
        var resp = await Api.PostAsJsonAsync("api/rotas", request);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<RotaDto>();
    }
}
