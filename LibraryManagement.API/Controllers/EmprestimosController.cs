using LibraryManagement.API.Requests;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmprestimosController : Controller
{
    private readonly EmprestimoService _service;

    public EmprestimosController(EmprestimoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Emprestimo>>> Listar()
    {
        var emprestimos = await _service.ListarEmprestimosAsync();
        return Ok(emprestimos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Emprestimo>> ObterPorId(int id)
    {
        var emprestimo = await _service.ObterPorIdAsync(id);
        if (emprestimo is null)
            return NotFound();

        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Solicitar([FromBody] CriarEmprestimoRequest request)
    {
        var id = await _service.SolicitarEmprestimo(request.LivroId);
        return CreatedAtAction(nameof(ObterPorId), new { id }, id);
    }

    [HttpPost("{id}/devolucao")]
    public async Task<IActionResult> RegistrarDevolucao(int id)
    {
        await _service.RegistrarDevolucao(id);
        return NoContent();
    }
}
