using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class TelemetriaHistoricoRepository : BaseRepository<TelemetriaHistoricoEntity>, ITelemetriaHistoricoRepository
    {
        public TelemetriaHistoricoRepository(IOptions<DefaultSettings> settings) : base(settings)
        {
        }
        public async Task<IEnumerable<TelemetriaHistoricoEntity>> ObterHistoricoPorVeiculoId(string veiculoId)
        {
            var filter = Builders<TelemetriaHistoricoEntity>.Filter.Eq(telemetria => telemetria.VeiculoId, veiculoId);

            return await Collection
                .Find(filter)
                .SortBy(telemetria => telemetria.DataHora)
                .ToListAsync();
        }
    }
}
