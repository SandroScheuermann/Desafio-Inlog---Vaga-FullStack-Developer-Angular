using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface ITelemetriaHistoricoRepository
    {
        public Task<IEnumerable<TelemetriaHistorico>> ObterHistoricoPorIdVeiculoAsync(int veiculoId); 
        public Task<int> InserirTelemetriaHistoricoAsync(TelemetriaHistorico telemetriaHistorico);
    }
}
