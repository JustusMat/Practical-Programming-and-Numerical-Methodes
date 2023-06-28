public class cubicinterpolation
{
    private int n;
    private  double[] x;
    private  double[] y;
    private  double[]  b, c, d;

    public cubicinterpolation(double[] xArray, double[] yArray)
    {
        x = xArray;
        y = yArray;
        n = x.Length;
        b = new double[n];
        c = new double[n - 1];
        d = new double[n - 1];
        AssignCoefficients();
    }

    private void AssignCoefficients()
    {

        double[] h = new double[n - 1];
        double[] p = new double[n - 1];
        for (int i = 0; i < n-1; i++)
        {
            h[i] = x[i + 1] - x[i];
            p[i] = (y[i + 1] - y[i])/h[i];
        }

        double[] B = new double[n];
        double[] D = new double[n];
        double[] Q = new double[n-1];
        double[] A = new double[n - 1];
        
        for (int i = 0; i < n-2; i++)
        {
            A[i + 1] = 1;
            D[i + 1] = 2 * h[i]/(h[i + 1]) + 2;
            Q[i + 1] = h[i] / h[i + 1];
            B[i + 1] = 3 * (p[i] + p[i + 1] * h[i] / h[i + 1]);
        }
        
        A[0] = 0;
        
        Q[0] = 1;
        Q[n - 2] = 0;

        D[0] = 2;
        D[n - 1] = 2;
        
        B[0] = 3 * p[0];
        B[n - 1] = 3 * p[n - 2];
        
        
        b = SolveTridiagonalSystem(A, D, Q, B);

        for (int i = 0; i < n-1; i++)
        {
            c[i] = (-2 * b[i] - b[i + 1] + 3 * p[i]) / h[i];
            d[i] = (b[i] + b[i + 1] - 2 * p[i]) / (h[i] * h[i]);
        }
    }

    public static double[] SolveTridiagonalSystem(double[] A, double[] D, double[] Q, double[] B)
    {
        int n = B.Length;
        double[] xvals = new double[n];
        for (int i = 1; i < n-1; i++)
        {
            double w = A[i] / D[i - 1];
            D[i] -= w * Q[i - 1];
            B[i] -= w * B[i - 1];
        }

        xvals[n - 1] = B[n - 1] / D[n - 1];
        for (int i = n-2; i >= 0; i--)
        {
            xvals[i] = (B[i] - Q[i] * xvals[i + 1]) / D[i];
        }
        
        return xvals;
    }
    
    public double Interpolate(double z)
    {
        int i = Binsearch(x, z);
        return ((d[i]*(z-x[i])+c[i])*(z-x[i])+b[i])*(z-x[i]) +y[i];
    }
    public double Derivative(double z)
    {
        int i = Binsearch(x, z);
        return b[i] + 2 * c[i]*(z - x[i]) + 3*d[i]*System.Math.Pow((z-x[i]),2);
    }
    public double Integrate(double z)
    {
        int index = Binsearch(x, z);
        double sum = 0;
        
        double deltaX = z - x[index];
        double integral = (((d[index] / 4 * deltaX +c[index]/3)*deltaX + b[index]/2)*deltaX + y[index])*deltaX;
        sum += integral;
        
        for (int i = 0; i < index; i++)
        {
            deltaX = x[i + 1] - x[i];
            integral = (((d[i] / 4 * deltaX +c[i]/3)*deltaX + b[i]/2)*deltaX + y[i])*deltaX;
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







/*
 Example code in the lecture notes
    D[0] = 2;
    D[n - 1] = 2;
    Q[0] = 1;
    
    for (int i = 0; i < n-2; i++)
    {
        D[i + 1] = 2 * h[i] / h[i + 1] + 2;
        Q[i + 1] = h[i] / h[i + 1];
        B[i + 1] = 3 * (p[i] + p[i + 1] * h[i] / h[i + 1]);
    }
    B[0] = 3 * p[0];
    B[n - 1] = 3 * p[n - 2];
    
    //Gauss elimination
    for (int i = 1; i < n; i++)
    {
        D[i] -= Q[i - 1] / D[i - 1];
        B[i] -= B[i - 1] / D[i - 1];
    }
    //Back substitution
    b[n - 1] = B[n - 1] / D[n - 1];
    for (int i = n-2; i >= 0; i--)
    {
        b[i] = (B[i] - Q[i] * b[i + 1]) / D[i];
    }
    */