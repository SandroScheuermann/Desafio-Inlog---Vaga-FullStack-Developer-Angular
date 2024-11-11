using FluentAssertions;
using Inlog.Desafio.Backend.Application.Commands;
using Inlog.Desafio.Backend.Application.Handlers;
using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.ResultHandling.Errors;
using Inlog.Desafio.Backend.Application.Validators;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Moq;

namespace Inlog.Desafio.Backend.Test
{
    public class InserirTelemetriaCommandTests
    {
        public required Mock<IVeiculoRepository> _veiculoRepository;

        public required Mock<ITelemetriaRepository> _telemetriaRepository;

        public required InserirTelemetriaCommand _inserirTelemetriaCommand;

        public required InserirTelemetriaCommandHandler _inserirTelemetriaHandler;

        public required InserirTelemetriaRequestValidator _inserirTelemetriaValidator;

        [SetUp]
        public void Setup()
        {
            _telemetriaRepository = new();

            _telemetriaRepository
                .Setup(repo => repo.InserirTelemetriaAsync(It.IsAny<Telemetria>()))
                .Returns(Task.FromResult(1));

            _veiculoRepository = new();

            Veiculo fakeVeiculo = new() { Chassi = "", Cor = "", Placa = "", };

            _veiculoRepository
                .Setup(repo => repo.ObterVeiculoPorId(It.IsAny<int>()))
                .Returns(Task.FromResult(fakeVeiculo)!);

            _inserirTelemetriaValidator = new();

            _inserirTelemetriaCommand = new InserirTelemetriaCommand
            {
                Request = new InserirTelemetriaRequest
                {
                    IdVeiculo = 1,
                    Latitude = -30.593100d,
                    Longitude = -55.548000d,
                    DataHora = DateTime.Now
                }
            };

            _inserirTelemetriaHandler = new(_inserirTelemetriaValidator, _veiculoRepository.Object, _telemetriaRepository.Object);
        }

        [Test]
        public async Task Deve_Cadastrar_Com_Sucesso()
        {
            var response = await _inserirTelemetriaHandler.Handle(_inserirTelemetriaCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Erro_De_Validacao()
        {
            _inserirTelemetriaCommand.Request.Latitude = 100;

            var response = await _inserirTelemetriaHandler.Handle(_inserirTelemetriaCommand, default);

            response.Error.Should().NotBeNull();
            response.Error.Should().BeOfType<RequestValidationError>();
        }

        [Test]
        public async Task Deve_Retornar_Erro_Veiculo_Nao_Encontrado()
        {
            Veiculo? nullVeiculo = null;

            _veiculoRepository
                .Setup(repo => repo.ObterVeiculoPorId(It.IsAny<int>()))
                .Returns(Task.FromResult(nullVeiculo));

            var response = await _inserirTelemetriaHandler.Handle(_inserirTelemetriaCommand, default);

            response.Error.Should().NotBeNull();
            response.Error.Should().BeOfType<VehicleNotFoundError>();
        }

    }
}
