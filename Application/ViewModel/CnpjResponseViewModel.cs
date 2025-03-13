namespace VeriFacil.Application.ViewModel;

public class CnpjResponseViewModel(string cnpj, string message = "CNPJ válido.")
{
    public string Message { get; set; } = message;
    public string Cnpj { get; set; } = cnpj;
}