using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;

namespace LibraryManagement.Application.Services;

public class EmprestimoService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IEmprestimoRepository _emprestimoRepository;

    public EmprestimoService(ILivroRepository livroRepository, IEmprestimoRepository emprestimoRepository)
    {
        _livroRepository = livroRepository;
        _emprestimoRepository = emprestimoRepository;
    }

    public async Task<Emprestimo?> ObterPorIdAsync(int id)
    {
        return await _emprestimoRepository.GetByIdAsync(id);
    }

    public async Task<List<Emprestimo>> ListarEmprestimosAsync()
    {
        return await _emprestimoRepository.GetAllAsync();
    }

    public async Task<Emprestimo> SolicitarEmprestimo(int livroId)
    {
        var livro = await _livroRepository.GetByIdAsync(livroId);

        if (livro is null)
            throw new InvalidOperationException("Livro não encontrado.");

        var emprestimo = new Emprestimo(livroId);
        livro.ReduzirQuantidade();

        await _emprestimoRepository.AddAsync(emprestimo);
        await _livroRepository.SaveChangesAsync();
        await _emprestimoRepository.SaveChangesAsync();

       return emprestimo;
    }

    public async Task RegistrarDevolucao(int livroId)
    {
        var emprestimo = await _emprestimoRepository.GetEmprestimoAtivoPorLivroIdAsync(livroId);

        if (emprestimo is null)
            throw new InvalidOperationException("Não existe nenhum empréstimo pendente para este livro.");

        emprestimo.RegistrarDevolucao();

        var livro = await _livroRepository.GetByIdAsync(livroId);
        livro.AumentarQuantidade();

        await _livroRepository.SaveChangesAsync();
        await _emprestimoRepository.SaveChangesAsync();
    }
}
