using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Domain.Models
{
    public class TelemetriaHistoricoEntity : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }
        public required DateTime DataHora { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
