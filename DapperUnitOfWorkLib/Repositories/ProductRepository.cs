using System.Collections.Generic;
using System.Data;
using Dapper;
using DapperUnitOfWorkLib.Entities;
using DapperUnitOfWorkLib.Interface;
using DapperUnitOfWorkLib.Interfaces;

namespace DapperUnitOfWorkLib.Repositories
{
    

    public class ProductRepository<DbType> : GenericRepository<Product>, IProductRepository where DbType : IDbConnection, new()
    {
        private readonly IUnitOfWork _uow;
        public ProductRepository(IUnitOfWork uow) : base(uow)
        {
            this._uow = uow;
        }

        public IEnumerable<Product> GetProducts(int num = 1000)
        {
            string sqlCmd = GetSqlCommand().GetProducts();
            return _uow.Connection.Query<Product>(sqlCmd, new { Num = num }, _uow.Transaction);
        }



        #region SqlCmd String
        private interface ISqlCommand
        {
            string GetProducts();
        }

        private class SqlServerCmd : ISqlCommand
        {
            public string GetProducts()
            {
                return @"";
            }
        }

        private class PostgreCmd : ISqlCommand
        {
            public string GetProducts()
            {
                return @"";
            }
        }
        private class MySqlCmd : ISqlCommand
        {
            public string GetProducts()
            {
                return @"";
            }
        }

        private readonly Dictionary<string, ISqlCommand> CmdDict = new Dictionary<string, ISqlCommand>
        {
            ["sqlconnection"] = new SqlServerCmd(),
            ["npgsqlconnection"] = new PostgreCmd(),
            ["mysqlconnection"] = new MySqlCmd(),
        };
        private readonly ISqlCommand DefaultAdapter = new SqlServerCmd();

        private ISqlCommand GetSqlCommand()
        {
            var name = typeof(DbType).FullName.ToLower();
            return CmdDict.TryGetValue(name, out var cmd) ? cmd : DefaultAdapter;
        }

        #endregion
    }

}