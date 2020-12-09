using System;
using System.Data;

namespace DapperUnitOfWorkLib.Interface {
    public interface IUnitOfWork : IDisposable {

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTrans ();
        void Commit ();
        void Rollback ();
    }
}