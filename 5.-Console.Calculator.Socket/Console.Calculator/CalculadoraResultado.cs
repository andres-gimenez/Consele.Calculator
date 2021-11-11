using System;

namespace Calculator
{
    public class CalculadoraResultado
    {
        public double Operador1 { get; set; }

        public double Operador2 { get; set; }

        public Operacion Operacion { get; set; }

        public double Resultado { get; set; }

        public string Error { get; set; }
    }
}
