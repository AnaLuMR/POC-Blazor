using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Endpoints;

public static class RotasEndpoints
{
    public static IEndpointRouteBuilder MapRotas(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/rotas", async (AppDbContext db) =>
        {
            var rotas = await db.Rotas
                .Include(r => r.Entregas)
                .OrderBy(r => r.Nome)
                .ToListAsync();

            return Results.Ok(rotas.Select(r => new RotaDto(
                Id: r.Id,
                Nome: r.Nome,
                Origem: r.Origem,
                Destino: r.Destino,
                DistanciaKm: r.DistanciaKm,
                TempoPrevistoMinutos: r.TempoPrevistoMinutos,
                TotalEntregas: r.Entregas.Count
            )));
        })
        .WithName("GetRotas")
        .WithTags("Rotas");

        app.MapPost("/api/rotas", async (CriarRotaRequest req, AppDbContext db) =>
        {
            var rota = new RotaEntity
            {
                Nome = req.Nome,
                Origem = req.Origem,
                Destino = req.Destino,
                DistanciaKm = req.DistanciaKm,
                TempoPrevistoMinutos = req.TempoPrevistoMinutos,
            };

            db.Rotas.Add(rota);
            await db.SaveChangesAsync();

            return Results.Created($"/api/rotas/{rota.Id}", new RotaDto(
                rota.Id, rota.Nome, rota.Origem, rota.Destino,
                rota.DistanciaKm, rota.TempoPrevistoMinutos, 0
            ));
        })
        .WithName("CriarRota")
        .WithTags("Rotas");

        return app;
    }
}
