using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Responses
{
    public class ListarVeiculosResponse
    {
        public required IEnumerable<Veiculo> Veiculos { get; set; } 
    }
}
