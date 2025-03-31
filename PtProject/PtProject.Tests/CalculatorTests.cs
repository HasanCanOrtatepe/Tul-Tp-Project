using PtProject.LogicLayer;
using Xunit;
using System;
using System.Collections.Generic;

namespace PtProject.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(2, 3, "+", 5)]
        [InlineData(10, 4, "-", 6)]
        [InlineData(6, 2, "*", 12)]
        [InlineData(9, 3, "/", 3)]
        public void Calculate_BasicOperations_ReturnsCorrectResult(double a, double b, string op, double expected)
        {
            var calculator = new Calculator();
            var result = calculator.Calculate(a, b, op);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_DivideByZero_ThrowsException()
        {
            var calculator = new Calculator();
            Assert.Throws<DivideByZeroException>(() => calculator.Calculate(10, 0, "/"));
        }

        [Fact]
        public void Calculate_InvalidOperator_ThrowsException()
        {
            var calculator = new Calculator();
            Assert.Throws<InvalidOperationException>(() => calculator.Calculate(10, 5, "^"));
        }
    }
}
