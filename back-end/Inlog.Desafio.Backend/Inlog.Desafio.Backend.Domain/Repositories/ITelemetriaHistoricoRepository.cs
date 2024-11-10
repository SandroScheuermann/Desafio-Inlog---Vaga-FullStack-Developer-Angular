using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface ITelemetriaHistoricoRepository : IBaseRepository<TelemetriaHistoricoEntity>
    {
        public Task<IEnumerable<TelemetriaHistoricoEntity>> ObterHistoricoPorVeiculoId(string veiculoId);
    }
}
