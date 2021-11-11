using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    [Flags]
    public enum Operacion
    {
        Suma = 1,
        Resta = 2,
        Multiplicacion = 4,
        Division = 5
    }
}
