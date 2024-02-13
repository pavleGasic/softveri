using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal abstract class SystemOperationBase
    {
        protected Broker broker;

        public SystemOperationBase()
        {
            broker = new Broker();
        }

        public void ExecuteTemplate()
        {
            try
            {
                broker.OpenConnection();
                broker.BeginTransaction();
                ExecuteConcreteOperation();
                broker.CommitTransaction();

            }catch (Exception ex)
            {
                broker.RollbackTransaction();
                throw ex;
            }
            finally { broker.CloseConnection(); }
        }

        protected abstract void ExecuteConcreteOperation();
    }
}
