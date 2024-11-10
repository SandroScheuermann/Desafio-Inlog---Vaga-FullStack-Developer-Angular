using Inlog.Desafio.Backend.Application.Queries;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class ObterTelemetriaCompletaQueryHandler : IRequestHandler<ObterTelemetriaCompletaQuery, Result<ObterTelemetriaCompletaResponse, Error>>
    {
        public ITelemetriaRepository TelemetriaRepository { get; set; }
        public ITelemetriaHistoricoRepository TelemetriaHistoricoRepository { get; set; }

        public ObterTelemetriaCompletaQueryHandler(ITelemetriaRepository telemetriaRepository, ITelemetriaHistoricoRepository telemetriaHistoricoRepository)
        { 
            TelemetriaRepository = telemetriaRepository;
            TelemetriaHistoricoRepository = telemetriaHistoricoRepository;
        }

        public async Task<Result<ObterTelemetriaCompletaResponse, Error>> Handle(ObterTelemetriaCompletaQuery query, CancellationToken cancellationToken)
        {
            var response = await ObterTelemetriaCompleta(query);

            return response;
        } 

        private async Task<Result<ObterTelemetriaCompletaResponse, Error>> ObterTelemetriaCompleta(ObterTelemetriaCompletaQuery query)
        {
            var veiculoId = query.Request.VeiculoId;

            var ultimaTelemetria = await TelemetriaRepository.ObterUltimaTelemetriaPorVeiculoIdAsync(veiculoId);

            var historicoTelemetria = await TelemetriaHistoricoRepository.ObterHistoricoPorVeiculoId(veiculoId);
              
            var resposta = new ObterTelemetriaCompletaResponse
            { 
                UltimaPosicao = ultimaTelemetria,
                HistoricoPosicao = historicoTelemetria
            };

            return resposta; 
        }
    }
}
