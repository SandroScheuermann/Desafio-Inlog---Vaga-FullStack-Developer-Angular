using Inlog.Desafio.Backend.Application.Requests;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Events
{
    public class AtualizarTelemetriaHistoricoEvent : INotification
    {
        public required AtualizarTelemetriaHistoricoRequest Request { get; set; }
    }
}
