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
    internal class AddReservation : SystemOperationBase
    {
        protected override void ExecuteConcreteOperation(IEntity entity)
        {
            try
            {
                Reservation reservation = (Reservation)entity;
                ValidationHelper.ValidateReservation(reservation);
                genericRepository.Add(reservation);
                foreach (var reservationItem in reservation.ReservationItems)
                {
                    reservationItem.ReservationId = reservation.ReservationId;
                    genericRepository.Add(reservationItem);
                    reservationItem.Film.Quantity--;
                    genericRepository.Update(reservationItem.Film);
                }
                Result = reservation;
            }
            catch
            {
                throw new Exception("System cannot create new reservation!");
            }
        }
    }
}
