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
        public ITelemetriaRepository TelemetriaRepository { get; set; }

        public ListarVeiculosQueryHandler(IVeiculoRepository repository, ITelemetriaRepository telemetriaRepository)
        {
            Repository = repository;
            TelemetriaRepository = telemetriaRepository;
        }

        public async Task<Result<ListarVeiculosResponse, Error>> Handle(ListarVeiculosQuery query, CancellationToken cancellationToken)
        {
            var response = await ObterListagemDeVeiculos();

            return response;
        }
        private async Task<Result<ListarVeiculosResponse, Error>> ObterListagemDeVeiculos()
        {
            var veiculos = await Repository.ObterVeiculos();

            var resposta = new ListarVeiculosResponse { Veiculos = veiculos }; 
             
            return resposta;
        }
    }
}
