public class Class1
{
    static void Main(string[] args)
    {
        int i = 1;
        while (i + 1 > i)
        {
            i++;
        }
        System.Console.Write("my max int={0}\n", i);
        const int minValue = int.MinValue;
        System.Console.Write($"my systems min integer value {minValue}\n");
        Line();
        
        System.Console.WriteLine("While and do while loop results for machine epsilon");
        System.Console.WriteLine("double machine epsilon: {0}",Epsfunc.MachineEpsilonDouble());
        System.Console.WriteLine("float machine epsilon {0}",Epsfunc.MachineEpsilonFloat());
        System.Console.WriteLine("Power representation of machine epsilon");
        System.Console.WriteLine("double machine epsilon: {0}",System.Math.Pow(2,-52) );
        System.Console.WriteLine("float machine epsilon: {0}",System.Math.Pow(2,-23) );
        Line();
        
        const int n = (int)1e6;
        var sums = Epsfunc.TinyMachineEpsilon();
        System.Console.WriteLine($"sumA-1 = {sums.Item1-1:e} should be {n*System.Math.Pow(2,-52)/2}");
        System.Console.WriteLine($"sumB-1 = {sums.Item2-1:e} should be {n*System.Math.Pow(2,-52)/2}");
        Line();
        
        const double d1 = 0.1 * 8;
        const double d2 = 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1;
        System.Console.WriteLine($"d1={d1:e15}");
        System.Console.WriteLine($"d2={d2:e15}");
        System.Console.WriteLine($"difference : {System.Math.Abs(d1-d2):e15}");
        System.Console.WriteLine($"d1==d2 ? => {d1==d2}");
        System.Console.WriteLine($"approx(d1==d2?) ? => {Epsfunc.Approx(d1,d2)}");
        Line();
    }
    
    static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }
    
    
    
}