using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class AddReservation : SystemOperationBase
    {
        private readonly Reservation reservation;
        public int Result { get; private set; }

        public AddReservation(Reservation reservation)
        {
            this.reservation = reservation;
        }

        protected override void ExecuteConcreteOperation()
        {
            Result = broker.AddReservation(reservation);
        }
    }
}
