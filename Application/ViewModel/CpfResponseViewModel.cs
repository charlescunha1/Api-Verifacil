namespace VeriFacil.Application.ViewModel;

public class CpfResponseViewModel(string cpf, string message = "CPF válido.")
{
    public string Message { get; set; } = message;
    public string Cpf { get; set; } = cpf;
}