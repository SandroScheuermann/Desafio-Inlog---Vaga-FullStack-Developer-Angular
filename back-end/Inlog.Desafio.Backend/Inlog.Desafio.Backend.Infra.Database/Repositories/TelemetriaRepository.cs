using Dapper;
using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class TelemetriaRepository : ITelemetriaRepository
    {
        private readonly string _connectionString;
        public TelemetriaRepository(IOptions<DefaultSettings> settings)
        {
            _connectionString = settings.Value.DefaultConnection ?? string.Empty;
        }

        public async Task<int> DeletarTelemetriaPorId(int id)
        {
            var sql = @"DELETE FROM Telemetrias WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(sql, new { id });
        }

        public async Task<int> InserirTelemetriaAsync(Telemetria telemetria)
        {
            var sql = @"INSERT INTO Telemetrias
                        (IdVeiculo, DataHora, Latitude, Longitude) 
                        VALUES 
                        (@IdVeiculo, @DataHora, @Latitude, @Longitude)";

            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(sql, new
            {
                telemetria.IdVeiculo,
                telemetria.DataHora,
                telemetria.Latitude,
                telemetria.Longitude
            });
        }

        public async Task<Telemetria?> ObterTelemetriaDesatualizadaAsync(int idVeiculo)
        {
            var sql = @"SELECT * FROM Telemetrias WHERE IdVeiculo = @IdVeiculo ORDER BY DataHora ASC";

            using var connection = new SqlConnection(_connectionString);

            var telemetrias = (await connection.QueryAsync<Telemetria>(sql, new { idVeiculo })).ToList();
             
            return telemetrias.Count > 1 ? telemetrias.FirstOrDefault() : null;
        }

        public async Task<Telemetria> ObterUltimaTelemetriaPorIdVeiculoAsync(int idVeiculo)
        {
            var sql = @"SELECT * FROM Telemetrias WHERE IdVeiculo = @Idveiculo ORDER BY DataHora DESC ";

            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstAsync<Telemetria>(sql, new { idVeiculo });
        }

        public Task<Telemetria> ObterUltimaTelemetriaPorVeiculoIdAsync(int idVeiculo)
        {
            throw new NotImplementedException();
        }
    }
}
