using System;
using System.Security.Cryptography.X509Certificates;

public class CubicSubSpline
{
    private int n;
    private double[] x, y, b, c, d;

    public CubicSubSpline(double[] xvals, double[] yvals, bool Akima=false)
    {
        n = xvals.Length;
        x = xvals;
        y = yvals;
        if (Akima)
        {
            AssignCoefficientsAkima();
        }
        else
        {
            AssignCoefficients();
        }
    }

    public void AssignCoefficientsAkima()
    {
        b = new double[n];
        c = new double[n-1];
        d = new double[n-1];

        double[] p = new double[n - 1];
        double[] h = new double[n - 1];
        for (int i = 0; i < n - 1; i++)
        {
            h[i] = (x[i + 1] - x[i]);
            p[i] = (y[i + 1] - y[i]) / h[i];
        }


        double[] s = new double[n];
        s[0] = p[0];
        s[1] = (p[0] + p[1]) / 2;
        s[n - 2] = (p[n - 3] + p[n - 2]) / 2;
        s[n - 1] = p[n - 2];

        for (int i = 2; i < n - 2; i++)
        {
            double w1 = Math.Abs(p[i + 1] - p[i]);
            double w2 = Math.Abs(p[i - 1] - p[i - 2]);
            if (w1 + w2 == 0)
            {
                s[i] = (p[i - 1] + p[i]) / 2;
            }
            else
            {
                s[i] = (w1 * p[i - 1] + w2 * p[i]) / (w1 + w2);
            }
        }

        for (int i = 0; i < n -1; i++)
        {
            b[i] = s[i];
            c[i] = (3 * p[i] - 2 * s[i] - s[i + 1]) / h[i];
            d[i] = (s[i] + s[i+1] - 2 * p[i]) / (h[i] * h[i]);
        } 
    }

    public void AssignCoefficients()
    {
        b = new double[n];
        c = new double[n-1];
        d = new double[n-1];

        //Determine derivatives at xi
        for (int i = 1; i < n-1; i++)
        {
            (double acoeff, double bcoeff, double ccoeff) = DeterminePoly2(x[i - 1], x[i], x[i + 1], y[i - 1], y[i], y[i + 1]);
            //System.Console.Error.WriteLine($"{i} {acoeff} {bcoeff} {ccoeff}");
            b[i] = 2 * acoeff * x[i] + bcoeff;
        }
        b[0] = b[1];
        b[n - 1] = b[n - 2];
        
        for (int i = 0; i < n-1; i++)
        {
            c[i] = ((x[i] - x[i + 1]) * (2 * b[i] + b[i + 1]) + 3 * (y[i + 1] - y[i])) / System.Math.Pow(x[i] - x[i + 1], 2);
            d[i] = ((b[i] + b[i + 1]) * (x[i] - x[i + 1]) + 2 * (y[i + 1] - y[i])) / System.Math.Pow(x[i] - x[i + 1], 3);
        }
    }
    
    public static (double a, double b, double c) DeterminePoly2(double x1, double x2, double x3, double y1, double y2, double y3)
    {
        //System.Console.Error.WriteLine($"-------> {x1} {x2} {x3}");
        //System.Console.Error.WriteLine($"-------> {y1} {y2} {y3}");
        double a = (x1 * (y3 - y2) + x2 * (y1 - y3) + x3 * (y2 - y1)) / ((x1 - x2) * (x1 - x3) * (x2 - x3));
        double b = (y2 - y1) / (x2 - x1) - a * (x1 + x2);
        //In principle not needed, but nice to have...
        double c = y1 - a * System.Math.Pow(x1,2) - b * x1;
        
        return (a, b, c);
    }
    
    public double Interpolate(double z)
    {
        int i = Binsearch(x, z);
        double dx = z - x[i];
        return y[i] + b[i] * dx + c[i] * System.Math.Pow(dx, 2) + d[i] * System.Math.Pow(dx, 3);
    }

    public double Derivative(double z)
    {
        int i = Binsearch(x, z);
        double dx = z - x[i];
        return b[i] + 2 * c[i] * dx + 3 * d[i] * System.Math.Pow(dx, 2);
    }

    public double Integrate(double z)
    {
        int index = Binsearch(x, z);
        double sum = 0;
        double dx = z - x[index];
        double integral = ((((d[index] / 4 * dx) + c[index] / 3) * dx + b[index] / 2) * dx + y[index]) * dx;
        sum += integral;
        for (int i = 0; i < index; i++)
        {
            dx = x[i + 1] - x[i];
            integral = ((((d[i] / 4 * dx) + c[i] / 3) * dx + b[i] / 2) * dx + y[i]) * dx;
            sum += integral;
        }

        return sum;
    }
    
    private static int Binsearch(double[] x, double z)
    {
        if (!(x[0] <= z && z <= x[x.Length - 1]))
        {
            throw new System.Exception("Binsearch: bad z");
        }

        int i = 0;
        int j = x.Length - 1;
        while (j - i > 1)
        {
            int mid = (i + j) / 2;
            if (z > x[mid])
            {
                i = mid;
            }
            else
            {
                j = mid;
            }
        }

        return i;
    }
}