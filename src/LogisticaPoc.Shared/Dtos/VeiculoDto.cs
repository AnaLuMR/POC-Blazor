using LogisticaPoc.Shared.Enums;

namespace LogisticaPoc.Shared.Dtos;

public record VeiculoDto(
    int Id,
    string Placa,
    string Modelo,
    int Ano,
    decimal CapacidadeKg,
    StatusVeiculo Status,
    int TotalEntregas
);
