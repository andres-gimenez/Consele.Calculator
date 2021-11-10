using System;
using System.Diagnostics;

namespace Calculator
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
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @".\Console.Calculator.Process.exe";
            startInfo.Arguments = commandLine;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

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
