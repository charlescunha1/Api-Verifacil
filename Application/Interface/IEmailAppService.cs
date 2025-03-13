using VeriFacil.Application.ViewModel;

namespace VeriFacil.Application.Interface;

public interface IEmailAppService
{
    public EmailResponseViewModel ValidarEmail(EmailRequestViewModel request);
}