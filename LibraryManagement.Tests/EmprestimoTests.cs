using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Tests;

public class EmprestimoTests
{
    [Fact]
    public void CriarEmprestimo_DeveTerStatusAtivo()
    {
        var emprestimo = new Emprestimo(1);

        Assert.Equal(StatusEmprestimo.Ativo, emprestimo.Status);
        Assert.NotEqual(default, emprestimo.DataEmprestimo);
    }

    [Fact]
    public void RegistrarDevolucao_DeveAtualizarStatusEData()
    {
        var emprestimo = new Emprestimo(1);

        emprestimo.RegistrarDevolucao();

        Assert.Equal(StatusEmprestimo.Devolvido, emprestimo.Status);
        Assert.NotNull(emprestimo.DataDevolucao);
    }

    [Fact]
    public void RegistrarDevolucao_DuasVezes_DeveLancarExcecao()
    {
        var emprestimo = new Emprestimo(1);
        emprestimo.RegistrarDevolucao();

        var ex = Assert.Throws<InvalidOperationException>(() => emprestimo.RegistrarDevolucao());
        Assert.Equal("Este empréstimo já foi devolvido.", ex.Message);
    }
}
