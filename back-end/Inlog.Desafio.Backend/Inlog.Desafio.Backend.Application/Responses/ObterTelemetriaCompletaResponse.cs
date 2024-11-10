using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Responses
{
    public class ObterTelemetriaCompletaResponse
    {
        public required TelemetriaEntity UltimaPosicao { get; set; }
        public IEnumerable<TelemetriaHistoricoEntity>? HistoricoPosicao { get; set; }
    }
}
