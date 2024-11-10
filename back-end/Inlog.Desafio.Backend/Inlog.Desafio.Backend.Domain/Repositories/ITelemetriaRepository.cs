using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface ITelemetriaRepository : IBaseRepository<TelemetriaEntity>
    {
        public Task<TelemetriaEntity> ObterTelemetriaAnteriorAsync(string ultimoId);

        public Task<TelemetriaEntity> ObterUltimaTelemetriaPorVeiculoIdAsync(string veiculoId);
    }
}
