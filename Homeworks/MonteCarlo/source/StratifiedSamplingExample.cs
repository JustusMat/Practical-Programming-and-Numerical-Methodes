public class StratiedFiedSamplingExample
{
    public static void Main(string[] args)
    {
        MISER miserObject = new MISER(32 * 16 * 2, 16 * 2, 0.1, 2.0);
        double[] xlow = new double[2] { 0, 0 };
        double[] xup = new double[2] { 1.0, 1.0 };
        (double ave, double var) = miserObject.Integrate(h, (int)10e7, xlow, xup);
        System.Console.WriteLine($"{ave} {System.Math.Sqrt(var)}");
    }


    private static double h(double[] x)
    {
        double condition1 = (System.Math.Pow(x[0], 2) + System.Math.Pow(x[1], 2)) < System.Math.Pow(0.9, 2) ? 1.0 : 0.0;
        double condition2 = (System.Math.Pow(x[0], 2) + System.Math.Pow(x[1], 2)) < System.Math.Pow(0.3, 2) ? 1.0 : 0.0;
        return condition1 - condition2;
    }
}