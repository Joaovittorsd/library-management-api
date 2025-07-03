using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Repositories;
using Moq;

namespace LibraryManagement.Tests;

public class EmprestimoServiceTests
{
    [Fact]
    public async Task SolicitarEmprestimo_QuantidadeDisponivel_DeveCriarEmprestimo()
    {
        var livro = new Livro("Titulo", "Autor", 2020, 1);

        var livroRepoMock = new Mock<ILivroRepository>();
        livroRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);

        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        var id = await service.SolicitarEmprestimo(1);

        emprestimoRepoMock.Verify(r => r.AddAsync(It.IsAny<Emprestimo>()), Times.Once);
        livroRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        emprestimoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        Assert.Equal(0, livro.QuantidadeDisponivel);
    }

    [Fact]
    public async Task SolicitarEmprestimo_SemExemplaresDisponiveis_DeveLancarExcecao()
    {
        var livro = new Livro("Titulo", "Autor", 2020, 0);

        var livroRepoMock = new Mock<ILivroRepository>();
        livroRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);

        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.SolicitarEmprestimo(1));

        Assert.Equal("Não há exemplares disponíveis.", ex.Message);
    }

    [Fact]
    public async Task SolicitarEmprestimo_LivroNaoEncontrado_DeveLancarExcecao()
    {
        var livroRepoMock = new Mock<ILivroRepository>();
        livroRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Livro)null);

        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.SolicitarEmprestimo(1));

        Assert.Equal("Livro não encontrado.", ex.Message);
    }

    [Fact]
    public async Task RegistrarDevolucao_EmprestimoNaoEncontrado_DeveLancarExcecao()
    {
        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();
        emprestimoRepoMock.Setup(r => r.GetEmprestimoAtivoPorLivroIdAsync(1)).ReturnsAsync((Emprestimo)null);

        var livroRepoMock = new Mock<ILivroRepository>();

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegistrarDevolucao(1));

        Assert.Equal("Não existe nenhum empréstimo pendente para este livro.", ex.Message);
    }

    [Fact]
    public async Task RegistrarDevolucao_EmprestimoJaDevolvido_DeveLancarExcecao()
    {
        var emprestimo = new Emprestimo(1);
        emprestimo.RegistrarDevolucao();

        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();
        emprestimoRepoMock.Setup(r => r.GetEmprestimoAtivoPorLivroIdAsync(1)).ReturnsAsync(emprestimo);

        var livroRepoMock = new Mock<ILivroRepository>();
        livroRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Livro("Titulo", "Autor", 2020, 0));

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegistrarDevolucao(1));

        Assert.Equal("Este empréstimo já foi devolvido.", ex.Message);
    }

    [Fact]
    public async Task RegistrarDevolucao_DeveAtualizarEmprestimoELivro()
    {
        var emprestimo = new Emprestimo(1);

        var livro = new Livro("Titulo", "Autor", 2020, 0);

        var emprestimoRepoMock = new Mock<IEmprestimoRepository>();
        emprestimoRepoMock
             .Setup(r => r.GetEmprestimoAtivoPorLivroIdAsync(1))
             .ReturnsAsync(emprestimo);

        var livroRepoMock = new Mock<ILivroRepository>();
        livroRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);

        var service = new EmprestimoService(livroRepoMock.Object, emprestimoRepoMock.Object);

        await service.RegistrarDevolucao(1);

        Assert.Equal(StatusEmprestimo.Devolvido, emprestimo.Status);
        Assert.Equal(1, livro.QuantidadeDisponivel);

        livroRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        emprestimoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
