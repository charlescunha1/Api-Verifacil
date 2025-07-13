using Microsoft.AspNetCore.Mvc;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Service.Controllers;

public class AsyncBaseController : ControllerBase
{
    public async Task<IActionResult> ExecutarValidacaoAsync<T>(Func<Task<T>> func)
    {
        try
        {
            var resultado = await func();
            return Ok(resultado);
        }

        catch (CepNaoEncontradoException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (CepExceptions ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { erro = "Ocorreu um erro interno. Tente novamente mais tarde." });
        }
    }
}
