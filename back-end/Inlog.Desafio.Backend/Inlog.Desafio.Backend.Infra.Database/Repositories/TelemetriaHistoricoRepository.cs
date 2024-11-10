using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class TelemetriaHistoricoRepository : BaseRepository<TelemetriaHistoricoEntity>, ITelemetriaHistoricoRepository
    {
        public TelemetriaHistoricoRepository(IOptions<DefaultSettings> settings) : base(settings)
        {
        } 
    }
}
