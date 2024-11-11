using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Queries
{
    public class ObterHistoricoTelemetriaQuery : IRequest<Result<ObterHistoricoTelemetriaResponse, Error>>
    {
        public required ObterHistoricoTelemetriaRequest Request { get; set; }

    }
}
