using FluentValidation;
using Inlog.Desafio.Backend.Application.Commands;

namespace Inlog.Desafio.Backend.Application.Validators
{
    public class CadastrarVeiculoRequestValidator : AbstractValidator<CadastrarVeiculoCommand>
    {
        public CadastrarVeiculoRequestValidator()
        {
            RuleFor(command => command.Request.TipoVeiculo) 
                .IsInEnum().WithMessage("O tipo do veículo deve ter um valor válido (1 - Onibus, 2 - Caminhão");

            RuleFor(command => command.Request.Placa)
                .NotEmpty().WithMessage("A placa é uma informação obrigatória.")
                .Length(7).WithMessage("A placa deve ter 7 caracteres.");

            RuleFor(command => command.Request.Chassi)
                .NotEmpty().WithMessage("O chassi é uma informação obrigatória.")
                .Length(17).WithMessage("O chassi deve possuir 17 caracteres alfanuméricos.");

            RuleFor(command => command.Request.Cor)
                .NotEmpty().WithMessage("A cor do veículo é uma informação obrigatória.");
        } 
    }
}
