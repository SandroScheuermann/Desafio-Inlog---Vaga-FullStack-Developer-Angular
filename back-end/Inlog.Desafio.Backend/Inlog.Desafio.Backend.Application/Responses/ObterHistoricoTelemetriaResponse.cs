using Inlog.Desafio.Backend.Domain.Models;

namespace Inlog.Desafio.Backend.Application.Responses
{
    public class ObterHistoricoTelemetriaResponse
    { 
        public IEnumerable<TelemetriaHistorico>? HistoricoPosicao { get; set; }
    }
}
