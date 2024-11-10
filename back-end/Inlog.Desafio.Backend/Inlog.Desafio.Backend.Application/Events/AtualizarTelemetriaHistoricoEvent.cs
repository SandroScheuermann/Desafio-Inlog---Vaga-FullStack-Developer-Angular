using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Events
{
    public class AtualizarTelemetriaHistoricoEvent : IRequest<Result<Task,Error>>
    {
        public required AtualizarTelemetriaHistoricoRequest Request { get; set; }
    }
}
