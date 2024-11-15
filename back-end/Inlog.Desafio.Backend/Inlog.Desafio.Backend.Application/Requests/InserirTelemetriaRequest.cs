﻿namespace Inlog.Desafio.Backend.Application.Requests
{
    public class InserirTelemetriaRequest
    { 
        public required int IdVeiculo { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
