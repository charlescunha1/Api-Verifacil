using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class PlacaVeiculoAppService : IPlacaVeiculoAppService
{
    public PlacaVeiculoResponseViewModel ValidarPlacaVeiculo(PlacaVeiculoRequestViewModel request)
    {
        var placa = request.Placa;

        if (string.IsNullOrWhiteSpace(placa))
        {
            throw PlacaVeiculoException.PlacaVaziaOuNula();
        }

        placa = placa.ToUpper().Replace("-", "").Trim();

        if (ValidarPlacaAntiga(placa) || ValidarPlacaMercosul(placa))
        {
            return new PlacaVeiculoResponseViewModel
            {
                Mensagem = "Placa válida"
            };
        }
        throw PlacaVeiculoException.PlacaInvalida();
    }

    private bool ValidarPlacaAntiga(string placa)
    {
        return Regex.IsMatch(placa, @"^[A-Z]{3}\d{4}$");
    }

    private bool ValidarPlacaMercosul(string placa)
    {
        return Regex.IsMatch(placa, @"^[A-Z]{3}\d[A-Z]\d{2}$");
    }
}