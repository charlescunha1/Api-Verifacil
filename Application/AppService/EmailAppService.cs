using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;

namespace VeriFacil.Application.AppService;

public class EmailAppService : IEmailAppService
{
    public string ValidarEmail(string email)
    {
        var emailSemEspacos = LimparEspacosEmBrancoDoEmail(email);
        if (!EmailEhValido(emailSemEspacos))
        {
            throw new ArgumentException("Email com formato inválido.");
        }
        return emailSemEspacos;
    }

    private bool EmailEhValido(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            // Normaliza o domínio do email
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Retorna verdadeiro se o email é válido e o domínio foi normalizado corretamente
            var emailEhValido = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            return emailEhValido;
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private string DomainMapper(Match match)
    {
        // Usa a classe IdnMapping para converter nomes de domínio Unicode em ASCII
        var dominio = new System.Globalization.IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = dominio.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Domínio inválido.");
        }
        var email = match.Groups[1].Value + domainName;
        return email;
    }

    private string LimparEspacosEmBrancoDoEmail(string email)
    {
        var emailSemEspaço = Regex.Replace(email, @"\s+", ""); // Remove todos os espaços em branco
        return emailSemEspaço;
    }
}

