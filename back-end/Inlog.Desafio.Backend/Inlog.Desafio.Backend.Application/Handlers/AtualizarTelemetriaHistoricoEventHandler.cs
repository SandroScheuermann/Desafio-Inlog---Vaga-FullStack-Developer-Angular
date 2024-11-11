using Inlog.Desafio.Backend.Application.Events;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class AtualizarTelemetriaHistoricoEventHandler : INotificationHandler<AtualizarTelemetriaHistoricoEvent>
    {
        public ITelemetriaRepository TelemetriaRepository { get; set; }
        public ITelemetriaHistoricoRepository TelemetriaHistoricoRepository { get; set; }

        public AtualizarTelemetriaHistoricoEventHandler(ITelemetriaRepository telemetriaRepository, ITelemetriaHistoricoRepository telemetriaHistoricoRepository)
        {
            TelemetriaRepository = telemetriaRepository;
            TelemetriaHistoricoRepository = telemetriaHistoricoRepository;
        }

        public async Task Handle(AtualizarTelemetriaHistoricoEvent notification, CancellationToken cancellationToken)
        {
            var telemetriaDesatualizada = await TelemetriaRepository.ObterTelemetriaDesatualizadaAsync(notification.Request.IdVeiculo);

            if (telemetriaDesatualizada == null)
            { 
                return;
            }

            var telemetriaHistorico = new TelemetriaHistorico
            {
                Id = telemetriaDesatualizada.Id,
                IdVeiculo = telemetriaDesatualizada.IdVeiculo,
                Latitude = telemetriaDesatualizada.Latitude,
                Longitude = telemetriaDesatualizada.Longitude,
                DataHora = telemetriaDesatualizada.DataHora,
            };

            await TelemetriaHistoricoRepository.InserirTelemetriaHistoricoAsync(telemetriaHistorico);
            await TelemetriaRepository.DeletarTelemetriaPorId(telemetriaDesatualizada.Id);
        }
    }
}
