using Common.Domain;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal class UpdateReservationStatusSO : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Reservation reservation = (Reservation)entity;
                genericRepository.Update(reservation);
                if(reservation.ReservationStatus == ReservationStatus.Returned) 
                {
                    foreach (var reservationItem in reservation.ReservationItems)
                    {
                        reservationItem.Film.Quantity++;
                        genericRepository.Update(reservationItem.Film);
                    }
                }
                Result = entity;
            }
            catch (Exception)
            {
                throw new Exception("System cannot update reservation");
            }
        }
    }
}
