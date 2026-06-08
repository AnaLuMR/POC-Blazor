using LogisticaPoc.Api.Data;
using LogisticaPoc.Api.Endpoints;
using LogisticaPoc.Shared.Enums;
using LogisticaPoc.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Tests;

public class ApiIntegrationTests
{
    private static AppDbContext CreateDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb_" + Guid.NewGuid())
            .Options;
        var db = new AppDbContext(options);
        DataSeeder.Seed(db);
        return db;
    }

    [Fact]
    public async Task GetEntregas_RetornaEntregasSeeded()
    {
        using var db = CreateDb();
        var result = await db.Entregas.Include(e => e.Motorista).Include(e => e.Veiculo).Include(e => e.Rota).ToListAsync();
        Assert.NotEmpty(result);
    }

    [Fact]
    public void DataSeeder_CriaEntregasComCodigos()
    {
        using var db = CreateDb();
        var entregas = db.Entregas.ToList();
        Assert.All(entregas, e => Assert.StartsWith("ENT-", e.Codigo));
    }

    [Fact]
    public async Task CriarEntrega_GeraCodigoSequencial()
    {
        using var db = CreateDb();
        var count = await db.Entregas.CountAsync();
        var entrega = new EntregaEntity
        {
            Codigo = $"ENT-{(count + 1):D3}",
            Destinatario = "Nova Empresa",
            EnderecoDestino = "Rua Nova, 1",
            DataPrevista = DateTime.UtcNow.AddDays(5),
            Status = StatusEntrega.Pendente,
            DataCriacao = DateTime.UtcNow,
        };
        db.Entregas.Add(entrega);
        await db.SaveChangesAsync();

        Assert.Equal($"ENT-{(count + 1):D3}", entrega.Codigo);
        Assert.Equal(count + 1, await db.Entregas.CountAsync());
    }

    [Fact]
    public async Task AtualizarStatus_MudaStatusDaEntrega()
    {
        using var db = CreateDb();
        var entrega = await db.Entregas.FirstAsync();
        entrega.Status = StatusEntrega.EmTransito;
        await db.SaveChangesAsync();

        var updated = await db.Entregas.FindAsync(entrega.Id);
        Assert.Equal(StatusEntrega.EmTransito, updated!.Status);
    }

    [Fact]
    public async Task ConcluirEntrega_RegistraDataConclusao()
    {
        using var db = CreateDb();
        var entrega = await db.Entregas.FirstAsync(e => e.Status == StatusEntrega.EmTransito);
        entrega.Status = StatusEntrega.Concluida;
        entrega.DataConclusao = DateTime.UtcNow;
        await db.SaveChangesAsync();

        var updated = await db.Entregas.FindAsync(entrega.Id);
        Assert.Equal(StatusEntrega.Concluida, updated!.Status);
        Assert.NotNull(updated.DataConclusao);
    }

    [Fact]
    public async Task GetMotoristas_RetornaMotoristasSeeded()
    {
        using var db = CreateDb();
        var motoristas = await db.Motoristas.ToListAsync();
        Assert.Equal(5, motoristas.Count);
    }

    [Fact]
    public async Task CriarMotorista_AdicionaAoDb()
    {
        using var db = CreateDb();
        var antes = await db.Motoristas.CountAsync();
        var motorista = new MotoristaEntity
        {
            Nome = "Teste Silva",
            CPF = "111.222.333-44",
            Telefone = "(11) 91111-2222",
            DataAdmissao = DateTime.UtcNow,
            Disponibilidade = DisponibilidadeMotorista.Disponivel,
        };
        db.Motoristas.Add(motorista);
        await db.SaveChangesAsync();

        Assert.Equal(antes + 1, await db.Motoristas.CountAsync());
    }

    [Fact]
    public async Task GetVeiculos_RetornaVeiculosSeeded()
    {
        using var db = CreateDb();
        var veiculos = await db.Veiculos.ToListAsync();
        Assert.Equal(5, veiculos.Count);
    }

    [Fact]
    public async Task GetRotas_RetornaRotasSeeded()
    {
        using var db = CreateDb();
        var rotas = await db.Rotas.ToListAsync();
        Assert.Equal(3, rotas.Count);
    }

    [Fact]
    public async Task Dashboard_TotaisCorretos()
    {
        using var db = CreateDb();
        var totalEntregas = await db.Entregas.CountAsync();
        var pendentes = await db.Entregas.CountAsync(e => e.Status == StatusEntrega.Pendente);
        var concluidas = await db.Entregas.CountAsync(e => e.Status == StatusEntrega.Concluida);

        Assert.True(totalEntregas > 0);
        Assert.True(pendentes >= 0);
        Assert.True(concluidas >= 0);
        Assert.Equal(totalEntregas, pendentes
            + await db.Entregas.CountAsync(e => e.Status == StatusEntrega.EmTransito)
            + concluidas
            + await db.Entregas.CountAsync(e => e.Status == StatusEntrega.Atrasada)
            + await db.Entregas.CountAsync(e => e.Status == StatusEntrega.Cancelada));
    }

    [Fact]
    public void EntregaMapper_MapeiaToDto()
    {
        using var db = CreateDb();
        var entrega = db.Entregas.Include(e => e.Motorista).First();
        var dto = EntregaMapper.ToDto(entrega);

        Assert.Equal(entrega.Id, dto.Id);
        Assert.Equal(entrega.Codigo, dto.Codigo);
        Assert.Equal(entrega.Status, dto.Status);
        Assert.Equal(entrega.Motorista?.Nome, dto.MotoristaNome);
    }
}
