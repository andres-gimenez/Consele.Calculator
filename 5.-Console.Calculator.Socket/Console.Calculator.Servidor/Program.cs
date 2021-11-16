using Calculator;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Calculator.Servidor
{
    internal class Program
    {
        static void Main(string[] args)
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
                //Console.WriteLine("Press F to finish...");
                

                while (true)
                {
                    Socket handler = listener.Accept();
                    var cacheRec = new byte[4096];
                    int bytesRec = handler.Receive(cacheRec);

                    if (bytesRec > 0)
                    {
                        var jsonParametro = Encoding.UTF8.GetString(cacheRec, 0, bytesRec);
                        var parametro = JsonSerializer.Deserialize<CalculadoraParametro>(jsonParametro);

                        var resultado = Calculadora(parametro);

                        var jsonResultado = JsonSerializer.Serialize<CalculadoraResultado>(resultado);

                        var cacheEnvio = Encoding.UTF8.GetBytes(jsonResultado);
                        handler.Send(cacheEnvio);
                        Thread.Sleep(0);
                    }

                    //var key = Console.ReadKey();

                    //if (key.Key == ConsoleKey.F)
                    //    break;
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        static CalculadoraResultado Calculadora(CalculadoraParametro parametros)
        {
            switch (parametros.Operacion)
            {
                case Operacion.Suma:
                    return Add(parametros.Operador1, parametros.Operador2);
                case Operacion.Resta:
                    return Sub(parametros.Operador1, parametros.Operador2);
                case Operacion.Multiplicacion:
                    return Plus(parametros.Operador1, parametros.Operador2);
                case Operacion.Division:
                    return Div(parametros.Operador1, parametros.Operador2);
                default:
                    return new CalculadoraResultado
                    {
                        Operador1 = parametros.Operador1,
                        Operador2 = parametros.Operador2,
                        Operacion = parametros.Operacion,
                        Error = $"Operación {parametros.Operacion} no soportada."
                    };
            }
        }

        private static CalculadoraResultado Add(double a, double b)
        {
            try
            {
                var resultado = a + b;

                Console.WriteLine($"{a} + {b} = {resultado}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Suma,
                    Resultado = resultado,
                    Operador1 = a,
                    Operador2 = b
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Suma,
                    Operador1 = a,
                    Operador2 = b,
                    Error = ex.Message
                };
            }
        }

        private static CalculadoraResultado Sub(double a, double b)
        {
            try
            {
                var resultado = a - b;

                Console.WriteLine($"{a} - {b} = {resultado}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Resta,
                    Resultado = resultado,
                    Operador1 = a,
                    Operador2 = b
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Resta,
                    Operador1 = a,
                    Operador2 = b,
                    Error = ex.Message
                };
            }
        }

        private static CalculadoraResultado Plus(double a, double b)
        {
            try
            {
                var resultado = a * b;

                Console.WriteLine($"{a} * {b} = {resultado}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Multiplicacion,
                    Resultado = resultado,
                    Operador1 = a,
                    Operador2 = b
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Multiplicacion,
                    Operador1 = a,
                    Operador2 = b,
                    Error = ex.Message
                };
            }
        }

        private static CalculadoraResultado Div(double a, double b)
        {
            if (b == 0.0)
            {
                return new CalculadoraResultado
                {
                    Operacion = Operacion.Division,
                    Operador1 = a,
                    Operador2 = b,
                    Error = "No se puede dividir por cero."
                };
            }

            try
            {
                var resultado = a / b;

                Console.WriteLine($"{a} / {b} = {resultado}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Division,
                    Resultado = resultado,
                    Operador1 = a,
                    Operador2 = b
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

                return new CalculadoraResultado
                {
                    Operacion = Operacion.Division,
                    Operador1 = a,
                    Operador2 = b,
                    Error = ex.Message
                };
            }
        }
    }
}
