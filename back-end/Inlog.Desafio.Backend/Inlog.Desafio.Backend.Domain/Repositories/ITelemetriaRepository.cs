using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface ITelemetriaRepository
    {
        public Task<int> InserirTelemetriaAsync(Telemetria telemetria);
        public Task<Telemetria?> ObterTelemetriaDesatualizadaAsync(int idVeiculo);
        public Task<Telemetria> ObterUltimaTelemetriaPorVeiculoIdAsync(int idVeiculo);
        public Task<int> DeletarTelemetriaPorId(int id);
    }
}
