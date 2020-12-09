using System.Collections.Generic;
using DapperUnitOfWorkLib.Entities;
using DapperUnitOfWorkLib.Interface;

namespace DapperUnitOfWorkLib.Interfaces
{
   public interface IProductRepository:IGenericRepository<Product>
    {
        IEnumerable<Product> GetProducts(int num = 1000);
    }
}