using System.Linq;
using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWorkLib.Repositories;
using Xunit;

namespace DapperUnitOfWorkTest
{

    public class TestsFixture : IDisposable
    {
        public IUnitOfWork sqlserverUow;
        public ITestRepository sqlserverTestRepository;
        public TestsFixture()
        {
           sqlserverUow = new UnitOfWork("Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false");  
           sqlserverTestRepository = new TestRepository(sqlserverUow);
        }

        public void Dispose()
        {
           sqlserverTestRepository.DeleteAll();
        }
    }

    [Table("Test")]
    public class Test{
        [Key]
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

    public class SqlServerTest :IClassFixture<TestsFixture>
    {
        public IUnitOfWork sqlserverUow;
        public ITestRepository sqlserverTestRepository;
        TestsFixture testsFixture;
        public SqlServerTest(TestsFixture testsFixture)
        // public SqlServerTest()
        {
            // this.testsFixture = testsFixture;
           sqlserverUow = new UnitOfWork("Server=localhost,1500;Database=DapperUnitOfWorkDB;user id=SA;password=Your_password123;Integrated Security=false");  
           sqlserverTestRepository = new TestRepository(sqlserverUow);
        }
        

        [Fact]
        public void CanInsertDataToTest()
        {
            sqlserverUow.BeginTrans();
            Test test = new Test{
                Name = "test",
                Description = "test description",
                Price = 100
            };
            sqlserverTestRepository.Insert(test);
            var returnTest = sqlserverTestRepository.Get(test.Id);
            sqlserverUow.Commit();

            Assert.Equal("test",returnTest.Name);
            Assert.Equal("test description",returnTest.Description);
            Assert.Equal(100,returnTest.Price);
        }


        [Fact]
        public void CanGetPaginationTest(){
            sqlserverUow.BeginTrans();
            sqlserverTestRepository.DeleteAll();

           List<Test> testList = new List<Test>();
            for (int i = 0; i < 100; i++)
            {
               testList.Add(new Test{
                   Name= $"test{i}",
                   Description = $"description{i}",
                   Price = i
               });
            }

            sqlserverTestRepository.BulkInsert(testList);
            int total = 0;
            var returnResult = sqlserverTestRepository.GetPaginated(ref total,1,10).ToList();
            sqlserverUow.Commit();

            Assert.Equal(100,total);
            Assert.Equal("test4",returnResult[4].Name);
            Assert.Equal("description3",returnResult[3].Description);
            Assert.Equal(4,returnResult[4].Price);
        }
    }
}
