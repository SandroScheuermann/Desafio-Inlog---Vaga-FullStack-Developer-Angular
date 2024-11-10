using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Requests
{
    public class CadastrarVeiculoRequest
    {
        public required string Chassi { get; set; }
        public required string Placa { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        public required string Cor { get; set; } 
    }
}
