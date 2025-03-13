using VeriFacil.Domain.Enum;

namespace VeriFacil.Application.ViewModel;

public class CpfRequestViewModel
{
    public required string Cpf { get; set; }
    public FormatoCpf FormatoCpf { get; set; }
}