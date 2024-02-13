using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetReservationsSO : SystemOperationBase
    {
        private readonly Worker worker;

        public List<Reservation> Result { get; private set; }

        public GetReservationsSO(Worker worker)
        {
            this.worker = worker;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.GetReservations(worker);
        }
    }
}
