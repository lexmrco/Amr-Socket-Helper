using SockNet.ServerSocket;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 9999;
            var socketServer = new SocketServer();
            socketServer.InitializeSocketServer(ip, 9999);
            socketServer.SetReaderBufferBytes(1024);
            socketServer.StartListening();
            Console.WriteLine($"Socket Listent on Ip: '{ip}'. Port: '{port}'");
            bool openServer = true;
            while (openServer)
            {
                if (socketServer.IsNewData())
                {
                    var data = socketServer.GetData();
                    // Do whatever you want with data
                    Task.Run(() => DoSomething(data, socketServer));
                }
            }

            //.... 
            socketServer.CloseServer();
        }

        private static void DoSomething(KeyValuePair<TcpClient, byte[]> data, SocketServer server)
        {
            Console.WriteLine(((IPEndPoint)data.Key.Client.RemoteEndPoint).Address.ToString() + ": " + Encoding.UTF8.GetString(data.Value));
            server.ResponseToClient(data.Key, "received");
        }
    }
}
