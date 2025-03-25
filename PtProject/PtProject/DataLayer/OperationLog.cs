namespace PtProject.DataLayer
{
    public class OperationLog
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operator { get; set; }
        public double Result { get; set; }

        public override string ToString()
        {
            return $"{Operand1} {Operator} {Operand2} = {Result}";
        }
    }
}
