namespace LogisticaPoc.Shared.Dtos;

public record DashboardDto(
    int TotalEntregas,
    int Pendentes,
    int EmTransito,
    int Concluidas,
    int Atrasadas,
    int Canceladas,
    int TotalMotoristas,
    int MotoristasDisponiveis,
    int TotalVeiculos,
    int VeiculosDisponiveis,
    IEnumerable<EntregaDto> EntregasRecentes
);
