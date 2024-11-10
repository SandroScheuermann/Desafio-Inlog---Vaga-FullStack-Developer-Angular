using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Queries
{
    public class ListarVeiculosQuery : IRequest<Result<ListarVeiculosResponse, Error>>
    { 
    }
}
