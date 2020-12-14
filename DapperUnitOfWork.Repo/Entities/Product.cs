using Dapper.Contrib.Extensions;
using DapperUnitOfWorkLib.Extensions;

namespace DapperUnitOfWork.Repo.Entities
{
    [Table ("Product")]
    public class Product
    {
        [OrderBy(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description {get;set;}
        public int Price { get; set; }
    }
}