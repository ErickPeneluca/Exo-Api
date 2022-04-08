using ExoApi.Models;
using ExoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExoApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ProjetoController : Controller
{
    private readonly ExoRepository _exoRepository;

    public ProjetoController(ExoRepository exoRepository)
    {
        _exoRepository = exoRepository;
    }
    /// <summary>
    /// Essa requisicao lista todos os projetos da emrpesa
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_exoRepository.Listar());
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    /// <summary>
    /// ESsa requisicao busca um projeto especifico de acordo com seu Id definido nos parametros
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        try
        {
            Projeto projetoBuscado = _exoRepository.BuscarPorId(id);

            if (projetoBuscado == null)
            {
                return NotFound();
            }

            return Ok(projetoBuscado);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
/// <summary>
/// Essa requisicao adiciona ao sistema mais um projeto 
/// </summary>
/// <param name="projeto"></param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
    [HttpPost]
    public IActionResult Criar(Projeto projeto)
    {
        try
        {
            _exoRepository.Criar(projeto);

            return Ok("Projeto criado!");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
/// <summary>
/// Esta requisicao altera um projeto no sistema de acordo com seu id passado nos parametros
/// </summary>
/// <param name="id"></param>
/// <param name="projeto"></param>
/// <returns></returns>
/// <exception cref="Exception"></exception>
    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Projeto projeto)
    {
        try
        {
            _exoRepository.Atualizar(id, projeto);

            return Ok("Projeto atualizado!");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    /// <summary>
    /// Essa requisicao deleta o projeto de acordo com o ID escolhido nos parametros
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        try
        {
            _exoRepository.Deletar(id);

            return Ok("Projeto deletado!");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}