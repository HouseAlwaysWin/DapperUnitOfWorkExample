using System;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false";
            IUnitOfWork uow = new UnitOfWork(connectionString);
            IProductRepository productRepo = new ProductRepository(uow);
            // Product product = new Product{
            //     Name = "test2"
            // };

            uow.BeginTrans();
            List<Product> productList = new List<Product>();
            for (int i = 0; i < 100; i++)
            {
            //   product = new Product{
            //        Id=i,
            //        Name = $"test{i}"
            //   };   
            //   productRepo.Insert(product);
              productList.Add(new Product{
                  Name = $"desc{i}",
                  Description=$"name{i}",
                  Price = i
              });
            }

            // int total = 0;
            // var products = productRepo.GetPaginated(ref total,1,10);

            // await productRepo.InsertAsync(product);
            

            // var productsAsync = await productRepo.GetPaginatedAsync(1,10);
            productRepo.BulkInsert(productList);

            uow.Commit();
        }

    }
}
