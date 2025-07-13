using System.Runtime.ConstrainedExecution;

namespace VeriFacil.Application.ViewModel;

public class CepResponseValidoViewModel
{
    public string Cep { get; set; }
    public string Mensagem { get; set; }

    public CepResponseValidoViewModel(string cep, string mensagem)
    {
        Cep = cep;
        Mensagem = mensagem;
    }
}
