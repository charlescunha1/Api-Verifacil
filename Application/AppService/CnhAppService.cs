using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class CnhAppService : ICnhAppService
{
    public CnhResponseViewModel ValidarCnh(CnhRequestViewModel request)
    {
        var cnh = request.Cnh;
        ValidarTamanhoCnh(cnh);
        ValidarCnhComLetras(cnh);

        if (cnh.Distinct().Count() == 1)
        {
            throw CnhException.CnhInvalida();
        }

        int[] multiplicadores1 = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
        int[] multiplicadores2 = { 1, 0, 9, 8, 7, 6, 5, 4, 3, 2 };

        string cnhBase = cnh.Substring(0, 9);
        int d1 = CalcularDigito(cnhBase, multiplicadores1);
        int d2 = CalcularDigito(cnhBase + d1, multiplicadores2);

        if (cnh.EndsWith($"{d1}{d2}"))
        {
            return new CnhResponseViewModel { Mensagem = "CNH válida" };
        }
        throw CnhException.CnhInvalida();
    }

    private static int CalcularDigito(string baseCnh, int[] multiplicadores)
    {
        int soma = 0;
        for (int i = 0; i < baseCnh.Length; i++)
        {
            soma += (baseCnh[i] - '0') * multiplicadores[i];
        }

        int resto = soma % 11;
        return resto >= 10 ? 0 : resto;
    }

    private void ValidarTamanhoCnh(string cnh)
    {
        if (cnh.Length != 11 || string.IsNullOrWhiteSpace(cnh))
        {
            throw CnhException.TamnahoCnh();
        }
    }

    private void ValidarCnhComLetras(string cnh)
    {
        if (!cnh.All(char.IsDigit))
        {
            throw CnhException.CnhComLetras();
        }
    }
}