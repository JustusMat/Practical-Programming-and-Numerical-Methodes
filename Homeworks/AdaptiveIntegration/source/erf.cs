public class erf
{
    public static void Main(string[] args)
    {
        double low = -5;
        double high = 5;
        int N = 500;
        double[] xvals = Linspace(low, high, N);
        
        for (int i = 0; i < xvals.Length; i++)
        {
            System.Console.WriteLine($"{xvals[i]} {ErrorFunction(xvals[i])}");
        }
        
        
    }

    public static double[] Linspace(double start, double stop, int num)
    {
        double[] result = new double[num];
        double step = (stop - start) / (num - 1);
        for (int i = 0; i < num; i++)
        {
            result[i] = start + i * step;
        }
        return result;
    }
    
    public static double ErrorFunction(double z)
    {
        if (z<0)
        {
            return -ErrorFunction(-z);
        }

        if (0 <= z & z<=1)
        {
            return 2 / System.Math.Sqrt(System.Math.PI) *
                   integrator.RAIntegrate(x => System.Math.Exp(-System.Math.Pow(x, 2)), 0, z);
            
        }
        else
        {
            System.Func<double, double> f = (t) =>
            {
                return System.Math.Exp(-System.Math.Pow(z + (1 - t )/ t, 2))/ t / t;
            };


            return 1 - 2 / System.Math.Sqrt(System.Math.PI) * integrator.RAIntegrate(f, 0, 1);
        }
    }
}  
