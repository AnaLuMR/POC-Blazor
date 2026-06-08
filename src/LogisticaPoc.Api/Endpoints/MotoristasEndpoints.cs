using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Dtos;
using LogisticaPoc.Shared.Requests;
using LogisticaPoc.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Endpoints;

public static class MotoristasEndpoints
{
    public static IEndpointRouteBuilder MapMotoristas(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/motoristas", async (AppDbContext db) =>
        {
            var motoristas = await db.Motoristas
                .Include(m => m.Entregas)
                .OrderBy(m => m.Nome)
                .ToListAsync();

            return Results.Ok(motoristas.Select(m => new MotoristaDto(
                Id: m.Id,
                Nome: m.Nome,
                CPF: m.CPF,
                Telefone: m.Telefone,
                Disponibilidade: m.Disponibilidade,
                DataAdmissao: m.DataAdmissao,
                TotalEntregas: m.Entregas.Count
            )));
        })
        .WithName("GetMotoristas")
        .WithTags("Motoristas");

        app.MapPost("/api/motoristas", async (CriarMotoristaRequest req, AppDbContext db) =>
        {
            var motorista = new MotoristaEntity
            {
                Nome = req.Nome,
                CPF = req.CPF,
                Telefone = req.Telefone,
                DataAdmissao = req.DataAdmissao,
                Disponibilidade = DisponibilidadeMotorista.Disponivel,
            };

            db.Motoristas.Add(motorista);
            await db.SaveChangesAsync();

            return Results.Created($"/api/motoristas/{motorista.Id}", new MotoristaDto(
                motorista.Id, motorista.Nome, motorista.CPF, motorista.Telefone,
                motorista.Disponibilidade, motorista.DataAdmissao, 0
            ));
        })
        .WithName("CriarMotorista")
        .WithTags("Motoristas");

        return app;
    }
}
