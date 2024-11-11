using FluentValidation;
using Inlog.Desafio.Backend.Application.Commands;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Application.ResultHandling.Errors;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class InserirTelemetriaCommandHandler : IRequestHandler<InserirTelemetriaCommand, Result<InserirTelemetriaResponse, Error>>
    {
        public IValidator<InserirTelemetriaCommand> Validador { get; set; }
        public ITelemetriaRepository TelemetriaRepository { get; set; }
        public IVeiculoRepository VeiculoRepository { get; set; }

        public InserirTelemetriaCommandHandler(IValidator<InserirTelemetriaCommand> validador, IVeiculoRepository veiculoRepository, ITelemetriaRepository telemetriaRepository)
        {
            Validador = validador;
            VeiculoRepository = veiculoRepository;
            TelemetriaRepository = telemetriaRepository;
        }

        public Task<Result<InserirTelemetriaResponse, Error>> Handle(InserirTelemetriaCommand command, CancellationToken cancellationToken)
        {
            var response = ValidarRequest(command)
                .Bind(VerificarSeVeiculoExiste)
                .Bind(InserirTelemetria);

            return Task.FromResult(response);
        }

        private Result<InserirTelemetriaCommand, Error> ValidarRequest(InserirTelemetriaCommand command)
        {
            var resultadoValidacao = Validador.Validate(command);

            if (!resultadoValidacao.IsValid)
            {
                return new RequestValidationError(resultadoValidacao.Errors);
            }

            return command;  
        }

        private Result<InserirTelemetriaCommand, Error> VerificarSeVeiculoExiste(InserirTelemetriaCommand command)
        {
            var idVeiculo = command.Request.IdVeiculo;

            var veiculo = VeiculoRepository.ObterVeiculoPorId(idVeiculo).Result;

            if (veiculo is null)
            {
                return new VehicleNotFoundError();
            }

            return command; 
        }

        private Result<InserirTelemetriaResponse, Error> InserirTelemetria(InserirTelemetriaCommand command)
        {
            var telemetria = new Telemetria
            { 
                IdVeiculo = command.Request.IdVeiculo,
                Latitude = command.Request.Latitude,
                Longitude = command.Request.Longitude,
                DataHora = DateTime.Now
            };

            var idTelemetria = TelemetriaRepository.InserirTelemetriaAsync(telemetria).Result;

            var resposta = new InserirTelemetriaResponse
            {
                IdTelemetria = idTelemetria,
                IdVeiculo = telemetria.IdVeiculo 
            };

            return resposta;  
        }
    }
}
