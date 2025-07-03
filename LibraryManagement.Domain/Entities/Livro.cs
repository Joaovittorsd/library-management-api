namespace LibraryManagement.Domain.Entities;

public class Livro
{
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int AnoPublicacao { get; private set; }
    public int QuantidadeDisponivel { get; private set; }

    protected Livro() { }

    public Livro(string titulo, string autor, int anoPublicacao, int quantidadeDisponivel)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentNullException(nameof(titulo), "O título do livro é obrigatório.");
        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentNullException(nameof(autor), "O autor do livro é obrigatório.");
        if (quantidadeDisponivel < 0)
            throw new ArgumentException("A quantidade não pode ser negativa.");

        Titulo = titulo;
        Autor = autor;
        AnoPublicacao = anoPublicacao;
        QuantidadeDisponivel = quantidadeDisponivel;
    }

    public void ReduzirQuantidade()
    {
        if (QuantidadeDisponivel <= 0)
            throw new InvalidOperationException("Não há exemplares disponíveis.");

        QuantidadeDisponivel--;
    }

    public void AumentarQuantidade()
    {
        QuantidadeDisponivel++;
    }
}
