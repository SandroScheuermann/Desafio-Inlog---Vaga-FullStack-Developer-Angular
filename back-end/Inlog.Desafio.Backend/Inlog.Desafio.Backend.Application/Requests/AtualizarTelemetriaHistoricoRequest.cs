namespace Inlog.Desafio.Backend.Application.Requests
{
    public class AtualizarTelemetriaHistoricoRequest
    { 
        public required int IdUltimaTelemetria { get; set; } 
        public required int IdVeiculo { get; set; }    
        public DateTime DataHora { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
