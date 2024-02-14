using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDBRepository <T> : IRepository<T> where T : class
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void OpenConnection();
        void CloseConnection();
    }
}
