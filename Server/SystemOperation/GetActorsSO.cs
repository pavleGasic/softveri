using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetActorsSO : SystemOperationBase
    {
        public List<Actor> Result { get; private set; }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.GetActors();
        }
    }
}
