using System.Text.Json.Serialization;

namespace VeriFacil.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FormatoCpf
{
    ApenasNumeros = 1,
    FormatoOriginal = 2
}