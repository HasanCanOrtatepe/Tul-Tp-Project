using PtProject.LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calculator();

            while (true)
            {
                try
                {
                    List<(double, string)> operations = new List<(double, string)>();

                    Console.Write("Enter the first number: ");
                    double number = ReadDouble();
                    operations.Add((number, "+"));

                    while (true)
                    {
                        Console.Write("Enter operation (+, -, *, /): ");
                        string op = ReadOperator();

                        Console.Write("Enter next number: ");
                        number = ReadDouble();
                        operations.Add((number, op));

                        Console.Write("Do you want to continue? (Y/N): ");
                        if (Console.ReadLine().Trim().ToLower() != "y")
                            break;
                    }

                    double result = calc.CalculateWithPrecedence(operations);
                    Console.WriteLine($"Result: {result}\n");

                    Console.Write("Press Enter to continue, or type 'H' to view history: ");
                    if (Console.ReadLine()?.Trim().ToLower() == "h")
                    {
                        ShowHistory(calc);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("Press 'Q' to quit, or Enter to continue.");
                if (Console.ReadLine()?.Trim().ToLower() == "q")
                    break;
            }
        }

        static double ReadDouble()
        {
            double value;
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. Please enter a valid number: ");
            }
            return value;
        }

        static string ReadOperator()
        {
            string op;
            while (true)
            {
                op = Console.ReadLine();
                if (op == "+" || op == "-" || op == "*" || op == "/")
                    break;
                Console.Write("Invalid operator. Please enter one of (+, -, *, /): ");
            }
            return op;
        }

        static void ShowHistory(Calculator calc)
        {
            Console.WriteLine("Operation History:");
            foreach (var log in calc.GetHistory())
            {
                Console.WriteLine(log);
            }
        }
    }
}