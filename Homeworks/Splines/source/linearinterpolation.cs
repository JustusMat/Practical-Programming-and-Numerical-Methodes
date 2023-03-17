public class linearinterpolation
{
    private static double[] x;
    private static double[] y;


    public linearinterpolation(double[] xs, double[] ys)
    {
        x = xs;
        y = ys;
    }

    public double Interpolate(double z)
    {
        int i = Binsearch(x,z);
        double dx = x[i + 1] - x[i];
        double dy = y[i + 1] - y[i];
        if (!(dx>0))
        {
            throw new System.Exception("Whoops");
        }

        return y[i] + dy / dx * (z - x[i]);
    }

    public double Integrate(double z)
    {
        int Index = Binsearch(x, z);
        double sum = 0;
        double m = (y[Index + 1] - y[Index]) / (x[Index + 1] - x[Index]); // slope of the linear function
        double b = y[Index]; 
        double integral = (m / 2) * System.Math.Pow(z - x[Index],2) + b * (z - x[Index]);
        sum += integral;
        for (int i = 0; i < Index; i++)
        {
            m = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            b = y[i];
            integral = (m / 2) * System.Math.Pow(x[i + 1] - x[i], 2) + b * (x[i + 1] - x[i]);
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