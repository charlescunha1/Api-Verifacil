using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.Interface;

public interface ICnpjAppService
{
    public CnpjResponseViewModel ValidarCnpj(CnpjRequestViewModel request);
}