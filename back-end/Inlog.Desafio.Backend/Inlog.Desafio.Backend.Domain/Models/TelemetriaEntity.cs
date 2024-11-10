using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Domain.Models
{
    public class TelemetriaEntity : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }
        public DateTime DataHora { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
