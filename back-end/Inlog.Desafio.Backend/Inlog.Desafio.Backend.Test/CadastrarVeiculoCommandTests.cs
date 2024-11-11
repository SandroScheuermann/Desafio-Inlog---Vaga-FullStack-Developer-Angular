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
    public class CadastrarVeiculoCommandTests
    {
        public required Mock<IVeiculoRepository> _veiculoRepository;

        public required CadastrarVeiculoCommand _cadastrarVeiculoCommand;

        public required CadastrarVeiculoCommandHandler _cadastrarVeiculoHandler;

        public required CadastrarVeiculoRequestValidator _cadastrarVeiculovalidator;

        [SetUp]
        public void Setup()
        {
            _veiculoRepository = new();

            _veiculoRepository
                .Setup(repo => repo.InserirVeiculoAsync(It.IsAny<Veiculo>()))
                .Returns(Task.FromResult(1));

            _cadastrarVeiculovalidator = new();

            _cadastrarVeiculoCommand = new CadastrarVeiculoCommand
            {
                Request = new CadastrarVeiculoRequest
                {
                    Chassi = "1HGCM82633A004352",
                    Placa = "ABC1234",
                    TipoVeiculo = TipoVeiculo.Caminhao,
                    Cor = "Preto"
                }
            };

            _cadastrarVeiculoHandler = new(_cadastrarVeiculovalidator, _veiculoRepository.Object);
        }

        [Test]
        public async Task Deve_Cadastrar_Com_Sucesso()
        {
            var response = await _cadastrarVeiculoHandler.Handle(_cadastrarVeiculoCommand, default);

            response.Error.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Erro_De_Validacao()
        {
            _cadastrarVeiculoCommand.Request.Chassi = "chassiErrado";

            var response = await _cadastrarVeiculoHandler.Handle(_cadastrarVeiculoCommand, default);

            response.Error.Should().NotBeNull();
            response.Error.Should().BeOfType<RequestValidationError>();
        }

    }
}
