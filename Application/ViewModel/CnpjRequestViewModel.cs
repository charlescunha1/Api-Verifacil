using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.ViewModel;

public class CnpjRequestViewModel
{
    public required string Cnpj { get; set; }
    public FormatoCnpj FormatoCnpj { get; set; }
}