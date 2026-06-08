using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboard(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/dashboard", async (AppDbContext db) =>
        {
            var entregas = await db.Entregas
                .Include(e => e.Motorista)
                .Include(e => e.Veiculo)
                .Include(e => e.Rota)
                .OrderByDescending(e => e.DataCriacao)
                .ToListAsync();

            var motoristas = await db.Motoristas.ToListAsync();
            var veiculos = await db.Veiculos.ToListAsync();

            var recentes = entregas.Take(8).Select(EntregaMapper.ToDto);

            return Results.Ok(new DashboardDto(
                TotalEntregas: entregas.Count,
                Pendentes: entregas.Count(e => e.Status == StatusEntrega.Pendente),
                EmTransito: entregas.Count(e => e.Status == StatusEntrega.EmTransito),
                Concluidas: entregas.Count(e => e.Status == StatusEntrega.Concluida),
                Atrasadas: entregas.Count(e => e.Status == StatusEntrega.Atrasada),
                Canceladas: entregas.Count(e => e.Status == StatusEntrega.Cancelada),
                TotalMotoristas: motoristas.Count,
                MotoristasDisponiveis: motoristas.Count(m => m.Disponibilidade == DisponibilidadeMotorista.Disponivel),
                TotalVeiculos: veiculos.Count,
                VeiculosDisponiveis: veiculos.Count(v => v.Status == StatusVeiculo.Disponivel),
                EntregasRecentes: recentes
            ));
        })
        .WithName("GetDashboard")
        .WithTags("Dashboard");

        return app;
    }
}
