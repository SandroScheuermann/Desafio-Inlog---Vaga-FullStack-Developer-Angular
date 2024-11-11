using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Application.Requests
{
    public class AtualizarTelemetriaHistoricoRequest
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UltimaTelemetriaId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }   

        public DateTime DataHora { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
