using FluentValidation;
using Inlog.Desafio.Backend.Application.Commands;
using MongoDB.Bson;

namespace Inlog.Desafio.Backend.Application.Validators
{
    public class InserirTelemetriaRequestValidator : AbstractValidator<InserirTelemetriaCommand>
    {
        public InserirTelemetriaRequestValidator()
        {
            RuleFor(command => command.Request.IdVeiculo)
                .NotNull().NotEmpty().WithMessage("O Id do veículo é uma informação obrigatória."); 

            RuleFor(command => command.Request.Latitude)
                .NotNull().NotEmpty().WithMessage("A latitude é uma informação obrigatória.")
                .InclusiveBetween(-90, 90).WithMessage("A latitude deve estar entre -90 e 90.");

            RuleFor(command => command.Request.Longitude)
                .NotNull().NotEmpty().WithMessage("A longitude é uma informação obrigatória.")
                .InclusiveBetween(-180, 180).WithMessage("A longitude deve estar entre -180 e 180.");
        } 
    }
}
