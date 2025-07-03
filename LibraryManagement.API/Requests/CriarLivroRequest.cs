using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Requests;

public record CriarLivroRequest(
    string Titulo,
    string Autor,
    [Range(1, 2100)] int AnoPublicacao,
    [Range(0, int.MaxValue)] int QuantidadeDisponivel
);