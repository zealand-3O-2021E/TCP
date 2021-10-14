using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 4646);
            listener.Start();
            Console.WriteLine("Server ready");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();//Until client comes in, the code doesn't continue
                Console.WriteLine("Incoming client");
                Task.Run(() =>
                {
                    Client(socket);
                });


            }
            // ReSharper disable once FunctionNeverReturns
        }
        private static void Client(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            //For more presise use, our reader and writer
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            Client client = new Client();
            while (true)
            {
                switch (reader.ReadLine().ToLower())
                {
                    case "getall":
                        writer.WriteLine(client.GetAll().Result);
                        break;
                    case "get":
                        writer.WriteLine(client.Get(reader.ReadLine()).Result);
                        break;
                    case "save":
                        client.Post(reader.ReadLine());
                        break;
                    case "update":
                        client.Put(reader.ReadLine());
                        break;
                    case "delete":
                        client.Remove(reader.ReadLine());
                        break;

                }
                writer.Flush();
            }
        }
    }
}
