using PtProject.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PtProject.LogicLayer
{
    public class Calculator
    {
        private List<OperationLog> _history = new List<OperationLog>();

        // Temel hesaplama fonksiyonu
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

            // İşlemi history'ye ekle
            _history.Add(new OperationLog
            {
                Operand1 = a,
                Operand2 = b,
                Operator = op,
                Result = result,
                Timestamp = DateTime.Now
            });

            return result;
        }

        // Kullanıcının girdiği işlemleri işlem önceliğine göre hesaplamak
        public double CalculateWithPrecedence(List<(double, string)> operations)
        {
            // Çarpma ve bölme işlemleri için geçici bir liste
            var precedenceOperations = new List<(double, string)>();
            double tempResult = operations[0].Item1;

            // İlk geçişte çarpma ve bölme işlemlerini yapıyoruz
            for (int i = 1; i < operations.Count; i++)
            {
                string op = operations[i].Item2;
                double num = operations[i].Item1;

                if (op == "*" || op == "/")
                {
                    tempResult = Calculate(tempResult, num, op);
                }
                else
                {
                    // Toplama ve çıkarma işlemleri için geçici listeye ekleme
                    precedenceOperations.Add((tempResult, op));
                    tempResult = num;
                }
            }

            // Tüm çarpma ve bölme işlemleri bittiğinde, toplama işlemi yapacağız
            precedenceOperations.Add((tempResult, "+"));

            //toplama ve çıkarma işlemlerini yapıyoruz
            return precedenceOperations.Aggregate(0.0, (acc, x) => Calculate(acc, x.Item1, x.Item2));
        }

        // Historydeki işlemleri al
        public List<OperationLog> GetHistory()
        {
            return _history;
        }
    }
}
