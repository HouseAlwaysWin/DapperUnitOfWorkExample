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

    public class GenericRepositoryTest :IClassFixture<TestsFixture>
    {
        TestsFixture testsFixture;
        public GenericRepositoryTest(TestsFixture testsFixture)
        {
            this.testsFixture = testsFixture;
        }
        

        [Fact]
        public void CanInsertDataToTest()
        {
            testsFixture.sqlserverUow.BeginTrans();
            Test test = new Test{
                Name = "test",
                Description = "test description",
                Price = 100
            };
            testsFixture.sqlserverTestRepository.Insert(test);
            var returnTest = testsFixture.sqlserverTestRepository.Get(test.Id);
            testsFixture.sqlserverUow.Commit();

            Assert.Equal("test",returnTest.Name);
            Assert.Equal("test description",returnTest.Description);
            Assert.Equal(100,returnTest.Price);

        }


        [Fact]
        public void CanGetPaginationTest(){
            testsFixture.sqlserverUow.BeginTrans();
            List<Test> testList = new List<Test>();
            for (int i = 0; i < 100; i++)
            {
               testList.Add(new Test{
                   Name= $"test{i}",
                   Description = $"description{i}",
                   Price = i
               });
            }
            
            int total = 0;
             var returnResult=testsFixture.sqlserverTestRepository.GetPaginated(ref total,1,10).ToList();
            testsFixture.sqlserverUow.Commit();

            Assert.Equal(100,total);
            Assert.Equal("test5",returnResult[4].Name);
            Assert.Equal("description4",returnResult[3].Description);
            Assert.Equal(4,returnResult[4].Price);
        }
    }
}
