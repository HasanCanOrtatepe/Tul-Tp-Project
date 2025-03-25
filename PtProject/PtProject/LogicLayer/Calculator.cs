using PtProject.DataLayer;
using System;
using System.Collections.Generic;

namespace PtProject.LogicLayer
{
    public class Calculator
    {
        private List<OperationLog> _history = new List<OperationLog>();

        public double Calculate(double a, double b, string op)
        {
            double result = op switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b != 0 ? a / b : throw new DivideByZeroException("Cannot divide by zero."),
                _ => throw new InvalidOperationException("Invalid operation.")
            };

            _history.Add(new OperationLog
            {
                Operand1 = a,
                Operand2 = b,
                Operator = op,
                Result = result
            });

            return result;
        }

        public List<OperationLog> GetHistory()
        {
            return _history;
        }
    }
}
