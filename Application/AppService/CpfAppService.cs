using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class CpfAppService : ICpfAppService
{
    public CpfResponseViewModel ValidarCpf(CpfRequestViewModel request)
    {
        ValidarTamahoCPF(request.Cpf);
        ValidarExistenciaDeLetras(request.Cpf);

        if (request.Cpf.Distinct().Count() == 1)
        {
            throw CpfExceptions.CpfInvalido();
        }

        string cpf = new string(request.Cpf.Where(char.IsDigit).ToArray());

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += int.Parse(cpf[i].ToString()) * (10 - i);
        int rev = 11 - (sum % 11);
        if (rev == 10 || rev == 11) rev = 0;
        if (rev != int.Parse(cpf[9].ToString()))
        {
            throw CpfExceptions.CpfInvalido();
        }

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(cpf[i].ToString()) * (11 - i);
        rev = 11 - (sum % 11);
        if (rev == 10 || rev == 11) rev = 0;
        if (rev != int.Parse(cpf[10].ToString()))
        {
            throw CpfExceptions.CpfInvalido();
        }

        if (request.FormatoCpf == FormatoCpf.FormatoOriginal)
        {
            return new CpfResponseViewModel(MascararCPF(request.Cpf));
        }
        return new CpfResponseViewModel(cpf);
    }

    private void ValidarExistenciaDeLetras(string numero)
    {
        if (Regex.IsMatch(numero, @"[a-zA-Z]"))
        {
            throw CpfExceptions.CpfComLetras();
        }
    }

    private void ValidarTamahoCPF(string numero)
    {
        if (string.IsNullOrEmpty(numero))
        {
            throw CpfExceptions.CpfVazioOuNulo();
        }

        var cpf = RemoverCaracteresEspeciais(numero);

        if (cpf.Length != 11)
        {
            throw CpfExceptions.CpfTamanhoInvalido();
        }
    }

    private string RemoverCaracteresEspeciais(string numero)
    {
        return Regex.Replace(numero, @"\D", "");
    }

    private string MascararCPF(string Cpf)
    {
        return string.Format("{0}.{1}.{2}-{3}",
            Cpf.Substring(0, 3),
            Cpf.Substring(3, 3),
            Cpf.Substring(6, 3),
            Cpf.Substring(9, 2));
    }
}