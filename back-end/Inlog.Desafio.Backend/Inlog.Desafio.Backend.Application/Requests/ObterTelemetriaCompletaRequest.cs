using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inlog.Desafio.Backend.Application.Requests
{
    public class ObterTelemetriaCompletaRequest
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required string VeiculoId { get; set; }
    }
}
