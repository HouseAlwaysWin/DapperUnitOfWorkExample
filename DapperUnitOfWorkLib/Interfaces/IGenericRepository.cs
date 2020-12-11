using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperUnitOfWorkLib.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(T model);
        Task DeleteAll();
        Task DeleteAsync(T model);
        T Get(object id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetPaginated(ref int total, int currentPage, int itemsPerPage);
        Task<(IEnumerable<T> list,int total)> GetPaginatedAsync(int currentPage, int itemsPerPage);
        Task<T> GetTaskAsync(object id);
        void Insert(T model);
        Task InsertAsync(T model);
        void BulkInsert(IEnumerable<T> model,int batchSize,int timeout);
        void Update(T model);
        Task UpdateAsync(T model);

    }
}