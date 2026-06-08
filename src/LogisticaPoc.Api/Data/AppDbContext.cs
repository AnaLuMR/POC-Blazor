using Microsoft.EntityFrameworkCore;

namespace LogisticaPoc.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<EntregaEntity> Entregas => Set<EntregaEntity>();
    public DbSet<MotoristaEntity> Motoristas => Set<MotoristaEntity>();
    public DbSet<VeiculoEntity> Veiculos => Set<VeiculoEntity>();
    public DbSet<RotaEntity> Rotas => Set<RotaEntity>();
}
