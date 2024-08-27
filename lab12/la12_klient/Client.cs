using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace lab12
{
    public class Client
    {
        private TcpClient client;
        private const string adress = "localhost";
        private const int port = 8888;

        public Client()
        {
            client = new TcpClient(adress, port);
        }

        public void Close()
        {
            Console.WriteLine("Client: stopped working!");
            client.Close();
            
        }

        public static void Main()
        {
            var client = new Client();
            Console.ReadKey();
            client.Close();
           // AppDomain.CurrentDomain.ProcessExit += (sender, e) => { onExit(sender, e); };
        }


        //public static void onExit(object sender, EventArgs e)
        //{

        //}
    }
}
