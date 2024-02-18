using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Server
{
    internal class ServerLogic
    {
        private Socket connectionSocket;
        private FrmServer frmServer;
        public ServerLogic(FrmServer frmServer) { 
            this.frmServer = frmServer;
            connectionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
        }
        public void Start()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), int.Parse(ConfigurationManager.AppSettings["port"]));

            connectionSocket.Bind(endPoint);
            connectionSocket.Listen(10);

            Thread thread = new Thread(AcceptClient);
            thread.Start();
        }

        private void AcceptClient()
        {
            try
            {
                while (true)
                {
                    Socket clientSocket = connectionSocket.Accept();
                    ClientHandler handler = new ClientHandler(clientSocket, frmServer);
                    Thread clientThread = new Thread(handler.HandleRequest);
                    clientThread.Start();
                }
            }catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        public void Stop()
        {
            connectionSocket.Close();
            foreach(var handler in Session.clientHandlers)
            {
                handler.Stop();
            }
            Session.clientHandlers.Clear();
        }
    }
}
