using Common.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.ClientCommunication
{
    internal class ClientController
    {
        private Sender _sender;
        private Receiver _receiver;
        private Socket _socket;
        public ClientController(Socket socket)
        {
            _socket = socket;
            _sender = new Sender(_socket);
            _receiver = new Receiver(_socket);
        }

        public void Send(Request req)
        {
            try
            {
                _sender.Send(req);
            }
            catch (IOException ex)
            {
                Debug.WriteLine(">>>>" + ex.Message);
                throw new Exception("Server is down!");
            }
            catch (SocketException ex)
            {
                Debug.WriteLine(">>>>" + ex.Message);
                throw new Exception("Cannot find valid socket for communication!");
            }
        }
        public Response Receive()
        {
            Response response = (Response)_receiver.Receive();
            if (response.Exception == null) return response;
            else
            {
                throw new Exception(response.Exception.Message);
            }
        }

        public void Close()
        {
            _sender.Close();
            _receiver.Close();
        }
    }
}
