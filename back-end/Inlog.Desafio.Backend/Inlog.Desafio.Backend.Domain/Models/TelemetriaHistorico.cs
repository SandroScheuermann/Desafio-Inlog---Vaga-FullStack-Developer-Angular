namespace Inlog.Desafio.Backend.Domain.Models
{
    public class TelemetriaHistorico 
    {
        public int Id { get; set; }
        public required int IdVeiculo { get; set; }
        public required DateTime DataHora { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
