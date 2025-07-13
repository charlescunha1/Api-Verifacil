namespace VeriFacil.Service.Exceptions;

public class CnhException : Exception
{
    public CnhException(string message) : base(message) { }
    public static CnhException CnhInvalida() => new("CNH inválida.");
    public static CnhException CnhComLetras() => new("Não deve existir letras na CNH.");
    public static CnhException TamnahoCnh() => new("Tamanho de CNH inválido.");
}