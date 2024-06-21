using Microsoft.AspNetCore.Mvc;
using VeriFacil.Application.Interface;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Controllers
{
    [Route("api/Validacoes")]
    public class ValidacoesController : ControllerBase
    {
        private INumeroCelularAppService _appService;
        private ICnpjAppService _cnpjAppService;
        private ICpfAppService _cpfAppService;
        private IEmailAppService _emailAppService;
        public ValidacoesController( ILogger<ValidacoesController> logger,
        INumeroCelularAppService appService, ICnpjAppService cnpjAppService,
        ICpfAppService cpfAppService, IEmailAppService emailAppService) : base( )
        {
            _appService = appService;
            _cnpjAppService = cnpjAppService;
            _cpfAppService = cpfAppService;
            _emailAppService = emailAppService;
        }

        [HttpGet]
        [Route("NumeroCelular")]
        public IActionResult ValidarNumeroCelular([FromQuery] string numeroCelular)
        {
            string numero;
            try
            {
                numero = _appService.ValidarNumeroCelular(numeroCelular);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(numero);
        }

        [HttpGet]
        [Route("Cnpj")]
        public IActionResult ValidarCnpj([FromQuery] string numeroCnpj,[FromQuery] FormatoCnpj? tipoCnpj)
        {
            string numero;
            try
            {
                numero = _cnpjAppService.ValidarCnpj(numeroCnpj, tipoCnpj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(numero);
        }

        [HttpGet]
        [Route("Cpf")]
        public IActionResult ValidarCpf([FromQuery] string numeroCpf, [FromQuery] FormatoCpf? formatoCpf)
        {
            string numero;
            try
            {
                numero = _cpfAppService.ValidarCpf(numeroCpf, formatoCpf);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(numero);
        }

        [HttpGet]
        [Route("Email")]
        public IActionResult ValidarEmail([FromQuery] string email)
        {
            string emailValido;
            try
            {
                emailValido = _emailAppService.ValidarEmail(email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(emailValido);
        }
    }
}
