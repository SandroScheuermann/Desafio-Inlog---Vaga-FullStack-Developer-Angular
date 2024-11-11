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
    public class CadastrarVeiculoCommandHandler : IRequestHandler<CadastrarVeiculoCommand, Result<CadastrarVeiculoResponse, Error>>
    {
        public IValidator<CadastrarVeiculoCommand> Validador { get; set; }
        public IVeiculoRepository Repository { get; set; }

        public CadastrarVeiculoCommandHandler(IValidator<CadastrarVeiculoCommand> validador, IVeiculoRepository repository)
        {
            Validador = validador;
            Repository = repository;
        }

        public Task<Result<CadastrarVeiculoResponse, Error>> Handle(CadastrarVeiculoCommand command, CancellationToken cancellationToken)
        {
            var response = ValidarRequest(command)
                .Bind(CadastrarVeiculo);

            return Task.FromResult(response);
        }

        private Result<CadastrarVeiculoCommand, Error> ValidarRequest(CadastrarVeiculoCommand command)
        {
            var resultadoValidacao = Validador.Validate(command);

            if (!resultadoValidacao.IsValid)
            {
                return new RequestValidationError(resultadoValidacao.Errors);
            }

            return command; 
        }

        private Result<CadastrarVeiculoResponse, Error> CadastrarVeiculo(CadastrarVeiculoCommand command)
        {
            var veiculo = new Veiculo
            { 
                Chassi = command.Request.Chassi,
                TipoVeiculo = command.Request.TipoVeiculo,
                Cor = command.Request.Cor,
                Placa = command.Request.Placa, 
            };

            veiculo.Id = Repository.InserirVeiculoAsync(veiculo).Result;
              
            var resposta = new CadastrarVeiculoResponse 
            {
                Id = veiculo.Id 
            };

            return resposta; 
        }
    }
}
