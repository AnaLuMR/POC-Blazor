namespace LogisticaPoc.Shared.Requests;

public record CriarMotoristaRequest(
    string Nome,
    string CPF,
    string Telefone,
    DateTime DataAdmissao
);
