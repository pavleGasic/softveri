using Client.UserControls;
using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Communication
    {
        private static Communication _instance;
        public static Communication Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Communication();
                return _instance;
            }
        }
        private Communication() { }

        private Socket socket;
        private Sender sender;
        private Receiver receiver;

        public void Connect()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), int.Parse(ConfigurationManager.AppSettings["port"]));
            receiver = new Receiver(socket);
            sender = new Sender(socket);
        }

        internal Response Login(Worker worker)
        {
            Request req = new Request()
            {
                Argument = worker,
                Username = worker.Username,
                Operation = Operation.Login
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response Register(Worker worker)
        {
            Request req = new Request()
            {
                Argument = worker,
                Username = worker.Username,
                Operation = Operation.Register
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response AddCustomer(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddCustomer
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response GetCustomers(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetCustomers
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response EditCustomer(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.UpdateCustomer
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response AddActor(Actor actor)
        {
            Request req = new Request()
            {
                Argument = actor,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddActor
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response GetGenres()
        {
            Request req = new Request()
            {
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetGenres
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response GetActors()
        {
            Request req = new Request() 
            {
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetActors
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response AddFilm(Film film)
        {
            Request req = new Request()
            {
                Argument = film,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddFilm
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response GetFilms(Film film)
        {
            Request req = new Request()
            {
                Argument = film,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetFilms
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response DeleteFilm(Film selectedFilm)
        {
            Request req = new Request()
            {
                Argument = selectedFilm,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.DeleteFilm
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response AddReservation(Reservation reservation)
        {
            Request req = new Request()
            {
                Argument = reservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddReservation
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response GetReservations()
        {
            Request req = new Request()
            {
                Argument = new Reservation() { Worker = Session.Instance.Worker },
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetReservations
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response UpdateReservationStatus(Reservation reservation)
        {
            Request req = new Request()
            {
                Argument = reservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.UpdateReservationStatus
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }

        internal Response DeleteReservation(Reservation selectedReservation)
        {
            Request req = new Request()
            {
                Argument = selectedReservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.DeleteReservation
            };
            sender.Send(req);
            return (Response)receiver.Receive();
        }
    }
}
