namespace VeriFacil.Service.Exceptions;

public class EmailExceptions(string message) : Exception(message)
{
    public static EmailExceptions EmailFormatoInvalido()
        => new("Email com formato inválido.");

    public static EmailExceptions EmailVazioOuNulo()
        => new("O campo de email está vazio.");
}