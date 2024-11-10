using Inlog.Desafio.Backend.Application.Configuration;
using Inlog.Desafio.Backend.Domain.Models;
using Inlog.Desafio.Backend.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Inlog.Desafio.Backend.Infra.Database.Repositories
{
    public class VeiculoRepository : BaseRepository<VeiculoEntity>, IVeiculoRepository
    {
        public VeiculoRepository(IOptions<DefaultSettings> settings) : base(settings)
        {
        }
    }
}
