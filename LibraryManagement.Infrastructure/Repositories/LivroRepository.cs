using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly LibraryDbContext _context;

    public LivroRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Livro?> GetByIdAsync(int id)
    {
        return await _context.Livros.FindAsync(id);
    }

    public async Task<List<Livro>> GetAllAsync()
    {
       return await _context.Livros.ToListAsync();
    }

    public async Task AddAsync(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
