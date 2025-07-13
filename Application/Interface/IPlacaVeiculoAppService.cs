using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface IPlacaVeiculoAppService
{
    PlacaVeiculoResponseViewModel ValidarPlacaVeiculo(PlacaVeiculoRequestViewModel request);
}