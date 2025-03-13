using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface INumeroCelularAppService
{
    public CelularResponseViewModel ValidarNumeroCelular(CelularRequestViewModel request);
}