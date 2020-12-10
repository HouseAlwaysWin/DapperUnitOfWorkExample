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
        /// <summary>
        /// Default is sqlserver
        /// </summary>
        private class SqlCommand
        {
            public virtual string GetProducts(){
                return @"SELECT * FROM Product";
            }
        }

        private class PostgreCmd : SqlCommand
        {
            public override string GetProducts()
            {
                return @"";
            }
        }
        private class MySqlCmd : SqlCommand
        {
            public override string GetProducts()
            {
                return @"";
            }
        }

        private readonly Dictionary<string, SqlCommand> CmdDict = new Dictionary<string, SqlCommand>
        {
            ["sqlconnection"] = new SqlCommand(),
            ["npgsqlconnection"] = new PostgreCmd(),
            ["mysqlconnection"] = new MySqlCmd(),
        };

        private SqlCommand GetSqlCommand()
        {
            var name = typeof(DbType).FullName.ToLower();
            return CmdDict.TryGetValue(name, out var cmd) ? cmd : new SqlCommand();
        }

        #endregion
    }

}