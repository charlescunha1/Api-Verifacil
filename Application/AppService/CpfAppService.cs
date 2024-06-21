using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.AppService;

public class CpfAppService : ICpfAppService
{
    public string ValidarCpf(string numeroCpf, FormatoCpf? formatoCpf)
    {
        ValidarExistenciadeLetras(numeroCpf);
        if (string.IsNullOrEmpty(numeroCpf))
        {
            throw new Exception("Insira um CPF válido.");
        }

        string cpf = new string(numeroCpf.Where(char.IsDigit).ToArray());
        if (cpf.Length == 11)
        {
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            int rev = 11 - (sum % 11);
            if (rev == 10 || rev == 11) rev = 0;
            if (rev != int.Parse(cpf[9].ToString())) return "CPF inválido";

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            rev = 11 - (sum % 11);
            if (rev == 10 || rev == 11) rev = 0;
            if (rev != int.Parse(cpf[10].ToString())) return "CPF inválido";

            if (formatoCpf == FormatoCpf.FormatoOriginal)
            {
              var numeroComMascara =  FormatarComMascaraCpf(numeroCpf);
                return numeroComMascara;
            }
        }
        return cpf;
    }

    private string FormatarComMascaraCpf(string numeroCpf)
    {
        var baa = ValidarExistenciadeLetras(numeroCpf);
        if (baa.Length != 11)
        {
            throw new ArgumentException("O CPF deve ter exatamente 11 dígitos.");
        }
        // Aplica a formatação do CPF: 000.000.000-00
        return string.Format("{0}.{1}.{2}-{3}",
            baa.Substring(0, 3),  // Primeiros 3 dígitos
            baa.Substring(3, 3),  // Próximos 3 dígitos
            baa.Substring(6, 3),  // Próximos 3 dígitos
            baa.Substring(9, 2)); // Últimos 2 dígitos
    }

    private string ValidarExistenciadeLetras(string numero)
    {
        string apenasDigitos = Regex.Replace(numero, @"[^\d]", "");
        if(apenasDigitos.Any(c => !char.IsDigit(c)))
        {
            throw new Exception("O CPF não deve conter letras.");
        }
        return apenasDigitos;
    }
}
