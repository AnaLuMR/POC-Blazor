namespace LogisticaPoc.Shared.Dtos;

public record RotaDto(
    int Id,
    string Nome,
    string Origem,
    string Destino,
    double DistanciaKm,
    int TempoPrevistoMinutos,
    int TotalEntregas
);
