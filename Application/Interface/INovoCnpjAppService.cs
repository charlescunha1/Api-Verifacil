using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface INovoCnpjAppService
{
    public CnpjResponseViewModel ValidarNovoCnpj(CnpjRequestViewModel request);
}