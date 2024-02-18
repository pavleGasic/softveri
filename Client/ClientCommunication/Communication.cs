using Client.ClientCommunication;
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
using System.Windows.Forms;

namespace Client.ClientCommunication
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
        private ClientController controller;

        public void Connect()
        {
            try
            {
                if(socket == null || !socket.Connected)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), int.Parse(ConfigurationManager.AppSettings["port"]));
                    controller = new ClientController(socket);
                }
            }
            catch (Exception)
            {
                throw new Exception("Cannot connect to server!");
            }
        }

        internal Worker Login(Worker worker)
        {
            Request req = new Request()
            {
                Argument = worker,
                Username = worker.Username,
                Operation = Operation.Login
            };
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<Worker>();
        }

        public void Logout()
        {
            controller.Send(new Request()
            {
                Username = Session.Instance.Worker.Username,
                Operation = Operation.Logout
            });
            controller.Close();
            socket.Close();
            socket = null;
        }

        internal Worker Register(Worker worker)
        {
            Request req = new Request()
            {
                Argument = worker,
                Username = worker.Username,
                Operation = Operation.Register
            };
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<Worker>();
        }

        internal Customer AddCustomer(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddCustomer
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Customer c = response.ParseResponse<Customer>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return c;
        }

        internal List<Customer> GetCustomers(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetCustomers
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<List<Customer>>();
        }

        internal Customer UpdateCustomer(Customer customer)
        {
            Request req = new Request()
            {
                Argument = customer,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.UpdateCustomer
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Customer c = response.ParseResponse<Customer>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return c;
        }

        internal Actor AddActor(Actor actor)
        {
            Request req = new Request()
            {
                Argument = actor,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddActor
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Actor a = response.ParseResponse<Actor>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return a;
        }

        internal List<Genre> GetGenres()
        {
            Request req = new Request()
            {
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetGenres
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<List<Genre>>();
        }

        internal List<Actor> GetActors()
        {
            Request req = new Request() 
            {
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetActors
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<List<Actor>>();
        }

        internal Film AddFilm(Film film)
        {
            Request req = new Request()
            {
                Argument = film,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddFilm
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Film f = response.ParseResponse<Film>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return f;
        }

        internal List<Film> GetFilms(Film film)
        {
            Request req = new Request()
            {
                Argument = film,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetFilms
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<List<Film>>();
        }

        internal Film DeleteFilm(Film selectedFilm)
        {
            Request req = new Request()
            {
                Argument = selectedFilm,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.DeleteFilm
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Film f = response.ParseResponse<Film>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return f;
        }

        internal Reservation AddReservation(Reservation reservation)
        {
            Request req = new Request()
            {
                Argument = reservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.AddReservation
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Reservation r = response.ParseResponse<Reservation>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return r;
        }

        internal List<Reservation> GetReservations()
        {
            Request req = new Request()
            {
                Argument = new Reservation() { Worker = Session.Instance.Worker },
                Username = Session.Instance.Worker.Username,
                Operation = Operation.GetReservations
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            return response.ParseResponse<List<Reservation>>();
        }

        internal Reservation UpdateReservationStatus(Reservation reservation)
        {
            Request req = new Request()
            {
                Argument = reservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.UpdateReservationStatus
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Reservation r = response.ParseResponse<Reservation>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return r;
        }

        internal Reservation DeleteReservation(Reservation selectedReservation)
        {
            Request req = new Request()
            {
                Argument = selectedReservation,
                Username = Session.Instance.Worker.Username,
                Operation = Operation.DeleteReservation
            };
            Connect();
            controller.Send(req);
            Response response = controller.Receive();
            Reservation r = response.ParseResponse<Reservation>();
            MessageBox.Show(response.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return r;
        }
    }
}
