namespace VeriFacil.Service.Exceptions;

public class CepExceptions(string message) : Exception(message)
{
    public static CepExceptions CepInvalido()
        => new("O CEP informado é inválido.");

    public static CepExceptions CepVazioOuNulo()
        => new("O campo do CEP está vazio.");
}