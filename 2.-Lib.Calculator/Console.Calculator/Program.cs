using Lib.Aritmetica;
using System;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 1 && args[0] == "--help")
            {
                ShowHelp();
            }
            else if(args.Length != 3)
            {
                ShowError();
                return;
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
        }

        private static void Add(double a, double b)
        {
            Arithmetic arithmetic = new Arithmetic();
            double add = arithmetic.Add(a, b);

            Console.WriteLine($"{a:N} + {b:N} = {add:N}");
        }

        private static void Sub(double a, double b)
        {
            Arithmetic arithmetic = new Arithmetic();
            double sub = arithmetic.Sub(a, b);

            Console.WriteLine($"{a:N} + {b:N} = {sub:N}");
        }

        private static void Plus(double a, double b)
        {
            Arithmetic arithmetic = new Arithmetic();
            double plus = arithmetic.Plus(a, b);

            Console.WriteLine($"{a:N} * {b:N} = {plus:N}");
        }

        private static void Div(double a, double b)
        {
            try
            {
                Arithmetic arithmetic = new Arithmetic();
                double div = arithmetic.Div(a, b);

                Console.WriteLine($"{a:N} / {b:N} = {div:N}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ShowError()
        {
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
