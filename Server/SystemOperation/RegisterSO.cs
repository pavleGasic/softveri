using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class RegisterSO : SystemOperationBase
    {
        private readonly Worker worker;
        public int Result { get; private set; }

        public RegisterSO(Worker worker)
        {
            this.worker = worker;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.Register(worker);
        }
    }
}
