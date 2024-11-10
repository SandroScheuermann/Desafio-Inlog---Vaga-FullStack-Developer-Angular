using Inlog.Desafio.Backend.Application.Queries;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class ObterUltimaTelemetriaQueryHandler : IRequestHandler<ObterUltimaTelemetriaQuery, Result<ObterUltimaTelemetriaResponse, Error>>
    { 
        public ITelemetriaRepository TelemetriaRepository { get; set; }

        public ObterUltimaTelemetriaQueryHandler(ITelemetriaRepository telemetriaRepository)
        { 
            TelemetriaRepository = telemetriaRepository;
        }

        public async Task<Result<ObterUltimaTelemetriaResponse, Error>> Handle(ObterUltimaTelemetriaQuery query, CancellationToken cancellationToken)
        {
            var response = await ObterUltimaTelemetria(query);

            return response;
        }  
        private async Task<Result<ObterUltimaTelemetriaResponse, Error>> ObterUltimaTelemetria(ObterUltimaTelemetriaQuery query)
        {
            var veiculoId = query.Request.VeiculoId;

            var result = await TelemetriaRepository.ObterUltimaTelemetriaPorVeiculoIdAsync(veiculoId);

            var resposta = new ObterUltimaTelemetriaResponse
            {
                Telemetria = result
            };

            return resposta; 
        }
    }
}
