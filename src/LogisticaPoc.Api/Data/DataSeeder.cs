using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Api.Data;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Motoristas.Any()) return;

        var motoristas = new List<MotoristaEntity>
        {
            new() { Nome = "Carlos Eduardo Silva",  CPF = "123.456.789-00", Telefone = "(11) 99001-1111", Disponibilidade = DisponibilidadeMotorista.Disponivel, DataAdmissao = new DateTime(2020, 3, 15) },
            new() { Nome = "Fernanda Lima Rocha",   CPF = "234.567.890-11", Telefone = "(11) 99002-2222", Disponibilidade = DisponibilidadeMotorista.EmRota,     DataAdmissao = new DateTime(2019, 7, 22) },
            new() { Nome = "Roberto Alves Nunes",   CPF = "345.678.901-22", Telefone = "(11) 99003-3333", Disponibilidade = DisponibilidadeMotorista.EmRota,     DataAdmissao = new DateTime(2021, 1, 10) },
            new() { Nome = "Juliana Pereira Costa", CPF = "456.789.012-33", Telefone = "(11) 99004-4444", Disponibilidade = DisponibilidadeMotorista.Folga,      DataAdmissao = new DateTime(2018, 11, 5) },
            new() { Nome = "Marcos Vinícius Souza", CPF = "567.890.123-44", Telefone = "(11) 99005-5555", Disponibilidade = DisponibilidadeMotorista.Disponivel, DataAdmissao = new DateTime(2022, 6, 30) },
        };

        var veiculos = new List<VeiculoEntity>
        {
            new() { Placa = "ABC-1234", Modelo = "Fiat Toro",       Ano = 2022, CapacidadeKg = 1200m,  Status = StatusVeiculo.Disponivel },
            new() { Placa = "DEF-5678", Modelo = "Volkswagen Delivery", Ano = 2021, CapacidadeKg = 3500m, Status = StatusVeiculo.EmRota },
            new() { Placa = "GHI-9012", Modelo = "Mercedes Actros", Ano = 2023, CapacidadeKg = 25000m, Status = StatusVeiculo.EmRota },
            new() { Placa = "JKL-3456", Modelo = "Ford Cargo",      Ano = 2020, CapacidadeKg = 18000m, Status = StatusVeiculo.Manutencao },
            new() { Placa = "MNO-7890", Modelo = "Iveco Daily",     Ano = 2022, CapacidadeKg = 3000m,  Status = StatusVeiculo.Disponivel },
        };

        var rotas = new List<RotaEntity>
        {
            new() { Nome = "Rota Centro-Sul",   Origem = "São Paulo - Centro",    Destino = "São Paulo - Vila Mariana", DistanciaKm = 12.5,  TempoPrevistoMinutos = 40 },
            new() { Nome = "Rota Nordeste",     Origem = "São Paulo - Zona Norte", Destino = "Guarulhos",               DistanciaKm = 28.0,  TempoPrevistoMinutos = 65 },
            new() { Nome = "Rota Interestadual",Origem = "São Paulo - SP",         Destino = "Campinas - SP",           DistanciaKm = 98.0,  TempoPrevistoMinutos = 110 },
        };

        db.Motoristas.AddRange(motoristas);
        db.Veiculos.AddRange(veiculos);
        db.Rotas.AddRange(rotas);
        db.SaveChanges();

        var now = DateTime.UtcNow;
        var entregas = new List<EntregaEntity>
        {
            new() { Codigo = "ENT-001", Destinatario = "Empresa Alpha Ltda",     EnderecoDestino = "Av. Paulista, 1000 - SP",       Status = StatusEntrega.Concluida,  MotoristaId = motoristas[0].Id, VeiculoId = veiculos[0].Id, RotaId = rotas[0].Id, DataCriacao = now.AddDays(-10), DataPrevista = now.AddDays(-8),  DataConclusao = now.AddDays(-8)  },
            new() { Codigo = "ENT-002", Destinatario = "Distribuidora Beta S.A", EnderecoDestino = "Rua Augusta, 500 - SP",          Status = StatusEntrega.Concluida,  MotoristaId = motoristas[1].Id, VeiculoId = veiculos[1].Id, RotaId = rotas[0].Id, DataCriacao = now.AddDays(-9),  DataPrevista = now.AddDays(-7),  DataConclusao = now.AddDays(-7)  },
            new() { Codigo = "ENT-003", Destinatario = "Mercado Gamma",          EnderecoDestino = "Rua da Consolação, 200 - SP",    Status = StatusEntrega.EmTransito, MotoristaId = motoristas[1].Id, VeiculoId = veiculos[1].Id, RotaId = rotas[1].Id, DataCriacao = now.AddDays(-2),  DataPrevista = now.AddDays(1)   },
            new() { Codigo = "ENT-004", Destinatario = "Farmácia Delta",         EnderecoDestino = "Al. Santos, 800 - SP",           Status = StatusEntrega.EmTransito, MotoristaId = motoristas[2].Id, VeiculoId = veiculos[2].Id, RotaId = rotas[2].Id, DataCriacao = now.AddDays(-1),  DataPrevista = now.AddDays(2)   },
            new() { Codigo = "ENT-005", Destinatario = "Tech Epsilon ME",        EnderecoDestino = "Rua Bela Cintra, 400 - SP",      Status = StatusEntrega.Pendente,   DataCriacao = now.AddDays(-1),  DataPrevista = now.AddDays(3)  },
            new() { Codigo = "ENT-006", Destinatario = "Restaurante Zeta",       EnderecoDestino = "Av. Brigadeiro Faria Lima - SP", Status = StatusEntrega.Pendente,   RotaId = rotas[0].Id,           DataCriacao = now,              DataPrevista = now.AddDays(2)  },
            new() { Codigo = "ENT-007", Destinatario = "Supermercado Eta",       EnderecoDestino = "Av. João Dias, 2000 - SP",       Status = StatusEntrega.Atrasada,   MotoristaId = motoristas[0].Id, VeiculoId = veiculos[4].Id,     DataCriacao = now.AddDays(-5),  DataPrevista = now.AddDays(-2), Observacoes = "Aguardando reagendamento" },
            new() { Codigo = "ENT-008", Destinatario = "Construtora Theta",      EnderecoDestino = "Av. Marginal Tietê, 500 - SP",  Status = StatusEntrega.Atrasada,   DataCriacao = now.AddDays(-7),  DataPrevista = now.AddDays(-3), Observacoes = "Problema no endereço" },
            new() { Codigo = "ENT-009", Destinatario = "Clínica Iota",           EnderecoDestino = "Rua Voluntários da Pátria - SP", Status = StatusEntrega.Cancelada,  DataCriacao = now.AddDays(-3),  DataPrevista = now.AddDays(1),  DataConclusao = now.AddDays(-1), Observacoes = "Cancelado a pedido do cliente" },
            new() { Codigo = "ENT-010", Destinatario = "Academia Kappa",         EnderecoDestino = "Rua Haddock Lobo, 300 - SP",    Status = StatusEntrega.Pendente,   MotoristaId = motoristas[4].Id, DataCriacao = now,              DataPrevista = now.AddDays(4)  },
            new() { Codigo = "ENT-011", Destinatario = "Escola Lambda",          EnderecoDestino = "R. Henrique Schaumann - SP",     Status = StatusEntrega.Pendente,   DataCriacao = now,              DataPrevista = now.AddDays(5)  },
            new() { Codigo = "ENT-012", Destinatario = "Hotel Mu",               EnderecoDestino = "Av. Ipiranga, 1200 - SP",        Status = StatusEntrega.EmTransito, MotoristaId = motoristas[2].Id, VeiculoId = veiculos[2].Id, RotaId = rotas[2].Id, DataCriacao = now.AddDays(-1),  DataPrevista = now.AddDays(1)  },
            new() { Codigo = "ENT-013", Destinatario = "Editora Nu",             EnderecoDestino = "Rua da Mooca, 900 - SP",         Status = StatusEntrega.Concluida,  MotoristaId = motoristas[3].Id, VeiculoId = veiculos[0].Id, RotaId = rotas[1].Id, DataCriacao = now.AddDays(-6),  DataPrevista = now.AddDays(-4),  DataConclusao = now.AddDays(-4)  },
            new() { Codigo = "ENT-014", Destinatario = "Padaria Xi",             EnderecoDestino = "Av. Celso Garcia, 450 - SP",     Status = StatusEntrega.Concluida,  MotoristaId = motoristas[0].Id, VeiculoId = veiculos[4].Id, RotaId = rotas[0].Id, DataCriacao = now.AddDays(-4),  DataPrevista = now.AddDays(-2),  DataConclusao = now.AddDays(-2)  },
            new() { Codigo = "ENT-015", Destinatario = "Automecânica Omicron",   EnderecoDestino = "Av. do Estado, 5000 - SP",       Status = StatusEntrega.Pendente,   RotaId = rotas[1].Id,           DataCriacao = now,              DataPrevista = now.AddDays(6)  },
        };

        db.Entregas.AddRange(entregas);
        db.SaveChanges();
    }
}
