using Common.Domain;
using DBBroker;
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
        private Broker broker;

        private static Controller instance;
        public static Controller Instance
        {
            get
            {
                if (instance == null) instance = new Controller();
                return instance;
            }
        }

        private Controller() { broker = new Broker(); }

        public Worker Login(Worker worker)
        {
            if(!Session.Instance.ValidateLogin(worker)) return null;
            LoginSO so = new LoginSO(worker);
            so.ExecuteTemplate();
            return so.Result;
        }

        public int Register(Worker worker)
        {
            if (!Session.Instance.ValidateRegister(worker)) return -1;
            RegisterSO so = new RegisterSO(worker);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal int AddCustomer(Customer customer)
        {
            if(!Session.Instance.ValidateCustomer(customer)) return -1;
            AddCustomerSO so = new AddCustomerSO(customer);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal List<Customer> GetCustomers(string argument)
        {
            GetCustomersSO so = new GetCustomersSO(argument);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal int UpdateCustomer(Customer customer)
        {
            if(!Session.Instance.ValidateCustomer(customer)) return -1;
            UpdateCustomerSO so = new UpdateCustomerSO(customer);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object AddActor(Actor actor)
        {
            AddActorSO so = new AddActorSO(actor);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object GetGenres()
        {
            GetGenresSO so = new GetGenresSO();
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object GetActors()
        {
            GetActorsSO so = new GetActorsSO();
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object AddFilm(Film film)
        {
            if (!Session.Instance.ValidateFilm(film)) return -1;
            AddFilmSO so = new AddFilmSO(film);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object GetFilms(string search)
        {
            GetFilmsSO so = new GetFilmsSO(search);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object DeleteFilm(Film film)
        {
            DeleteFilmSO so = new DeleteFilmSO(film);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal int AddReservation(Reservation reservation)
        {
            AddReservation so = new AddReservation(reservation); 
            so.ExecuteTemplate();
            return so.Result;
        }

        internal List<Reservation> GetReservations(Worker worker)
        {
            GetReservationsSO so = new GetReservationsSO(worker);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal int UpdateReservationStatus(Reservation reservation)
        {
            UpdateReservationStatusSO so = new UpdateReservationStatusSO(reservation);
            so.ExecuteTemplate();
            return so.Result;
        }

        internal object DeleteReservation(Reservation reservation)
        {
            DeleteReservationSO so = new DeleteReservationSO(reservation);
            so.ExecuteTemplate();
            return so.Result;
        }
    }
}
