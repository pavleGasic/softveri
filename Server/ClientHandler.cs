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
        private Worker worker;
        private bool kraj = false;

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
                while (!kraj)
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
            finally { Stop(); }
        }

        

        private void AddMessageToList(Request req)
        {
            if (req != null && req.Username != null && req.Username.Length > 0)
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
                        if (res.Result == null) res.Exception = new Exception("Wrong credentials! Login error.");
                        else if (!IsWorkerActive(Session.clientHandlers, (Worker)res.Result))
                        {
                            worker = (Worker)res.Result;
                            Session.clientHandlers.Add(this);
                        }
                        else if (IsWorkerActive(Session.clientHandlers, (Worker)res.Result))
                        {
                            res.Exception = new Exception("This account is currently active!");
                        }
                        break;
                    case Operation.Register:
                        res.Result = Controller.Instance.Register((Worker)req.Argument);
                        break;
                    case Operation.AddCustomer:
                        res.Result = Controller.Instance.AddCustomer((Customer)req.Argument);
                        if ((Customer)res.Result != null)
                            res.Message = "System added customer successfully.";
                        break;
                    case Operation.GetCustomers:
                        res.Result = Controller.Instance.GetCustomers((Customer)req.Argument);
                        break;
                    case Operation.UpdateCustomer:
                        res.Result = Controller.Instance.UpdateCustomer((Customer)req.Argument);
                        if ((Customer)res.Result != null)
                            res.Message = "System updated customer successfully.";
                        break;
                    case Operation.AddActor:
                        res.Result = Controller.Instance.AddActor((Actor)req.Argument);
                        if ((Actor)res.Result != null)
                            res.Message = "System added actor successfully.";
                        break;
                    case Operation.GetGenres:
                        res.Result = Controller.Instance.GetGenres();
                        break;
                    case Operation.GetActors:
                        res.Result = Controller.Instance.GetActors();
                        break;
                    case Operation.AddFilm:
                        res.Result = Controller.Instance.AddFilm((Film)req.Argument);
                        if ((Film)res.Result != null)
                            res.Message = "System added film successfully.";
                        break;
                    case Operation.GetFilms:
                        res.Result = Controller.Instance.GetFilms((Film)req.Argument);
                        break;
                    case Operation.DeleteFilm:
                        res.Result = Controller.Instance.DeleteFilm((Film)req.Argument);
                        if ((Film)res.Result != null)
                            res.Message = "System deleted film successfully.";
                        break;
                    case Operation.AddReservation:
                        res.Result = Controller.Instance.AddReservation((Reservation)req.Argument);
                        if ((Reservation)res.Result != null)
                            res.Message = "System added reservation successfully.";
                        break;
                    case Operation.GetReservations:
                        res.Result = Controller.Instance.GetReservations((Reservation)req.Argument);
                        break;
                    case Operation.UpdateReservationStatus:
                        res.Result = Controller.Instance.UpdateReservationStatus((Reservation)req.Argument);
                        if ((Reservation)res.Result != null)
                            res.Message = "System updated reservation successfully.";
                        break;
                    case Operation.DeleteReservation:
                        res.Result = Controller.Instance.DeleteReservation((Reservation)req.Argument);
                        if ((Reservation)res.Result != null)
                            res.Message = "System deleted reservation successfully.";
                        break;
                    case Operation.Logout:
                        Session.clientHandlers.Remove(this);
                        worker = null;
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

        private bool IsWorkerActive(List<ClientHandler> clientHandlers, Worker worker)
        {
            foreach (ClientHandler handler in clientHandlers)
            {
                if(handler.worker.Equals(worker))
                {
                    return true;
                }
            }
            return false;
        }

        internal void Stop()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
            Session.clientHandlers.Remove(this);
        }
    }
}