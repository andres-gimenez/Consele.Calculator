using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Calculator.Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];

            try
            {
                // Connect to a Remote server
                // Get Host IP Address that is used to establish a connection
                // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                // If a host has multiple addresses, you will get a list of addresses
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    // Connect to Remote EndPoint
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void bb ()
        { 
            // Declare variables and then initialize to zero.
            int num1 = 0; int num2 = 0;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (true)
            {
                // Ask the user to type the first number.
                Console.WriteLine("Type a number, and then press Enter");
                num1 = Convert.ToInt32(Console.ReadLine());

                // Ask the user to type the second number.
                Console.WriteLine("Type another number, and then press Enter");
                num2 = Convert.ToInt32(Console.ReadLine());

                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Exit");
                Console.Write("Your option? ");
            
                var operation = Console.ReadLine();
                if (operation != "e")
                {
                    switch (operation)
                    {
                        case "a":
                            CallChildrenProcess($"--add {num1} {num2}");
                            break;
                        case "s":
                            CallChildrenProcess($"--sub {num1} {num2}");
                            break;
                        case "m":
                            CallChildrenProcess($"--plus {num1} {num2}");
                            break;
                        case "d":
                            CallChildrenProcess($"--div {num1} {num2}");
                            break;
                    }
                }
            }
            
            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }

        private static void CallChildrenProcess(string commandLine)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @".\Console.Calculator.Process.exe",
                Arguments = commandLine,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            using Process process = Process.Start(startInfo);

            var errorReader = process.StandardError;
            var outputWriter = process.StandardOutput;

            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                Console.WriteLine("El proceso finalizó con exito.");
                Console.WriteLine(outputWriter.ReadToEnd());
            }
            else
            {
                Console.WriteLine($"Se produjo un error con codigo: {process.ExitCode}.");
                Console.WriteLine(errorReader.ReadToEnd());
            }
        }
    }
}
