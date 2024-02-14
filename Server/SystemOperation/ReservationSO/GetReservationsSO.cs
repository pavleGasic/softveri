using Common.Domain;
using DBBroker;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class GetReservationsSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            Reservation reservation = (Reservation)entity;
            Result = genericRepository.Find(reservation, " r.WorkerId = @WorkerId ").Cast<Reservation>().ToList();
        }
    }
}
