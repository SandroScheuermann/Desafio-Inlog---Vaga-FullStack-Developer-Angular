using Dapper;
using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly string _connectionString;

        public VeiculoRepository(IOptions<DefaultSettings> settings)
        {
            _connectionString = settings.Value.DefaultConnection ?? string.Empty;
        }

        public async Task<int> InserirVeiculoAsync(Veiculo veiculo)
        {
            var sql = @"INSERT INTO Veiculos
                        (Chassi, Placa, IdTipoVeiculo, Cor) 
                        VALUES 
                        (@Chassi, @Placa, @IdTipoVeiculo, @Cor)";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteAsync(sql, new
                {
                    veiculo.Chassi,
                    veiculo.Placa,
                    IdTipoVeiculo = (int)veiculo.TipoVeiculo,
                    veiculo.Cor
                });
            };
        } 
        public async Task<IEnumerable<Veiculo>> ObterVeiculos()
        {
            var sql = @"
            SELECT 
            v.IdTipoVeiculo as TipoVeiculo,
            v.*, 
            t.Id AS IdTelemetria,
            t.*
            FROM Veiculos v 
            RIGHT JOIN Telemetrias t ON t.IdVeiculo = v.Id";

            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QueryAsync<Veiculo, Telemetria, Veiculo>(
                sql,
                (veiculo, telemetria) =>
                {
                    veiculo.UltimaTelemetria = telemetria;
                    return veiculo;
                },
                splitOn: "IdTelemetria"
            );

            return result;
        }

        public async Task<Veiculo?> ObterVeiculoPorId(int id)
        {
            var sql = "SELECT * FROM Veiculos V WHERE V.Id = @Id";

            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstAsync<Veiculo>(sql, new { id });
        }
    }
}
