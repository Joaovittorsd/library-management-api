﻿using LibraryManagement.API.Requests;
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

    [HttpPost("solicitar")]
    public async Task<ActionResult<Emprestimo>> Solicitar([FromBody] CriarEmprestimoRequest request)
    {
        try
        {
            var emprestimo = await _service.SolicitarEmprestimo(request.LivroId);
            return CreatedAtAction(nameof(ObterPorId), new { id = emprestimo.Id }, emprestimo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpPost("devolucao/{livroId}")]
    public async Task<IActionResult> RegistrarDevolucao(int livroId)
    {
        try
        {
            await _service.RegistrarDevolucao(livroId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Emprestimo>> ObterPorId(int id)
    {
        var emprestimo = await _service.ObterPorIdAsync(id);
        if (emprestimo is null)
            return NotFound();

        return Ok(emprestimo);
    }
}
