using Common.Domain;
using Server.SystemOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server
{

    internal class Controller
    {

        private static Controller instance;
        public static Controller Instance
        {
            get
            {
                if (instance == null) instance = new Controller();
                return instance;
            }
        }

        private Controller() { }

        public Worker Login(Worker worker)
        {
            LoginSO so = new LoginSO();
            so.ExecuteTemplate(worker);
            return (Worker)so.Result;
        }

        public Worker Register(Worker worker)
        {
            RegisterSO so = new RegisterSO();
            so.ExecuteTemplate(worker);
            return (Worker)so.Result;
        }

        internal Customer AddCustomer(Customer customer)
        {
            AddCustomerSO so = new AddCustomerSO();
            so.ExecuteTemplate(customer);
            return (Customer)so.Result;
        }

        internal List<Customer> GetCustomers(Customer customer)
        {
            GetCustomersSO so = new GetCustomersSO();
            so.ExecuteTemplate(customer);
            return (List<Customer>)so.Result;
        }

        internal Customer UpdateCustomer(Customer customer)
        {
            UpdateCustomerSO so = new UpdateCustomerSO();
            so.ExecuteTemplate(customer);
            return (Customer)so.Result;
        }

        internal Actor AddActor(Actor actor)
        {
            AddActorSO so = new AddActorSO();
            so.ExecuteTemplate(actor);
            return (Actor)so.Result;
        }

        internal List<Genre> GetGenres()
        {
            GetGenresSO so = new GetGenresSO();
            so.ExecuteTemplate(new Genre());
            return (List<Genre>)so.Result;
        }

        internal List<Actor> GetActors()
        {
            GetActorsSO so = new GetActorsSO();
            so.ExecuteTemplate(new Actor());
            return (List<Actor>)so.Result;
        }

        internal Film AddFilm(Film film)
        {
            AddFilmSO so = new AddFilmSO();
            so.ExecuteTemplate(film);
            return (Film)so.Result;
        }

        internal List<Film> GetFilms(Film film)
        {
            GetFilmsSO so = new GetFilmsSO();
            so.ExecuteTemplate(film);
            return (List<Film>)so.Result;
        }

        internal Film DeleteFilm(Film film)
        {
            DeleteFilmSO so = new DeleteFilmSO();
            so.ExecuteTemplate(film);
            return (Film)so.Result;
        }

        internal Reservation AddReservation(Reservation reservation)
        {
            AddReservation so = new AddReservation(); 
            so.ExecuteTemplate(reservation);
            return (Reservation)so.Result;
        }

        internal List<Reservation> GetReservations(Reservation reservation)
        {
            GetReservationsSO so = new GetReservationsSO();
            so.ExecuteTemplate(reservation);
            return (List<Reservation>)so.Result;
        }

        internal Reservation UpdateReservationStatus(Reservation reservation)
        {
            UpdateReservationStatusSO so = new UpdateReservationStatusSO();
            so.ExecuteTemplate(reservation);
            return (Reservation)so.Result;
        }

        internal Reservation DeleteReservation(Reservation reservation)
        {
            DeleteReservationSO so = new DeleteReservationSO();
            so.ExecuteTemplate(reservation);
            return (Reservation)so.Result;
        }
    }
}
