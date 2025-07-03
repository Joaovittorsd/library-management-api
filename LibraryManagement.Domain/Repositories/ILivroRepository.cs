using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface ILivroRepository
{
    Task<Livro> GetByIdAsync(int  id);
    Task<List<Livro>> GetAllAsync();
    Task AddAsync(Livro livro);
    Task SaveChangesAsync();
}
