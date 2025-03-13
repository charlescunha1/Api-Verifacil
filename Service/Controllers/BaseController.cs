using Microsoft.AspNetCore.Mvc;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Service.Controllers;

public class BaseController : ControllerBase
{
    public IActionResult ExecutarValidacao<T>(Func<T> func)
    {
        try
        {
            var resultado = func();
            return Ok(resultado);
        }

        catch (Exception ex) when (ex is CpfExceptions
                            || ex is EmailExceptions
                            || ex is CnpjExceptions
                            || ex is NumeroCelularExceptions
                            || ex is ArgumentException)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { erro = "Ocorreu um erro interno. Tente novamente mais tarde." });
        }
    }
}