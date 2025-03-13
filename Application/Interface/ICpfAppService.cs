using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.Interface;

public interface ICpfAppService
{
    public CpfResponseViewModel ValidarCpf(CpfRequestViewModel request);
}