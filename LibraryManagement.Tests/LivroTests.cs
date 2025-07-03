using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Tests;

public class LivroTests
{
    [Fact]
    public void CriarLivro_SemTitulo_DeveLancarExcecao()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new Livro("", "Autor", 2020, 1));
        Assert.Contains("O título do livro é obrigatório.", ex.Message);
    }

    [Fact]
    public void CriarLivro_SemAutor_DeveLancarExcecao()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => new Livro("Titulo", "", 2020, 1));
        Assert.Contains("O autor do livro é obrigatório.", ex.Message);
    }

    [Fact]
    public void ReduzirQuantidade_DeveDiminuirQuantidadeDisponivel()
    {
        var livro = new Livro("Titulo", "Autor", 2020, 2);

        livro.ReduzirQuantidade();

        Assert.Equal(1, livro.QuantidadeDisponivel);
    }

    [Fact]
    public void ReduzirQuantidade_QuantidadeZero_DeveLancarExcecao()
    {
        var livro = new Livro("Titulo", "Autor", 2020, 0);

        var ex = Assert.Throws<InvalidOperationException>(() => livro.ReduzirQuantidade());
        Assert.Contains("Não há exemplares disponíveis.", ex.Message);
    }

    [Fact]
    public void AumentarQuantidade_DeveIncrementarQuantidade()
    {
        var livro = new Livro("Titulo", "Autor", 2020, 1);

        livro.AumentarQuantidade();

        Assert.Equal(2, livro.QuantidadeDisponivel);
    }
}
