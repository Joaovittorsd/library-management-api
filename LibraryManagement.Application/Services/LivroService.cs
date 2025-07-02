using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;

namespace LibraryManagement.Application.Services;

public class LivroService
{
    private readonly ILivroRepository _livroRepository;

    public LivroService(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<List<Livro>> ListarLivroAsync()
    {
        return await _livroRepository.GetAllAsync();
    }

    public async Task<Livro?> ObterPorIdAsync(int id)
    {
        return await _livroRepository.GetByIdAsync(id);
    }

    public async Task<int> CriarLivroAsync(string titulo, string autor, int ano, int quantidade)
    {
        var livro = new Livro(titulo, autor, ano, quantidade);
        await _livroRepository.AddAsync(livro);
        await _livroRepository.SaveChangesAsync();
        return livro.Id;
    }
}
