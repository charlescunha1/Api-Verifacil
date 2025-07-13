using Microsoft.AspNetCore.Mvc;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;

namespace VeriFacil.Service.Controllers;

[Route("api/v1/validar")]
public class ValidacoesController(
    INumeroCelularAppService appService, 
    ICnpjAppService cnpjAppService,
    INovoCnpjAppService novoCnpjAppService,
    ICpfAppService cpfAppService, 
    IEmailAppService emailAppService, 
    ICepAppService cepAppService, 
    IRgAppService rgAppService,
    ICnhAppService cnhAppService,
    IPlacaVeiculoAppService placaVeiculoAppService) : BaseController()
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
    [Route("cnpj-alfanumerico")]
    public IActionResult ValidarNovoCnpj([FromBody] CnpjRequestViewModel request)
    {
        return ExecutarValidacao(() => novoCnpjAppService.ValidarNovoCnpj(request));
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

    [HttpPost("cep")]
    public async Task<IActionResult> ValidarCep([FromBody] CepRequestViewModel request)
    {
        return await ExecutarValidacaoAsync(() => cepAppService.ValidarCep(request));
    }

    [HttpPost]
    [Route("rg")]
    public IActionResult ValidarRg([FromBody] RgRequestViewModel request)
    {
        return ExecutarValidacao(() => rgAppService.ValidarRg(request));
    }

    [HttpPost]
    [Route("cnh")]
    public IActionResult ValidarCnh([FromBody] CnhRequestViewModel request)
    {
        return ExecutarValidacao(() => cnhAppService.ValidarCnh(request));
    }

    [HttpPost]
    [Route("placa-veiculo")]
    public IActionResult ValidarPlacaBVeiculo([FromBody] PlacaVeiculoRequestViewModel request)
    {
        return ExecutarValidacao(() => placaVeiculoAppService.ValidarPlacaVeiculo(request));
    }
}