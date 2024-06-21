using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.Interface;

public interface ICnpjAppService
{
    public string ValidarCnpj(string numeroCnpj, FormatoCnpj? formatoCnpj);
}
