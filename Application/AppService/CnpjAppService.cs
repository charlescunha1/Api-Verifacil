using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class CnpjAppService : ICnpjAppService
{
    public CnpjResponseViewModel ValidarCnpj(CnpjRequestViewModel request)
    {
        ValidarExistenciaDeLetras(request.Cnpj);
        ValidarTamahoCnpj(request.Cnpj);

        if (request.Cnpj.Distinct().Count() == 1)
        {
            throw CnpjExceptions.CnpjInvalido();
        }

        string valorLimpo = new(request.Cnpj.Where(char.IsDigit).ToArray());

        string cnpj = valorLimpo.PadLeft(14, '0');
        int sum = 0;
        int weight = 2;

        for (int i = 11; i >= 0; i--)
        {
            sum += int.Parse(cnpj[i].ToString()) * weight;
            weight = weight == 9 ? 2 : weight + 1;
        }

        int mod = sum % 11;
        int digito = mod < 2 ? 0 : 11 - mod;

        if (int.Parse(cnpj[12].ToString()) != digito)
        {
            throw CnpjExceptions.CnpjInvalido();
        }

        sum = 0;
        weight = 2;

        for (int i = 12; i >= 0; i--)
        {
            sum += int.Parse(cnpj[i].ToString()) * weight;
            weight = weight == 9 ? 2 : weight + 1;
        }

        int mod2 = sum % 11;
        int digit2 = mod2 < 2 ? 0 : 11 - mod2;

        if (int.Parse(cnpj[13].ToString()) != digit2)
        {
            throw CnpjExceptions.CnpjInvalido();
        }
        if (request.FormatoCnpj == FormatoCnpj.FormatoOriginal)
        {
            return new CnpjResponseViewModel(FormatarComMascaraCnpj(request.Cnpj));
        }
        return new CnpjResponseViewModel(valorLimpo);
    }

    private void ValidarExistenciaDeLetras(string numero)
    {
        if (Regex.IsMatch(numero, @"[a-zA-Z]"))
        {
            throw CnpjExceptions.CnpjComLetras();
        }
    }

    private void ValidarTamahoCnpj(string numero)
    {
        if (string.IsNullOrEmpty(numero))
        {
            throw CnpjExceptions.CnpjVazioOuNulo();
        }

        var cnpj = RemoverCaracteresEspeciais(numero);

        if (cnpj.Length != 14)
        {
            throw CnpjExceptions.CnpjTamanhoInvalido();
        }
    }
    private string RemoverCaracteresEspeciais(string numero)
    {
        return Regex.Replace(numero, @"\D", "");
    }

    public string FormatarComMascaraCnpj(string numero)
    {
        return string.Format("{0}.{1}.{2}/{3}-{4}",
            numero.Substring(0, 2),
            numero.Substring(2, 3),
            numero.Substring(5, 3),
            numero.Substring(8, 4),
            numero.Substring(12, 2)
        );
    }
}