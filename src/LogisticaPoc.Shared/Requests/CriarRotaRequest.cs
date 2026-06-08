namespace LogisticaPoc.Shared.Requests;

public record CriarRotaRequest(
    string Nome,
    string Origem,
    string Destino,
    double DistanciaKm,
    int TempoPrevistoMinutos
);
