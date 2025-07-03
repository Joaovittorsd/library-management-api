using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface IEmprestimoRepository
{
    Task<Emprestimo> GetByIdAsync(int id);
    Task<List<Emprestimo>> GetByLivroIdAsync(int livroId);
    Task<Emprestimo> GetEmprestimoAtivoPorLivroIdAsync(int livroId);
    Task<List<Emprestimo>> GetAllAsync();
    Task AddAsync(Emprestimo emprestimo);
    Task SaveChangesAsync();
}
