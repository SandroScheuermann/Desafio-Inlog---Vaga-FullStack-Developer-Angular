using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Commands
{
    public class CadastrarVeiculoCommand : IRequest<Result<CadastrarVeiculoResponse, Error>>
    {
        public required CadastrarVeiculoRequest Request { get; set; }
    }
}
