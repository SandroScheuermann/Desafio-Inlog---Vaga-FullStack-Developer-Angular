namespace Inlog.Desafio.Backend.Domain.Models;

public class Veiculo
{
    public int Id { get; set; }
    public required string Chassi { get; set; }
    public required string Placa { get; set; }  
    public TipoVeiculo TipoVeiculo { get; set; }
    public required string Cor { get; set; } 
    public Telemetria? UltimaTelemetria { get; set; }
}
