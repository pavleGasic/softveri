using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddActorSO : SystemOperationBase
    {
        private readonly Actor actor;
        public int Result { get; private set; }

        public AddActorSO(Actor actor)
        {
            this.actor = actor;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = broker.AddActor(actor);
        }
    }
}
