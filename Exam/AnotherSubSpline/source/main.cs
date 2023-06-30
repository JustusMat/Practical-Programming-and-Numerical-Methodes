public class main
{
    public static void Main(string[] args)
    {
        double[] xArrayInterpolate = Linspace(-2*System.Math.PI, 2*System.Math.PI, 10);
        double[] yArrayInterpolate = new double[xArrayInterpolate.Length];
        for (int i = 0; i < xArrayInterpolate.Length; i++)
        {
            yArrayInterpolate[i] = f(xArrayInterpolate[i]);
        }

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./Interpolation.data"))
        {
            for (int i = 0; i < xArrayInterpolate.Length; i++)
            {
                writer.WriteLine($"{xArrayInterpolate[i]} {yArrayInterpolate[i]}");
            }
        }

        double[] xPlot = Linspace(-2*System.Math.PI, 2*System.Math.PI, 1000);
        var InterpolationObject = new CubicSubSpline(xArrayInterpolate, yArrayInterpolate);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotData.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {InterpolationObject.Interpolate(xPlot[i])} {f(xPlot[i])}");
            }
        }
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotDerivativeData.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {InterpolationObject.Derivative(xPlot[i])} {Derivative(xPlot[i])}");
            }
        }

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotAntiderivativeData.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {InterpolationObject.Integrate(xPlot[i])} {Antiderivative(xPlot[i])}");
            }
        }
        
        
        
        //For Akima sub spline comparison
        var AkimaInterpolationObject = new CubicSubSpline(xArrayInterpolate, yArrayInterpolate,Akima:true);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotDataAkima.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {AkimaInterpolationObject.Interpolate(xPlot[i])}");
            }
        }
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotDerivativeDataAkima.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {AkimaInterpolationObject.Derivative(xPlot[i])}");
            }
        }

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./PlotAntiderivativeDataAkima.data"))
        {
            for (int i = 0; i < xPlot.Length; i++)
            {
                writer.WriteLine($"{xPlot[i]} {AkimaInterpolationObject.Integrate(xPlot[i])}");
            }
        }
        
    }


    public static double f(double x)
    {
        return System.Math.Exp(-x / 10.0) * System.Math.Sin(x);
    }

    public static double Derivative(double x)
    {
        return -System.Math.Exp(-x / 10.0) * (System.Math.Sin(x) - 10.0 * System.Math.Cos(x)) /
               10.0;
    }

    public static double Antiderivative(double x)
    {
        double numerator = System.Math.Exp(-x / 10) *
                           (10.0*System.Math.Sin(x) + 100 * System.Math.Cos(x));
        double denominator = 100.0 + 1.0;
        return -numerator/denominator +1.855897116421127;
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