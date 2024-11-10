using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Responses
{
    public class ListarVeiculosResponse
    {
        public IEnumerable<VeiculoEntity>? Veiculos { get; set; }
    }
}
