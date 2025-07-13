namespace VeriFacil.Service.Exceptions;

public class RgException(string message) : Exception(message)
{
    public static RgException RgInvalido() => new("RG inválido.");
}