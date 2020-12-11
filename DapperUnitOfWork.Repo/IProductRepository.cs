using System.Collections.Generic;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWork.Repo.Entities;

namespace DapperUnitOfWork.Repo
{
   public interface IProductRepository:IGenericRepository<Product>
    {
        IEnumerable<Product> GetProducts(int num = 1000);
    }
}