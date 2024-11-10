using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Application.Requests
{
    public class InserirTelemetriaRequest
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
