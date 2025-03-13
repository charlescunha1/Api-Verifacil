namespace VeriFacil.Service.Exceptions;

public class CnpjExceptions(string message) : Exception(message)
{
    public static CnpjExceptions CnpjComLetras()
        => new CnpjExceptions("O CNPJ não deve conter letras.");

    public static CnpjExceptions CnpjVazioOuNulo()
        => new CnpjExceptions("O campo do CNPJ está vazio.");

    public static CnpjExceptions CnpjTamanhoInvalido()
        => new CnpjExceptions("O CNPJ deve conter exatamente 14 dígitos.");

    public static CnpjExceptions CnpjInvalido()
        => new CnpjExceptions("CNPJ inválido.");
}