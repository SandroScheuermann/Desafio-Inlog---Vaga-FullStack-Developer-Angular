using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Queries
{
    public class ObterUltimaTelemetriaQuery : IRequest<Result<ObterUltimaTelemetriaResponse, Error>>
    {
        public required ObterUltimaTelemetriaRequest Request { get; set; }

    }
}
