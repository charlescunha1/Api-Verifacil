using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class EmailAppService : IEmailAppService
{
    public EmailResponseViewModel ValidarEmail(EmailRequestViewModel request)
    {
        ValidarNulidadeEmail(request.Email);
        var emailSemEspacos = RetirarEspacosEmBrancoDoEmail(request.Email);
        if (!EmailEhValido(emailSemEspacos))
        {
            throw EmailExceptions.EmailFormatoInvalido();
        }
        return new EmailResponseViewModel(emailSemEspacos);
    }

    private bool EmailEhValido(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw EmailExceptions.EmailVazioOuNulo();
        }

        try
        {
            // Normaliza o domínio do email
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Retorna verdadeiro se o email é válido e o domínio foi normalizado corretamente
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            throw EmailExceptions.EmailFormatoInvalido();
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
            throw EmailExceptions.EmailFormatoInvalido();
        }
        return match.Groups[1].Value + domainName;
    }

    private string RetirarEspacosEmBrancoDoEmail(string email)
    {
        return Regex.Replace(email, @"\s+", "");
    }

    private void ValidarNulidadeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw EmailExceptions.EmailVazioOuNulo();
        }
    }
}