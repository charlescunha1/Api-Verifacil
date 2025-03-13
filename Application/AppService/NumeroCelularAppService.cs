using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class NumeroCelularAppService : INumeroCelularAppService
{
    public CelularResponseViewModel ValidarNumeroCelular(CelularRequestViewModel request)
    {
        ValidarNulidadeCelular(request.NumeroCelular);
        ValidarExistenciaDeLetras(request.NumeroCelular);
        ValidarTamahoNumeroCelular(request.NumeroCelular);
        return new CelularResponseViewModel(request.NumeroCelular);
    }

    private void ValidarTamahoNumeroCelular(string numero)
    {
        numero = RemoverCaracteresEspeciais(numero);

        if (numero.Length != 11)
        {
            throw NumeroCelularExceptions.NumeroCelularTamanhoInvalido();
        }
    }
    private string RemoverCaracteresEspeciais(string numero)
    {
        return Regex.Replace(numero, @"[^\d]", "");
    }

    private void ValidarExistenciaDeLetras(string numero)
    {
        if (Regex.IsMatch(numero, @"[a-zA-Z]"))
        {
            throw NumeroCelularExceptions.NumeroCelularComLetras();
        }
    }

    private void ValidarNulidadeCelular(string celular)
    {
        if (string.IsNullOrEmpty(celular))
        {
            throw NumeroCelularExceptions.NumeroCelularVazioOuNulo();
        }
    }
}