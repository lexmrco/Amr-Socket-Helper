using SockNet.ClientSocket;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientExample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            byte[] recData = null;
            SocketClient client = new SocketClient("127.0.0.1", 9999);
            try
            {
                int salir = 0;
                while (salir == 0)
                {
                    Console.WriteLine("Escriba el mensaje:");
                    var message = Console.ReadLine();
                    if (await client.Connect())
                    {
                        await client.Send(message);
                        recData = await client.ReceiveBytes();
                    }

                    Console.WriteLine("Received data: " + Encoding.UTF8.GetString(recData));
                    Console.WriteLine("Desea salir?");
                    Console.WriteLine("0: NO");
                    Console.WriteLine("1: SI");

                    salir = int.Parse(Console.ReadLine());
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception raised: " + e);
            }
            //...
            client.Disconnect();
        }
    }
}
