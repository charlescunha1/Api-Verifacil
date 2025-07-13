using Newtonsoft.Json;

namespace VeriFacil.Application.ViewModel
{
    public class CepResponseViewModel
    {
        public string Cep { get; set; }
        [JsonProperty("localidade")]
        public string? Cidade { get; set; }
        [JsonProperty("uf")]
        public string? Estado { get; set; }
        public string? Bairro { get; set; }
        public string? Logradouro { get; set; }
        public string? Ibge { get; set; }
        public string? Gia { get; set; }
        public string? Ddd { get; set; }
        public string? Siafi { get; set; }
        [JsonProperty("erro")]
        public bool? Erro { get; set; }

        public CepResponseViewModel(string cep, string? cidade = null, string? estado = null, string? bairro = null, string? logradouro = null, string? ibge = null, string? gia = null, string? ddd = null, string? siafi = null, bool? erro = null)
        {
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            Bairro = bairro;
            Logradouro = logradouro;
            Ibge = ibge;
            Gia = gia;
            Ddd = ddd;
            Siafi = siafi;
            Erro = erro;
        }
    }
}