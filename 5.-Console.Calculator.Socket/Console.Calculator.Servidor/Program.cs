using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Calculator.Servidor
{
    internal class Program
    {
        static int Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            
            try
            {

                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        static void aa()
        { 
            //System.Diagnostics.Debugger.Launch();

            if(args.Length == 1 && args[0] == "--help")
            {
                ShowHelp();
            }
            else if(args.Length != 3)
            {
                ShowError();
                return 1;
            }

            var command = args[0];

            double.TryParse(args[1], out double value1);

            double.TryParse(args[2], out double value2);

            switch (command)
            {
                case "--add":
                    Add(value1, value2);
                    break;
                case "--sub":
                    Sub(value1, value2);
                    break;
                case "--plus":
                    Plus(value1, value2);
                    break;
                case "--div":
                    Div(value1, value2);
                    break;
                default:
                    ShowError();
                    break;
            }

            return 0;
        }

        private static void Add(double a, double b)
        {
            Console.WriteLine($"{a:N} + {b:N} = {a + b:N}");
        }

        private static void Sub(double a, double b)
        {
            Console.WriteLine($"{a:N} - {b:N} = {a - b:N}");
        }

        private static void Plus(double a, double b)
        {
            Console.WriteLine($"{a:N} * {b:N} = {a * b:N}");
        }

        private static void Div(double a, double b)
        {
            if (b == 0.0)
                throw new ArgumentException("No se dividir entre 0");
                //Console.WriteLine("No se dividir entre 0");
            else
                Console.WriteLine($"{a:N} / {b:N} = {a / b:N}");
        }

        private static void ShowError()
        {
            Console.Error.WriteLine("Error: linea de comando mal formada.");
            Console.WriteLine("Error: linea de comando mal formada.");
            ShowHelp();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("USO:");

            Console.WriteLine("ConsoleCalculator [comand] [value1] [value2]");

            Console.WriteLine();

            Console.WriteLine("donde");
            Console.WriteLine("comand \t Comando add, sub, plus, div");
            Console.WriteLine();

        }
    }
}
