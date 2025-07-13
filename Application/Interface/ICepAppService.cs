using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface ICepAppService
{
   Task<CepResponseViewModel> ValidarCep(CepRequestViewModel request);
}