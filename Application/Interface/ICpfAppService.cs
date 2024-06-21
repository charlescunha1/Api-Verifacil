using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.Interface;

public interface ICpfAppService
{
    public string ValidarCpf(string numeroCpf, FormatoCpf? formatoCpf);
}
