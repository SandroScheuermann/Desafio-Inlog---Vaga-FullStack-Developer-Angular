namespace Inlog.Desafio.Backend.Domain.Models;

public class VeiculoEntity : BaseEntity
{
    public required string Chassi { get; set; }
    public required string Placa { get; set; }  
    public TipoVeiculo TipoVeiculo { get; set; }
    public required string Cor { get; set; }  
}
