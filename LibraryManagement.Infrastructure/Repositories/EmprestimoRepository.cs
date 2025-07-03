using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly LibraryDbContext _context;

    public EmprestimoRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Emprestimo> GetByIdAsync(int id)
    {
        return await _context.Emprestimos.FindAsync(id);
    }

    public async Task<Emprestimo> GetEmprestimoAtivoPorLivroIdAsync(int livroId)
    {
        return await _context.Emprestimos
            .FirstOrDefaultAsync(e => e.LivroId == livroId && e.Status == StatusEmprestimo.Ativo);
    }


    public async Task<List<Emprestimo>> GetAllAsync()
    {
        return await _context.Emprestimos.ToListAsync();
    }

    public async Task AddAsync(Emprestimo emprestimo)
    {
        await _context.Emprestimos.AddAsync(emprestimo);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
