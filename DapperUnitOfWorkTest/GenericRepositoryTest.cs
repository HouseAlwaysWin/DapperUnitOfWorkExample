using System;
using System.Collections.Generic;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWorkLib.Repositories;
using Xunit;

namespace DapperUnitOfWorkTest
{

    public class Test{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }

    public interface ITestRepository:IGenericRepository<Test>
    {
    }

    public class TestRepository : GenericRepository<Test>, ITestRepository 
    {
        public TestRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }

    public class GenericRepositoryTest 
    {
        IUnitOfWork sqlserverUow;
        ITestRepository testRepository;
        public GenericRepositoryTest()
        {
            sqlserverUow = new UnitOfWork("Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false"); 
        }
        

        [Fact]
        public void CanInsertDataToTest()
        {
            testRepository = new TestRepository(sqlserverUow);
            sqlserverUow.BeginTrans();
            testRepository.Insert(new Test{

            });
            sqlserverUow.Commit();
        }
    }
}
