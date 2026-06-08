namespace LogisticaPoc.Shared.Requests;

public record CriarVeiculoRequest(
    string Placa,
    string Modelo,
    int Ano,
    decimal CapacidadeKg
);
