public class SolveDifferentialEquationByNetwork
{
    public static void Main(string[] args)
    {
        int N = 100;
        
        var diffeqNetwork = new DifferentialEquationNetwork(6, "Gaussian Wavelet", Phi);
        // a, b, c, y(c)=y(0)=2, y'(c)=y'(0)=-5
        double[] initial = new double[] { -8, 8, 0, 2, -5 };
        diffeqNetwork.TrainDifferentialEquation(initial, alpha:5.0, beta:8.0);
        
        
        
        double[] xVals = Linspace(initial[0], initial[1], N);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./DifferentialEquationSolution.data"))
        {
            for (int i = 0; i < N; i++)
            {
                writer.WriteLine($"{xVals[i]} {AnalyticSolution(xVals[i])} {diffeqNetwork.Response(xVals[i])}");
            }
        }
    }



    public static double AnalyticSolution(double t)
    {
        //return System.Math.Exp(-t) * (System.Math.Exp(2 * t) - 1) / 2.0;
        return System.Math.Pow(t,3)/12 -5*t +2;
    }
    
    
    public static
        double Phi(System.Func<double, double> ypp, System.Func<double, double> yp, System.Func<double, double> y,
            double x)
    {
        return 2*ypp(x) - x;
    }
    
    
    public static double[] Linspace(double start, double stop, int num)
    {
        double[] result = new double[num];
        double stepSize = (stop - start) / (num - 1);

        for (int i = 0; i < num; i++)
        {
            result[i] = start + i * stepSize;
        }

        return result;
    }
}