using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Aritmetica
{
    public class Arithmetic
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Sub(double a, double b)
        {
            return a - b;
        }

        public double Plus(double a, double b)
        {
            return a * b;
        }

        public double Div(double a, double b)
        {
            if (b == 0.0)
                throw new Exception("No se dividir entre 0");
            else
                return a / b;
        }
    }
}
