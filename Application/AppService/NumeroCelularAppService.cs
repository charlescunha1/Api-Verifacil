using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;

namespace VeriFacil.Application.AppService;

public class NumeroCelularAppService : INumeroCelularAppService
{
    public string ValidarNumeroCelular(string numeroCelular)
    {
        var numeroCelularFormatado = FormatarNumeroCelular(numeroCelular);
        return numeroCelularFormatado;
    }

    private string FormatarNumeroCelular(string numero)
    {
        string apenasDigitos = Regex.Replace(numero, @"[^\d]", "");
        if (apenasDigitos.Length != 11)
        {
            throw new Exception("O número deve conter exatamente 11 dígitos.");
        }
        ValidarExistenciaDeLetras(apenasDigitos);
        return apenasDigitos;
    }

    private void ValidarExistenciaDeLetras(string apenasDigitos)
    {
        if (int.TryParse(apenasDigitos, out int number))
        {
            throw new Exception("O número de celular não deve conter letras.");
        }
    }
}
