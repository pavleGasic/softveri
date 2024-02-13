using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class UpdateReservationStatusSO : SystemOperationBase
    {
        private readonly Reservation reservation;
        public int Result { get; private set; }

        public UpdateReservationStatusSO(Reservation reservation)
        {
            this.reservation = reservation;
        }
        protected override void ExecuteConcreteOperation()
        {
            Result = broker.UpdateReservationStatus(reservation);
        }
    }
}
