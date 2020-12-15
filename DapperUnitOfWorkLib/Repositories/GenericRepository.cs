using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using DapperUnitOfWorkLib.Extensions;
using DapperUnitOfWorkLib.Interface;

namespace DapperUnitOfWorkLib.Repositories
{


    /// <summary>
    /// Generic GenerictRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IUnitOfWork _uow;
        public GenericRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<T> GetPaginated(ref int total, int currentPage, int itemsPerPage)
        {
            return _uow.Connection.GetPaginated<T>(ref total, currentPage, itemsPerPage, _uow.Transaction);
        }

        public async Task<(IEnumerable<T> list,int total)> GetPaginatedAsync(int currentPage, int itemsPerPage)
        {
            return  await _uow.Connection.GetPaginatedAsync<T>(currentPage, itemsPerPage, _uow.Transaction);
        }

        public IEnumerable<T> GetAll()
        {
            return _uow.Connection.GetAll<T>(_uow.Transaction);
        }

        public T Get(object id)
        {
            return _uow.Connection.Get<T>(id, _uow.Transaction);
        }

        public async Task<T> GetTaskAsync(object id)
        {
            return await _uow.Connection.GetAsync<T>(id, _uow.Transaction);
        }

        public void Insert(T model)
        {
            _uow.Connection.Insert<T>(model, _uow.Transaction);
        }

        public async Task InsertAsync(T model)
        {
            await _uow.Connection.InsertAsync<T>(model, _uow.Transaction);
        }

        public void Update(T model)
        {
            _uow.Connection.Update<T>(model, _uow.Transaction);
        }

        public async Task UpdateAsync(T model)
        {
            await _uow.Connection.UpdateAsync<T>(model, _uow.Transaction);
        }

        public void Delete(T model)
        {
            _uow.Connection.Delete<T>(model, _uow.Transaction);
        }

        public void  DeleteAll()
        {
             _uow.Connection.DeleteAll<T>(_uow.Transaction);
        }

        public async Task DeleteAsync(T model)
        {
            await _uow.Connection.DeleteAsync<T>(model, _uow.Transaction);
        }

        public async Task DeleteAllAsync()
        {
            await _uow.Connection.DeleteAllAsync<T>(_uow.Transaction);
        }

        public void BulkInsert(IEnumerable<T> model,int batchSize=0,int timeout=30)
        {
            _uow.Connection.BulkInsert<T>(model,_uow.Transaction,batchSize,timeout);
        }

        public async Task BulkInsertAsync(IEnumerable<T> model, int batchSize = 0, int timeout = 30)
        {
            await _uow.Connection.BulkInsertAsync<T>(model,_uow.Transaction,batchSize,timeout);
        }
    }
}