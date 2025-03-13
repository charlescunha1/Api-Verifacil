using Microsoft.AspNetCore.Mvc;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Service.Controllers;

[Route("api/v1/validar")]
public class ValidacoesController(INumeroCelularAppService appService, ICnpjAppService cnpjAppService,
ICpfAppService cpfAppService, IEmailAppService emailAppService) : BaseController()
{
    [HttpPost]
    [Route("celular")]
    public IActionResult ValidarNumeroCelular([FromBody] CelularRequestViewModel request)
    {
        return ExecutarValidacao(() => appService.ValidarNumeroCelular(request));
    }

    [HttpPost]
    [Route("cnpj")]
    public IActionResult ValidarCnpj([FromBody] CnpjRequestViewModel request)
    {
        return ExecutarValidacao(() => cnpjAppService.ValidarCnpj(request));
    }

    [HttpPost]
    [Route("cpf")]
    public IActionResult ValidarCpf([FromBody] CpfRequestViewModel request)
    {
        return ExecutarValidacao(() => cpfAppService.ValidarCpf(request));
    }

    [HttpPost]
    [Route("email")]
    public IActionResult ValidarEmail([FromBody] EmailRequestViewModel request)
    {
        return ExecutarValidacao(() => emailAppService.ValidarEmail(request));
    }
}