using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
    public class Server
    {
        private const int port = 8888;
        private TcpListener? tcpListener;
        private readonly List<Thread> clients = [];
        public Server()
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            Console.WriteLine($"Server: started working [port: {port}]");
        }

        public void Start()
        {
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                Console.WriteLine("Server: new client connected!");
                var clientThread = new Thread(clientHandler);
                clientThread.Start(client);
                clients.Add(clientThread);

                if(!clientThread.IsAlive)
                {
                    Console.WriteLine("Dis");
                    clients.Remove(clientThread);
                }

                if(clients.Count <= 0) {
                    Close();
                }
            }
        }

        private void clientHandler(object obj)
        {
            var client = (TcpClient)obj;
            Console.ReadLine();
        }

        private void Close()
        {
            if (tcpListener != null)
            {
                Console.WriteLine("Server: stopped working!");
                tcpListener.Stop();
            }
        }

        public static void Main()
        {
            var server = new Server();
            server.Start();

            //Console.ReadLine();
            //server.Close();
        }
    }
}
