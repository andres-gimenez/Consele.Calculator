using Lib.Aritmetica;
using System;
using Xunit;

namespace UnitTest.calculator
{
    public class CalculatorUnit
    {
        [Fact]
        public void AddTest()
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Add(3, 4);

            Assert.Equal(7.0, result);
        }

        [Theory]
        [InlineData(5, 1, 6)]
        [InlineData(7, 1, 8)]
        [InlineData(784.33,  773.32, 1557.65)]
        public void AddTest2(double a, double b, double theoryResult)
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Add(a, b);

            Assert.Equal(theoryResult, result);
        }

        [Fact]
        public void SubTest()
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Sub(3, 4);

            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void PlusTest()
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Plus(3, 4);

            Assert.Equal(12, result);
        }

        [Fact]
        public void DivTest()
        {
            Arithmetic arithmetic = new Arithmetic();
            double result = arithmetic.Div(8, 4);

            Assert.Equal(2, result);
        }

        [Fact]
        public void DivExceptionTest()
        {
            Arithmetic arithmetic = new Arithmetic();

            Assert.Throws<Exception>(() =>
            {
                double result = arithmetic.Div(8, 0);
            });
        }
    }
}