using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Queries
{
    public class ObterTelemetriaCompletaQuery : IRequest<Result<ObterTelemetriaCompletaResponse, Error>>
    {
        public required ObterTelemetriaCompletaRequest Request { get; set; }

    }
}
