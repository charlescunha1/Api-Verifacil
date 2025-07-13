using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface ICnhAppService
{
    CnhResponseViewModel ValidarCnh(CnhRequestViewModel request);
}