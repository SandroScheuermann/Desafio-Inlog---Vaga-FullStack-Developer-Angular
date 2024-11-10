using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Application.Requests
{
    public class ObterUltimaTelemetriaRequest
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }
    }
}
