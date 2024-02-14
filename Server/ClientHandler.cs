using Common.Communication;
using Common.Domain;
using DBBroker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Server
{
    internal class ClientHandler
    {

        private Sender sender;
        private Receiver receiver;
        private Socket socket;
        private FrmServer frmServer;

        public ClientHandler(Socket socket, FrmServer frmServer)
        {
            this.frmServer = frmServer;
            this.socket = socket;
            sender = new Sender(socket);
            receiver = new Receiver(socket);
        }

        internal void HandleRequest()
        {
            try
            {
                while (true)
                {
                    Request req = (Request)receiver.Receive();
                    AddMessageToList(req);
                    Response res = ProcessRequest(req);
                    sender.Send(res);
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void AddMessageToList(Request req)
        {
            if (req != null && req.Username.Length > 0)
            {
                frmServer.Invoke(new Action(() =>
                {
                    frmServer.listBox1.Items.Add($"[ {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} ], {req.Username} requested for {req.Operation.ToString()}");
                }));
            }
        }

        private Response ProcessRequest(Request req)
        {
            Response res = new Response();
            try
            {
                switch (req.Operation)
                {
                    case Operation.Login:
                        res.Result = Controller.Instance.Login((Worker)req.Argument);
                        if (res.Result == null) res.Exception = new Exception();
                        break;
                    case Operation.Register:
                        res.Result = Controller.Instance.Register((Worker)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.AddCustomer:
                        res.Result = Controller.Instance.AddCustomer((Customer)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.GetCustomers:
                        res.Result = Controller.Instance.GetCustomers((Customer)req.Argument);
                        break;
                    case Operation.UpdateCustomer:
                        res.Result = Controller.Instance.UpdateCustomer((Customer)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.AddActor:
                        res.Result = Controller.Instance.AddActor((Actor)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.GetGenres:
                        res.Result = Controller.Instance.GetGenres();
                        break;
                    case Operation.GetActors:
                        res.Result = Controller.Instance.GetActors();
                        break;
                    case Operation.AddFilm:
                        res.Result = Controller.Instance.AddFilm((Film)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.GetFilms:
                        res.Result = Controller.Instance.GetFilms((Film)req.Argument);
                        break;
                    case Operation.DeleteFilm:
                        res.Result = Controller.Instance.DeleteFilm((Film)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.AddReservation:
                        res.Result = Controller.Instance.AddReservation((Reservation)req.Argument);
                        break;
                    case Operation.GetReservations:
                        res.Result = Controller.Instance.GetReservations((Reservation)req.Argument);
                        break;
                    case Operation.UpdateReservationStatus:
                        res.Result = Controller.Instance.UpdateReservationStatus((Reservation)req.Argument);
                        if ((int)res.Result == -1) res.Exception = new Exception();
                        break;
                    case Operation.DeleteReservation:
                        res.Result = Controller.Instance.DeleteReservation((Reservation)req.Argument);
                        break;
                }
            }
            catch (Exception e)
            {
                res.Exception = e;
                Debug.WriteLine(e.Message);
            }
            return res;
        }
    }
}