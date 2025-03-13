namespace VeriFacil.Application.ViewModel;

public class EmailResponseViewModel(string email, string message = "Email válido.")
{
    public string Message { get; set; } = message;
    public string Email { get; set; } = email;
}