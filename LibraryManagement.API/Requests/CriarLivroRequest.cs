using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.API.Requests;

public record CriarLivroRequest(
    [Required, MaxLength(200)] string Titulo,
    [Required, MaxLength(200)] string Autor,
    [Range(1, 2100)] int AnoPublicacao,
    [Range(0, int.MaxValue)] int QuantidadeDisponivel
);