using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Shared.Dtos;

public record MotoristaDto(
    int Id,
    string Nome,
    string CPF,
    string Telefone,
    DisponibilidadeMotorista Disponibilidade,
    DateTime DataAdmissao,
    int TotalEntregas
);
