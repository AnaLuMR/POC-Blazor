using LogisticaPoc.Api.Data;
using LogisticaPoc.Shared.Requests;
using LogisticaPoc.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Endpoints;

public static class EntregasEndpoints
{
    public static IEndpointRouteBuilder MapEntregas(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/entregas", async (AppDbContext db) =>
        {
            var entregas = await db.Entregas
                .Include(e => e.Motorista)
                .Include(e => e.Veiculo)
                .Include(e => e.Rota)
                .OrderByDescending(e => e.DataCriacao)
                .ToListAsync();

            return Results.Ok(entregas.Select(EntregaMapper.ToDto));
        })
        .WithName("GetEntregas")
        .WithTags("Entregas");

        app.MapGet("/api/entregas/{id:int}", async (int id, AppDbContext db) =>
        {
            var entrega = await db.Entregas
                .Include(e => e.Motorista)
                .Include(e => e.Veiculo)
                .Include(e => e.Rota)
                .FirstOrDefaultAsync(e => e.Id == id);

            return entrega is null ? Results.NotFound() : Results.Ok(EntregaMapper.ToDto(entrega));
        })
        .WithName("GetEntregaById")
        .WithTags("Entregas");

        app.MapPost("/api/entregas", async (CriarEntregaRequest req, AppDbContext db) =>
        {
            var count = await db.Entregas.CountAsync();
            var entrega = new EntregaEntity
            {
                Codigo = $"ENT-{(count + 1):D3}",
                Destinatario = req.Destinatario,
                EnderecoDestino = req.EnderecoDestino,
                DataPrevista = req.DataPrevista,
                MotoristaId = req.MotoristaId,
                VeiculoId = req.VeiculoId,
                RotaId = req.RotaId,
                Observacoes = req.Observacoes,
                Status = StatusEntrega.Pendente,
                DataCriacao = DateTime.UtcNow,
            };

            db.Entregas.Add(entrega);
            await db.SaveChangesAsync();

            await db.Entry(entrega).Reference(e => e.Motorista).LoadAsync();
            await db.Entry(entrega).Reference(e => e.Veiculo).LoadAsync();
            await db.Entry(entrega).Reference(e => e.Rota).LoadAsync();

            return Results.Created($"/api/entregas/{entrega.Id}", EntregaMapper.ToDto(entrega));
        })
        .WithName("CriarEntrega")
        .WithTags("Entregas");

        app.MapPut("/api/entregas/{id:int}/status", async (int id, AtualizarStatusEntregaRequest req, AppDbContext db) =>
        {
            var entrega = await db.Entregas
                .Include(e => e.Motorista)
                .Include(e => e.Veiculo)
                .Include(e => e.Rota)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entrega is null) return Results.NotFound();

            entrega.Status = req.NovoStatus;
            if (req.Observacoes is not null) entrega.Observacoes = req.Observacoes;
            if (req.NovoStatus == StatusEntrega.Concluida) entrega.DataConclusao = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(EntregaMapper.ToDto(entrega));
        })
        .WithName("AtualizarStatusEntrega")
        .WithTags("Entregas");

        return app;
    }
}
