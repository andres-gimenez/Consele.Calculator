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

        public override string ToString()
        {
            var operacionString = string.Empty;
            switch(Operacion)
            {
                case Operacion.Suma:
                    operacionString = "+";
                    break;
                case Operacion.Resta:
                    operacionString = "-";
                    break;
                case Operacion.Multiplicacion:
                    operacionString = "*";
                    break;
                case Operacion.Division:
                    operacionString = "/";
                    break;
            }

            return $"{Operador1} {operacionString} {Operador2} = {Resultado}";
        }
    }
}
