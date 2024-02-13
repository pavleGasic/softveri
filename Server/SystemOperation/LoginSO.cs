using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class LoginSO : SystemOperationBase
    {
        private readonly Worker worker;
        public Worker Result { get; private set; }

        public LoginSO(Worker worker)
        {
            this.worker = worker;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.Login(worker);
        }
    }
}
