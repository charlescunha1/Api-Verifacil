namespace VeriFacil.Service.Exceptions;

public class NumeroCelularExceptions(string message) : Exception(message)
{
    public static NumeroCelularExceptions NumeroCelularComLetras()
        => new("O número de celular não deve conter letras.");
    public static NumeroCelularExceptions NumeroCelularVazioOuNulo()
        => new("O campo de número de celular está vazio.");
    public static NumeroCelularExceptions NumeroCelularTamanhoInvalido()
        => new("O número de celular deve conter exatamente 11 dígitos.");
    public static NumeroCelularExceptions NumeroCelularInvalido()
        => new("Número de celular inválido.");
}