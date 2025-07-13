using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class NovoCnpjAppService : INovoCnpjAppService
{
    public CnpjResponseViewModel ValidarNovoCnpj(CnpjRequestViewModel request)
    {
        // 🔄 Novo: Verifica se é um CNPJ alfanumérico
        bool isAlfanumerico = Regex.IsMatch(request.Cnpj, @"^[A-Z0-9]{12}[0-9]{2}$", RegexOptions.IgnoreCase);

        if (!isAlfanumerico)
        {
            ValidarExistenciaDeLetras(request.Cnpj); // Mantida para CNPJ tradicional
        }

        ValidarTamahoCnpj(request.Cnpj);

        if (request.Cnpj.Distinct().Count() == 1)
        {
            throw CnpjExceptions.CnpjInvalido();
        }

        if (isAlfanumerico)
        {
            if (!ValidarCnpjAlfanumerico(request.Cnpj.ToUpper())) // 🔄 Novo método para validar novo formato
            {
                throw CnpjExceptions.CnpjInvalido();
            }

            return new CnpjResponseViewModel(request.Cnpj.ToUpper());
        }
        else
        {
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
        return Regex.Replace(numero, @"\W", ""); // 🔄 Alterado para remover tudo que não é letra ou número
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

    // ✅ NOVO: Validação para CNPJ alfanumérico (aplica módulo 11 usando ASCII - 48)
    private bool ValidarCnpjAlfanumerico(string cnpj)
    {
        if (cnpj.Length != 14)
            return false;

        int[] pesos1 = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] pesos2 = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int SomaComPesos(string entrada, int[] pesos)
        {
            int soma = 0;
            for (int i = 0; i < pesos.Length; i++)
            {
                int valorChar = Char.IsDigit(entrada[i])
                    ? entrada[i] - '0'
                    : entrada[i] - 48; // ASCII - 48 (como definido pela Receita)

                soma += valorChar * pesos[i];
            }
            return soma;
        }

        int CalcularDV(string entrada, int[] pesos)
        {
            int soma = SomaComPesos(entrada, pesos);
            int resto = soma % 11;
            return (resto < 2) ? 0 : 11 - resto;
        }

        string baseCnpj = cnpj.Substring(0, 12);
        int dv1 = CalcularDV(baseCnpj, pesos1);
        int dv2 = CalcularDV(baseCnpj + dv1.ToString(), pesos2);

        return cnpj[12].ToString() == dv1.ToString() && cnpj[13].ToString() == dv2.ToString();
    }
}
