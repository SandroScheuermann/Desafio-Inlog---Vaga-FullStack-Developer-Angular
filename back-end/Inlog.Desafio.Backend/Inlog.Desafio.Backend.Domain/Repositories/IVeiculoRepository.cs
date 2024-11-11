using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface IVeiculoRepository
    {
        public Task<int> InserirVeiculoAsync(Veiculo veiculo);
        public Task<IEnumerable<Veiculo>> ObterVeiculos();
        public Task<Veiculo?> ObterVeiculoPorId(int id);
    }
}
