using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Calculator.Cliente
{
    internal class Program
    {
        static void Main(string[] args)
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
                    CalculadoraParametro parametro = null;
                    switch (operation)
                    {
                        case "a":
                            parametro = new CalculadoraParametro
                            {
                                Operacion = Operacion.Suma,
                                Operador1 = num1,
                                Operador2 = num2
                            };
                            break;
                        case "s":
                            parametro = new CalculadoraParametro
                            {
                                Operacion = Operacion.Resta,
                                Operador1 = num1,
                                Operador2 = num2
                            };
                            break;
                        case "m":
                            parametro = new CalculadoraParametro
                            {
                                Operacion = Operacion.Multiplicacion,
                                Operador1 = num1,
                                Operador2 = num2
                            };
                            break;
                        case "d":
                            parametro = new CalculadoraParametro
                            {
                                Operacion = Operacion.Division,
                                Operador1 = num1,
                                Operador2 = num2
                            };
                            break;
                    }

                    if (parametro != null)
                    {
                        var resultado = CallReoteProcess(parametro);

                        Console.WriteLine(resultado);
                    }
                }
            }

            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }

        static CalculadoraResultado CallReoteProcess(CalculadoraParametro parametro)
        {
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
                    //byte[] msg1 = Encoding.ASCII.GetBytes($"This is a test{i}");
                    var jsonParametro = JsonSerializer.Serialize<CalculadoraParametro>(parametro);

                    var cacheEnvio = Encoding.UTF8.GetBytes(jsonParametro);

                    // Send the data through the socket.
                    int bytesSend = sender.Send(cacheEnvio);

                    // Receive the response from the remote device.
                    byte[] bufferRec = new byte[1024];
                    int bytesRec1 = sender.Receive(bufferRec);

                    var jsonResultado = Encoding.UTF8.GetString(bufferRec, 0, bytesRec1);

                    var resultado = JsonSerializer.Deserialize<CalculadoraResultado>(jsonResultado);

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    return resultado;

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

            return null;
        }



        //private static void CallChildrenProcess(string commandLine)
        //{
        //    ProcessStartInfo startInfo = new ProcessStartInfo
        //    {
        //        FileName = @".\Console.Calculator.Process.exe",
        //        Arguments = commandLine,
        //        UseShellExecute = false,
        //        RedirectStandardError = true,
        //        RedirectStandardOutput = true
        //    };

        //    using Process process = Process.Start(startInfo);

        //    var errorReader = process.StandardError;
        //    var outputWriter = process.StandardOutput;

        //    process.WaitForExit();

        //    if (process.ExitCode == 0)
        //    {
        //        Console.WriteLine("El proceso finalizó con exito.");
        //        Console.WriteLine(outputWriter.ReadToEnd());
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Se produjo un error con codigo: {process.ExitCode}.");
        //        Console.WriteLine(errorReader.ReadToEnd());
        //    }
        //}
    }
}
