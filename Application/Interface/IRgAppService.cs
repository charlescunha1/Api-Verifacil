using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface IRgAppService
{
    RgResponseViewModel ValidarRg(RgRequestViewModel request);
}