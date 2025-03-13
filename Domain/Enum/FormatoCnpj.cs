using System.Text.Json.Serialization;

namespace VeriFacil.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FormatoCnpj
{
    ApenasNumeros = 1,
    FormatoOriginal = 2
}