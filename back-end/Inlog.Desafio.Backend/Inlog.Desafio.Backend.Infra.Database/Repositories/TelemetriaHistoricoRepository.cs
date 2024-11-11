using Dapper;
using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class TelemetriaHistoricoRepository : ITelemetriaHistoricoRepository
    {
        private readonly string _connectionString;
        public TelemetriaHistoricoRepository(IOptions<DefaultSettings> settings)
        {
            _connectionString = settings.Value.DefaultConnection ?? string.Empty;
        }
        public async Task<int> InserirTelemetriaHistoricoAsync(TelemetriaHistorico telemetriaHistorico)
        {
            var sql = @"INSERT INTO TelemetriasHistorico
                        (IdVeiculo, DataHora, Latitude, Longitude) 
                        VALUES 
                        (@IdVeiculo, @DataHora, @Latitude, @Longitude)";

            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(sql, new
            {
                telemetriaHistorico.IdVeiculo,
                telemetriaHistorico.DataHora,
                telemetriaHistorico.Latitude,
                telemetriaHistorico.Longitude
            });
        }

        public async Task<IEnumerable<TelemetriaHistorico>> ObterHistoricoPorIdVeiculoAsync(int idVeiculo)
        {
            var sql = @"
                        SELECT th.*
                        FROM TelemetriasHistorico th
                        WHERE th.IdVeiculo = @idVeiculo
                        
                        UNION ALL
                        
                        SELECT t.*
                        FROM Telemetrias t
                        WHERE t.IdVeiculo = @idVeiculo
                        
                        ORDER BY DataHora ASC;";

            using var connection = new SqlConnection(_connectionString);

            var telemetrias = await connection.QueryAsync<TelemetriaHistorico>(sql, new { idVeiculo });

            return telemetrias;
        }

    }
}
