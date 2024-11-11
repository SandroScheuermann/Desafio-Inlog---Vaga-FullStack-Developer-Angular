using Inlog.Desafio.Backend.Application.Queries;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class ObterHistoricoTelemetriaQueryHandler : IRequestHandler<ObterHistoricoTelemetriaQuery, Result<ObterHistoricoTelemetriaResponse, Error>>
    {
        public ITelemetriaRepository TelemetriaRepository { get; set; }
        public ITelemetriaHistoricoRepository TelemetriaHistoricoRepository { get; set; }

        public ObterHistoricoTelemetriaQueryHandler(ITelemetriaRepository telemetriaRepository, ITelemetriaHistoricoRepository telemetriaHistoricoRepository)
        { 
            TelemetriaRepository = telemetriaRepository;
            TelemetriaHistoricoRepository = telemetriaHistoricoRepository;
        }

        public async Task<Result<ObterHistoricoTelemetriaResponse, Error>> Handle(ObterHistoricoTelemetriaQuery query, CancellationToken cancellationToken)
        {
            var response = await ObterHistoricoTelemetria(query);

            return response;
        } 

        private async Task<Result<ObterHistoricoTelemetriaResponse, Error>> ObterHistoricoTelemetria(ObterHistoricoTelemetriaQuery query)
        {
            var idVeiculo = query.Request.IdVeiculo; 

            var historicoTelemetria = await TelemetriaHistoricoRepository.ObterHistoricoPorIdVeiculoAsync(idVeiculo);

            var resposta = new ObterHistoricoTelemetriaResponse
            { 
                HistoricoPosicao = historicoTelemetria
            };

            return resposta; 
        }
    }
}
