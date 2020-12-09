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
                Name = "test1"
            };

            uow.BeginTrans();
            productRepo.Insert(product);
            uow.Commit();
        }
    }
}
