using LibraryManagement.API.Requests;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : Controller
{
    private readonly LivroService _service;

    public LivrosController(LivroService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Livro>>> Listar()
    {
        var livros = await _service.ListarLivroAsync();
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Livro>> ObterPorId(int id)
    {
        var livro = await _service.ObterPorIdAsync(id);
        if (livro is null)
            return NotFound();

        return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult<Livro>> Criar([FromBody] CriarLivroRequest request)
    {
        try
        {
            var id = await _service.CriarLivroAsync(
                request.Titulo,
                request.Autor,
                request.AnoPublicacao,
                request.QuantidadeDisponivel
            );

            return CreatedAtAction(nameof(ObterPorId), new { id }, id);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }
}
