using PtProject.LogicLayer;
using System;

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
                    Console.Write("1. Sayı: ");
                    double a = Convert.ToDouble(Console.ReadLine());

                    Console.Write("İşlem (+, -, *, /): ");
                    string op = Console.ReadLine();

                    Console.Write("2. Sayı: ");
                    double b = Convert.ToDouble(Console.ReadLine());

                    double result = calc.Calculate(a, b, op);
                    Console.WriteLine($"Sonuç: {result}\n");

                    Console.Write("Devam etmek için ENTER, geçmişi görmek için G yaz: ");
                    string choice = Console.ReadLine();
                    if (choice?.ToLower() == "g")
                    {
                        Console.WriteLine("İşlem Geçmişi:");
                        foreach (var log in calc.GetHistory())
                        {
                            Console.WriteLine(log);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }

                Console.WriteLine("Çıkmak için Q, devam için ENTER");
                if (Console.ReadLine()?.ToLower() == "q")
                    break;
            }
        }
    }
}
