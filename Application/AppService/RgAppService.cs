using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class RgAppService : IRgAppService
{
    public RgResponseViewModel ValidarRg(RgRequestViewModel request)
    {
        var rg = request.Rg;
        if (string.IsNullOrWhiteSpace(request.Rg))
        {
            throw RgException.RgInvalido();
        }

        rg = rg.Replace(".", "").Replace("-", "").Trim();
        if (!Regex.IsMatch(rg, @"^\d{7,9}$"))
        {
            throw RgException.RgInvalido();
        }

        return new RgResponseViewModel { Mensagem = "RG válido" };
    }
}