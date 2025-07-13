using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using VeriFacil.Application.Interface;
using VeriFacil.Application.ViewModel;
using VeriFacil.Domain.Enum;
using VeriFacil.Service.Exceptions;

namespace VeriFacil.Application.AppService;

public class CepAppService(HttpClient httpClient) : ICepAppService
{
    private readonly HttpClient _httpClient = httpClient;
    static string[] Scopes = { GmailService.Scope.GmailReadonly }; // Ou GmailModify / GmailSend etc.
    static string ApplicationName = "Gmail CSharp App";

    public static async Task<GmailService> GetGmailServiceAsync()
    {
        UserCredential credential;

        using (var stream = new FileStream(@"C:\Users\Usuario\Documents\minha-conta-google\client_secret.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)
            );
        }

        return new GmailService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });
    }







    public async Task<CepResponseViewModel> ValidarCep(CepRequestViewModel request)
    {
        var gmailService = await GetGmailServiceAsync();

        request.Cep = LimparCep(request.Cep);
        ValidarFormatoCep(request.Cep);

        var dadosCep = await BuscarCepNaApi(request.Cep);
        return dadosCep ?? throw CepExceptions.CepInvalido();
    }

    private void ValidarFormatoCep(string cep)
    {
        if (!Regex.IsMatch(cep, @"^\d{8}$"))
        {
            throw CepExceptions.CepInvalido();
        }
    }

    private string LimparCep(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
        {
            throw CepExceptions.CepVazioOuNulo();
        }
        return Regex.Replace(cep, @"[^\d]", "");
    }

    private async Task<CepResponseViewModel?> BuscarCepNaApi(string cep)
    {
        string url = $"https://viacep.com.br/ws/{cep}/json/";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var resultado = JsonConvert.DeserializeObject<CepResponseViewModel>(json);
        if (resultado?.Erro == true)
        {
            throw new CepNaoEncontradoException();
        }

        if (string.IsNullOrWhiteSpace(resultado?.Cidade) && string.IsNullOrWhiteSpace(resultado?.Estado))
        {
            throw new CepNaoEncontradoException();
        }
        return resultado;
    }

    //private void teste()
    //{
    //    string url = $"https://viacep.com.br/ws/{cep}/json/";
    //    var response = await _httpClient.GetAsync(url);

    //    // Método de teste para verificar a funcionalidade do serviço
    //    // Pode ser removido ou adaptado conforme necessário
    //    var request = new CepRequestViewModel { Cep = "01001-000" };
    //    var resultado = ValidarCep(request).GetAwaiter().GetResult();

    //}
    }