namespace VeriFacil.Service.Exceptions;

public class CpfExceptions(string message) : Exception(message)
{
    public static CpfExceptions CpfComLetras()
        => new("O CPF não deve conter letras.");

    public static CpfExceptions CpfVazioOuNulo()
        => new("O campo de CPF estar vazio.");

    public static CpfExceptions CpfTamanhoInvalido()
        => new("O CPF deve conter exatamente 11 dígitos.");

    public static CpfExceptions CpfInvalido()
        => new("CPF inválido.");
}
