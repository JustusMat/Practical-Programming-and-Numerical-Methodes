public class Program
{
    public static void Main(string[] args)
    {
        Line();
        Line();
        System.Console.WriteLine("Minimising the RosenBrock function");
        System.Console.WriteLine("Initial guess:");
        vector x0 = new vector(10.0, 10.0);
        x0.print();
        var RosenBrockMinObject = new minimisationclass(RosenBrock, x0);
        vector xsolution = RosenBrockMinObject.MinimiseQN(maxIteration:(int)1e4);
        System.Console.WriteLine($"The solution is converged in {RosenBrockMinObject.iteration} iterations at");
        xsolution.print();
        Line();
        System.Console.WriteLine("Minimising the HimmelBlau function");
        System.Console.WriteLine("Initial guess:");
        x0.print();
        var HimmelBlauMinObject = new minimisationclass(HimmelBlau, x0);
        xsolution = HimmelBlauMinObject.MinimiseQN(maxIteration:(int)1e4);
        System.Console.WriteLine($"The solution is converged in {HimmelBlauMinObject.iteration} iterations at");
        xsolution.print();
        Line();
    }


    public static double RosenBrock(vector x)
    {
        double a = 100;
        double b = 1;

        double c = System.Math.Pow(b - x[0], 2);
        double d = a*System.Math.Pow(x[1] - x[0] * x[0], 2);

        return c + d;
    }

    public static double HimmelBlau(vector x)
    {
        double a = 11;
        double b = 7;
        double c = System.Math.Pow(x[0] * x[0] + x[1] - a, 2);
        double d = System.Math.Pow(x[0] + x[1] * x[1] - b, 2);
        return c + d;
    }
    
    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('─',n));
    }
}