namespace Inlog.Desafio.Backend.Domain.Models
{
    public class Telemetria
    {
        public int Id { get; set; }
        public required int IdVeiculo { get; set; }
        public DateTime DataHora { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
