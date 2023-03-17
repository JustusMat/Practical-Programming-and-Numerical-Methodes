public class quadraticinterpolation
{
    private int n;
    private double[] x, y, b, c;

    public quadraticinterpolation(double[] xvals, double[] yvals)
    {
        n = xvals.Length;
        x = xvals;
        y = yvals;
        b = new double[n - 1];
        c = new double[n - 1];
        double[] p = new double[n - 1];
        double[] h = new double[n - 1];
        for (int i = 0; i < n-1; i++)
        {
            h[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i]) / h[i];
        }

        c[0] = 0;
        for (int i = 0; i < n-2; i++)
        {
            c[i + 1] = (p[i + 1] - p[i] - c[i] * h[i]) / h[i + 1];
        }

        c[n - 2] /= 2;
        for (int i = 0; i < n-2; i++)
        {
            c[i] = (p[i + 1] - p[i] - c[i + 1] * h[i + 1]) / h[i];
        }

        for (int i = 0; i < n-1; i++)
        {
            b[i] = p[i] - c[i] * h[i];
        }
    }
    public double Interpolate(double z)
    {
        int i = Binsearch(x, z);
        return y[i] + (z - x[i])*(b[i] + (z - x[i])* c[i]);
    }
    public double Derivative(double z)
    {
        int i = Binsearch(x, z);
        return b[i] + 2 * c[i]*(z - x[i]);
    }
    public double Integrate(double z)
    {
        int index = Binsearch(x, z);
        double sum = 0;
        
        double deltaX = z - x[index];
        double integral = (((c[index] / 3 * deltaX) + b[index] / 2) * deltaX + y[index]) * deltaX;
        sum += integral;
        
        for (int i = 0; i < index; i++)
        {
            deltaX = x[i + 1] - x[i];
            integral = (((c[i] / 3 * deltaX) + b[i] / 2) * deltaX + y[i]) * deltaX;
            sum += integral;
        }

        return sum;
    }
    private static int Binsearch(double[] x, double z)
    {
        if (!(x[0]<=z && z<= x[x.Length-1]))
        {
            throw new System.Exception("binsearch: bad z");
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