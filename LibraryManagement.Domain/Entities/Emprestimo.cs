using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities;

public class Emprestimo
{
    public int Id { get; private set; }
    public int LivroId { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime? DataDevolucao { get; private set; }
    public StatusEmprestimo Status { get; private set; }

    protected Emprestimo() { }

    public Emprestimo(int livroId)
    {
        LivroId = livroId;
        DataEmprestimo = DateTime.UtcNow;
        Status = StatusEmprestimo.Ativo;
    }

    public void RegistrarDevolucao()
    {
        if (Status == StatusEmprestimo.Devolvido)
            throw new InvalidOperationException("Este empréstimo já foi devolvido.");

        Status = StatusEmprestimo.Devolvido;
        DataDevolucao = DateTime.UtcNow;
    }
}
