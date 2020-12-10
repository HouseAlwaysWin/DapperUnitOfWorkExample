using System;
using DapperUnitOfWorkLib.Entities;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWorkLib.Interfaces;
using DapperUnitOfWorkLib.Repositories;
using Microsoft.Data.SqlClient;

namespace DapperUnitOfWorkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false";
            IUnitOfWork uow = new UnitOfWork<SqlConnection>(connectionString);
            IProductRepository productRepo = new ProductRepository<SqlConnection>(uow);
            Product product = new Product{
                Name = "test2"
            };

            uow.BeginTrans();
            // for (int i = 0; i < 100; i++)
            // {
            //  product = new Product{
            //     Id=i,
            //     Name = $"test{i}"
            //  };   
            //   productRepo.Insert(product);
            // }
            int total = 0;
            var products = productRepo.GetPaginated(ref total,2,10);

            uow.Commit();

            foreach (var item in products)
            {
               System.Console.WriteLine(item.Name); 
            }
        }
    }
}
