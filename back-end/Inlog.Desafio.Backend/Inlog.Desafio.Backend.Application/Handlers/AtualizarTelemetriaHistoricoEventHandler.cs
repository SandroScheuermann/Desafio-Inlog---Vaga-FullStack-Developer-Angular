using Inlog.Desafio.Backend.Application.Events;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class AtualizarTelemetriaHistoricoEventHandler : IRequestHandler<AtualizarTelemetriaHistoricoEvent, Result<Task, Error>>
    {
        public ITelemetriaRepository TelemetriaRepository { get; set; }
        public ITelemetriaHistoricoRepository TelemetriaHistoricoRepository { get; set; }

        public AtualizarTelemetriaHistoricoEventHandler(ITelemetriaRepository telemetriaRepository, ITelemetriaHistoricoRepository telemetriaHistoricoRepository)
        {
            TelemetriaRepository = telemetriaRepository;
            TelemetriaHistoricoRepository = telemetriaHistoricoRepository;
        }
        public async Task<Result<Task, Error>> Handle(AtualizarTelemetriaHistoricoEvent command, CancellationToken cancellationToken)
        {
            var ultimaTelemetriaId = command.Request.UltimaTelemetriaId;
            var veiculoId = command.Request.VeiculoId;

            var telemetriaDesatualizada = await TelemetriaRepository.ObterTelemetriaAnteriorAsync(veiculoId, ultimaTelemetriaId);

            if (telemetriaDesatualizada is null)
            {
                return Task.CompletedTask;
            }

            var telemetriaHistorico = new TelemetriaHistoricoEntity
            {
                Id = telemetriaDesatualizada.Id,
                VeiculoId = telemetriaDesatualizada.VeiculoId,
                Latitude = telemetriaDesatualizada.Latitude,
                Longitude = telemetriaDesatualizada.Longitude,
                DataHora = telemetriaDesatualizada.DataHora,
            };

            await TelemetriaHistoricoRepository.InsertAsync(telemetriaHistorico);

            await TelemetriaRepository.DeleteAsync(telemetriaDesatualizada.Id);

            return Task.CompletedTask;
        } 
    }
}
