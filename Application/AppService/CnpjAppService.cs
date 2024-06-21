using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.AppService;

public class CnpjAppService : ICnpjAppService
{
    public string ValidarCnpj(string numeroCnpj, FormatoCnpj? tipoCnpj)
    {
        FormatarNumeroCnpj(numeroCnpj);
        if (string.IsNullOrEmpty(numeroCnpj))
        {
            throw new Exception("O campo do CNPJ está vazio.");
        }

        string valorLimpo = new string(numeroCnpj.Where(char.IsDigit).ToArray());

        if (valorLimpo.Length == 14)
        {
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
                throw new Exception("Insira um CNPJ válido.");
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
                throw new Exception("Insira um CNPJ válido.");
            }
            if (tipoCnpj == FormatoCnpj.FormatoOriginal)
            {
               string v = FormatarComMascaraCnpj(numeroCnpj);
                return v;
            }
        }
        return valorLimpo;
    }

    private string FormatarNumeroCnpj(string numero)
    {
       var numeroFormatado = ValidarExistenciaDeLetras(numero).ToString();
        if (numeroFormatado.Length != 14)
        {
            throw new Exception("O CNPJ deve conter exatamente 14 dígitos.");
        }       
        return numeroFormatado;
    }

    public long ValidarExistenciaDeLetras(string numero)
    {
        string apenasDigitos = Regex.Replace(numero, @"[^\d]", "");
        if (!long.TryParse(apenasDigitos, out long number))
        {
            throw new Exception("O CNPJ não deve conter letras.");
        }
        return number;
    }

    public string FormatarComMascaraCnpj(string numero)
    {
        // Aplica a formatação do CNPJ: 00.000.000/0000-00
        return string.Format("{0}.{1}.{2}/{3}-{4}",
            numero.Substring(0, 2),  // Primeiros 2 dígitos
            numero.Substring(2, 3),  // Próximos 3 dígitos
            numero.Substring(5, 3),  // Próximos 3 dígitos
            numero.Substring(8, 4),  // Próximos 4 dígitos
            numero.Substring(12, 2)  // Últimos 2 dígitos
        );
    }
}

