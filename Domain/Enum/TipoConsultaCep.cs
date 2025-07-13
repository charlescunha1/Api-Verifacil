using System.Text.Json.Serialization;

namespace VeriFacil.Domain.Enum;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoConsultaCep
{
    ValidoSomente = 1,
    Completo = 2
}