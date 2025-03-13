namespace VeriFacil.Application.ViewModel;

public class CelularResponseViewModel(string celular, string message = "Número de celular válido.")
{
    public string Message { get; set; } = message;
    public string Celular { get; set; } = celular;
}