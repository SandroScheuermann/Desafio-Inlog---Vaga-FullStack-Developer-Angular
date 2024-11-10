using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class TelemetriaRepository : BaseRepository<TelemetriaEntity>, ITelemetriaRepository
    {
        public TelemetriaRepository(IOptions<DefaultSettings> settings) : base(settings)
        {
        }

        public async Task<TelemetriaEntity> ObterTelemetriaAnteriorAsync(string ultimoId)
        {
            var filter = Builders<TelemetriaEntity>.Filter.Ne(telemetria => telemetria.Id, ultimoId);
             
            return await Collection
                .Find(filter)
                .SortByDescending(telemetria => telemetria.DataHora)
                .FirstOrDefaultAsync();
        }

        public async Task<TelemetriaEntity> ObterUltimaTelemetriaPorVeiculoIdAsync(string veiculoId)
        {
            var filter = Builders<TelemetriaEntity>.Filter.Eq(telemetria => telemetria.VeiculoId, veiculoId);

            return await Collection
                .Find(filter)
                .SortByDescending(telemetria => telemetria.DataHora)
                .FirstOrDefaultAsync();
        }
    }
}
