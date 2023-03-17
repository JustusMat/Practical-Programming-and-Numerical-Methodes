public class Class1
{
    public static void Main(string[] args)
    {

        int NInterpolationPoints = 16;
        int NPlotPoints = 1000;
        double xlow = -2 * System.Math.PI;
        double xhigh = 2 * System.Math.PI;
        
        double[] xinterp = Linspace(xlow, xhigh,NInterpolationPoints);
        double[] yinterp = Sinc(xinterp);
        //double[] yinterp = Sine(xinterp);
        
        
        //Objects for interpolation
        linearinterpolation liObject = new linearinterpolation(xinterp,yinterp);
        quadraticinterpolation qiObject = new quadraticinterpolation(xinterp,yinterp);
        
        
        
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/interpolationdata.data"))
        {
            for (int i = 0; i < NInterpolationPoints; i++)
            {
                writer.WriteLine($"{xinterp[i]} {yinterp[i]}");
            }    
        }
        
        double[] xplot = Linspace(xlow, xhigh,NPlotPoints);
        double[] yplot = Sinc(xplot);
        //double[] yplot = Sine(xplot);
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/GraphData.data"))
        {
            for (int i = 0; i < NPlotPoints; i++)
            {
                writer.WriteLine($"{xplot[i]} {yplot[i]} {liObject.Interpolate(xplot[i])} {qiObject.Interpolate(xplot[i])}");
            }
        }
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/integratedinterpolated.data"))
        {
            for (int i = 1; i < NPlotPoints; i++)
            {
                writer.WriteLine($"{xplot[i]} {SincAntiDerivative(xplot[i]) +0.5} {liObject.Integrate(xplot[i])} {qiObject.Integrate(xplot[i])}");
            }
        }

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/derivativedata.data"))
        {
            for (int i = 0; i < NPlotPoints; i++)
            {
                writer.WriteLine($"{xplot[i]} {DerivativeSinc(xplot[i])} {qiObject.Derivative(xplot[i])}");
            }
        }
        
        
    }


    public static double DerivativeSinc(double x)
    {
        return System.Math.Cos(System.Math.PI * x) / x - System.Math.Sin(System.Math.PI * x) / (System.Math.PI * x * x);
    }
    
    
    public static double[] Sine(double[] x)
    {
        double[] y = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            y[i] = System.Math.Sin(x[i]);
        }
        
        return y;
    }
    public static double[] Sinc(double[] x)
    {
        double[] y = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            y[i] = System.Math.Sin(System.Math.PI*x[i]) / (System.Math.PI*x[i]);
        }
        return y;
    } 
    public static double SincAntiDerivative(double x)
    {

        return Si(System.Math.PI * x) / (System.Math.PI);
    }
    public static double Si(double x)
    {
        if (x==0)
        {
            return 0;
        }
        else if (x < 0)
        {
            return -Si(-x);
        }
        else
        {
            double integral = 0;
            double step = 0.0001;
            for (double t = step; t<=x; t+=step)
            {
                integral += System.Math.Sin(t) / t;
            }

            integral *= step;
            return integral;
        }
        
        
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
    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('-',n));
    }
    
}