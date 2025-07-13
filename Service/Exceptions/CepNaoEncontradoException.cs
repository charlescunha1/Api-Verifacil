namespace VeriFacil.Service.Exceptions;

public class CepNaoEncontradoException : CepExceptions
{
    public CepNaoEncontradoException() : base("CEP não encontrado na base de dados.") { }
}
