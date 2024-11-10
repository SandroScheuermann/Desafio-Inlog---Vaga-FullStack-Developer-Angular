using Inlog.Desafio.Backend.Application.Queries;
using Inlog.Desafio.Backend.Application.Responses;
using Inlog.Desafio.Backend.Application.ResultHandling;
using Inlog.Desafio.Backend.Domain.Repositories;
using MediatR;

namespace Inlog.Desafio.Backend.Application.Handlers
{
    public class ListarVeiculosQueryHandler : IRequestHandler<ListarVeiculosQuery, Result<ListarVeiculosResponse, Error>>
    { 
        public IVeiculoRepository Repository { get; set; }

        public ListarVeiculosQueryHandler(IVeiculoRepository repository)
        { 
            Repository = repository;
        }

        public async Task<Result<ListarVeiculosResponse, Error>> Handle(ListarVeiculosQuery query, CancellationToken cancellationToken)
        {
            var response = await ListarVeiculos(query);

            return response;
        }  
        private async Task<Result<ListarVeiculosResponse, Error>> ListarVeiculos(ListarVeiculosQuery query)
        {  
            var result = await Repository.GetAllAsync(); 

            var resposta = new ListarVeiculosResponse 
            {
                Veiculos = result
            };

            return resposta; 
        }
    }
}
