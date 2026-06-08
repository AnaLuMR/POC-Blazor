using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Requests;
using LogisticaPoc.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Endpoints;

public static class VeiculosEndpoints
{
    public static IEndpointRouteBuilder MapVeiculos(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos", async (AppDbContext db) =>
        {
            var veiculos = await db.Veiculos
                .Include(v => v.Entregas)
                .OrderBy(v => v.Placa)
                .ToListAsync();

            return Results.Ok(veiculos.Select(v => new VeiculoDto(
                Id: v.Id,
                Placa: v.Placa,
                Modelo: v.Modelo,
                Ano: v.Ano,
                CapacidadeKg: v.CapacidadeKg,
                Status: v.Status,
                TotalEntregas: v.Entregas.Count
            )));
        })
        .WithName("GetVeiculos")
        .WithTags("Veiculos");

        app.MapPost("/api/veiculos", async (CriarVeiculoRequest req, AppDbContext db) =>
        {
            var veiculo = new VeiculoEntity
            {
                Placa = req.Placa.ToUpper(),
                Modelo = req.Modelo,
                Ano = req.Ano,
                CapacidadeKg = req.CapacidadeKg,
                Status = StatusVeiculo.Disponivel,
            };

            db.Veiculos.Add(veiculo);
            await db.SaveChangesAsync();

            return Results.Created($"/api/veiculos/{veiculo.Id}", new VeiculoDto(
                veiculo.Id, veiculo.Placa, veiculo.Modelo, veiculo.Ano,
                veiculo.CapacidadeKg, veiculo.Status, 0
            ));
        })
        .WithName("CriarVeiculo")
        .WithTags("Veiculos");

        return app;
    }
}
