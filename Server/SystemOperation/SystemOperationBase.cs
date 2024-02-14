using DBBroker;
using Repository.Implementation;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal abstract class SystemOperationBase
    {
        protected IDBRepository<IEntity> genericRepository;
        public object Result { get; protected set; }

        public SystemOperationBase()
        {
            genericRepository = new GenericDBRepository();
        }

        public void ExecuteTemplate(IEntity entity)
        {
            try
            {
                genericRepository.BeginTransaction();
                ExecuteConcreteOperation(entity);
                genericRepository.Commit();

            }catch (Exception ex)
            {
                genericRepository.Rollback();
                Debug.WriteLine("EXCEPTION: " + ex.ToString());
                throw ex;
            }
            finally { genericRepository.CloseConnection(); }
        }

        protected abstract void ExecuteConcreteOperation(IEntity entity);
    }
}
