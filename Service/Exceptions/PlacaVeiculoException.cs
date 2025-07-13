namespace VeriFacil.Service.Exceptions;

public class PlacaVeiculoException : Exception
{
    public PlacaVeiculoException(string message) : base(message) { }

    public static PlacaVeiculoException PlacaInvalida() => new("Placa de veículo inválida.");
    public static PlacaVeiculoException PlacaVaziaOuNula() => new("O campo de placa do veículo não pode estar vazio.");
}