using Inlog.Desafio.Backend.Domain.Models;
using MongoDB.Driver;

namespace Inlog.Desafio.Backend.Domain.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(string id);

        public Task InsertAsync(T item);

        public Task InsertManyAsync(IEnumerable<T> items);

        public Task<DeleteResult> DeleteAsync(string id);

        public Task<UpdateResult> UpdateAsync(T item);

        public Task<bool> CheckExistanceById(string id);
    }
}
