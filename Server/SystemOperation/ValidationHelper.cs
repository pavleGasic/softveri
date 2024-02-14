using Common.Domain;
using Server.SystemOperation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.SystemOperation
{
    internal static class ValidationHelper
    {
        public static void ValidateRegister(Worker worker)
        {
            if (worker != null)
            {
                string pattern = @"^(?=.*[A-Z])(?=.*\d).+$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(worker.Password))
                {
                    throw new WorkerException("Pass must contain 1 capital letter and number");
                }
                else
                {
                    if (string.IsNullOrEmpty(worker.FirstName)  || string.IsNullOrEmpty(worker.LastName) || string.IsNullOrEmpty(worker.Username) || string.IsNullOrEmpty(worker.Password))
                    {
                        throw new WorkerException("Every property must be non null or empty");
                    }
                }
                return;
            }
            throw new WorkerException("Worker is null");
        }

        public static void ValidateLogin(Worker worker)
        {
            if (worker != null)
            {
                if (string.IsNullOrEmpty(worker.Username) || string.IsNullOrEmpty(worker.Password))
                {
                    throw new WorkerException("Every property must be non null or empty");
                }
                return;
            }
            throw new WorkerException("Worker is null");

        }

        public static void ValidateCustomer(Customer customer)
        {
            if (customer != null)
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);
                if (!regex.IsMatch(customer.Email))
                {
                    throw new CustomerException("Email is not in right format");
                }
                else
                {
                    if ( string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName))
                    {
                        throw new CustomerException("Every property must be non null or empty");
                    }
                }
                return;
            }
            throw new WorkerException("Customer is null");
        }

        internal static void ValidateFilm(Film film)
        {
            if (film != null)
            {
                if (string.IsNullOrEmpty(film.Title) || film.Genre == null)
                {
                    throw new FilmException("Every property must be non null or empty");
                }
                return;
            }
            throw new FilmException("Film is null");
        }

        internal static void ValidateActor(Actor actor)
        {
            if (actor != null)
            {
                if (string.IsNullOrEmpty(actor.Name))
                {
                    throw new ActorException("Every property must be non null or empty");
                }
                return;
            }
            throw new ActorException("Actor is null");

        }

        internal static void ValidateReservation(Reservation reservation)
        {
            if(reservation != null)
            {
                return;
            }
            throw new ReservationException("Reservation is null");

        }
    }
}
