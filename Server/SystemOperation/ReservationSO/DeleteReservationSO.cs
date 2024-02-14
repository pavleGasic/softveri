using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class DeleteReservationSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
               if(((Reservation)entity).ReservationStatus == ReservationStatus.Returned)
                {
                    Reservation reservation = (Reservation)entity;
                    genericRepository.Delete(reservation, reservation.DeleteQuery);
                    Result = reservation;
                }
                else
                {
                    throw new Exception("Cannot delete not returned reservation!");
                }
            }
            catch (Exception)
            {
                throw new Exception("System cannot delete reservation!");
            }
        }
    }
}
