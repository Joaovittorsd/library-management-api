using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Tests;

public class LivroServiceTests
{
    [Fact]
    public async Task CriarLivro_DeveSalvarLivro()
    {
        var repoMock = new Mock<ILivroRepository>();
        var service = new LivroService(repoMock.Object);

        var id = await service.CriarLivroAsync("Titulo", "Autor", 2020, 2);

        repoMock.Verify(r => r.AddAsync(It.Is<Livro>(
            l => l.Titulo == "Titulo" && l.Autor == "Autor" && l.AnoPublicacao == 2020 && l.QuantidadeDisponivel == 2
        )), Times.Once);

        repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task CriarLivro_TituloVazio_DeveLancarExcecao()
    {
        var repoMock = new Mock<ILivroRepository>();
        var service = new LivroService(repoMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => service.CriarLivroAsync("", "Autor", 2020, 2));
    }

    [Fact]
    public async Task CriarLivro_AutorVazio_DeveLancarExcecao()
    {
        var repoMock = new Mock<ILivroRepository>();
        var service = new LivroService(repoMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => service.CriarLivroAsync("Titulo", "", 2020, 2));
    }


}
