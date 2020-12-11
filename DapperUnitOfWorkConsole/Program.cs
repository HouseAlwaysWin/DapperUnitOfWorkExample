using System;
using System.Threading.Tasks;
using DapperUnitOfWork.Repo;
using DapperUnitOfWork.Repo.Entities;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWorkLib.Repositories;
using Microsoft.Data.SqlClient;

namespace DapperUnitOfWorkConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false";
            IUnitOfWork uow = new UnitOfWork(connectionString);
            IProductRepository productRepo = new ProductRepository(uow);
            Product product = new Product{
                Name = "test2"
            };

            uow.BeginTrans();
            for (int i = 0; i < 100; i++)
            {
              product = new Product{
                   Id=i,
                   Name = $"test{i}"
              };   
              productRepo.Insert(product);
            }

            int total = 0;
            var products = productRepo.GetPaginated(ref total,1,10);

            await productRepo.InsertAsync(product);
            

            var productsAsync = await productRepo.GetPaginatedAsync(1,10);

            uow.Commit();
        }

    }
}
