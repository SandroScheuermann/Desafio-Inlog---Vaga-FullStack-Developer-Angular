using Inlog.Desafio.Backend.Application.Commands;
using Inlog.Desafio.Backend.Application.Events;
using Inlog.Desafio.Backend.Application.Queries;
using Inlog.Desafio.Backend.Application.Requests;
using Inlog.Desafio.Backend.Application.ResultHandling.Errors;
using Inlog.Desafio.Backend.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inlog.Desafio.Backend.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController(ILogger<VeiculoController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<VeiculoController> _logger = logger;
    private readonly IMediator _mediator = mediator;

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarVeiculoRequest request)
    {
        var command = new CadastrarVeiculoCommand { Request = request };

        var response = await _mediator.Send(command);

        return response.Match(
            Ok,
            error => error switch
            {
                RequestValidationError => BadRequest(error.Message),
                _ => Problem(error.Message)
            });
    }

    [HttpPost("InserirTelemetria")]
    public async Task<IActionResult> InserirTelemetria([FromBody] InserirTelemetriaRequest request)
    {
        var command = new InserirTelemetriaCommand { Request = request };

        var response = await _mediator.Send(command);

        return response.Match(
            success =>
            {
                _mediator.Publish(new AtualizarTelemetriaHistoricoEvent
                {
                    Request = new AtualizarTelemetriaHistoricoRequest
                    {
                        IdUltimaTelemetria = success.IdTelemetria,
                        IdVeiculo = success.IdVeiculo,
                    }
                });

                return Ok(success);
            },
            error => error switch
            {
                RequestValidationError => BadRequest(error.Message),
                VehicleNotFoundError => NotFound(error.Message),
                _ => Problem(error.Message)
            });
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarVeiculosAsync()
    {
        var query = new ListarVeiculosQuery();

        var response = await _mediator.Send(query);

        return response.Match(
            Ok,
            error => Problem(error.Message));
    }
    [HttpGet("TiposVeiculo")]
    public IActionResult ObterTiposVeiculo()
    {
        var enumValues = Enum.GetValues(typeof(TipoVeiculo))
                             .Cast<TipoVeiculo>()
                             .Select(e => new
                             {
                                 Value = (int)e,
                                 Label = e.ToString()
                             });

        return Ok(enumValues);
    }

    [HttpGet("ObterHistoricoTelemetria/{idVeiculo}")]
    public async Task<IActionResult> ObterHistoricoTelemetriaAsync(int idVeiculo)
    {
        var query = new ObterHistoricoTelemetriaQuery
        {
            Request = new ObterHistoricoTelemetriaRequest
            {
                IdVeiculo = idVeiculo
            }
        };

        var response = await _mediator.Send(query);

        return response.Match(
            Ok,
            error => Problem(error.Message));
    }
}

