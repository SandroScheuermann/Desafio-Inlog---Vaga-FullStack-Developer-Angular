using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Commands
{
    public class InserirTelemetriaCommand : IRequest<Result<InserirTelemetriaResponse, Error>>
    {
        public required InserirTelemetriaRequest Request { get; set; }
    }
}
