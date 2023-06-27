public class StratSamp
{
    static double integrand(int dim, double[] x)
    {
        double sumOfSquares = 0;
        for (int i = 0; i < dim; i++)
        {
            sumOfSquares += System.Math.Pow(x[i], 2);
        }
        return System.Math.Sqrt(sumOfSquares);
    }

    
    public static void Main(string[] args)
    {
        int dim = 2; // dimension of the domain
        double[] a = {-1, -1}; // lower bounds of the domain
        double[] b = {1, 1}; // upper bounds of the domain
        double acc = 1e-6; // absolute error tolerance
        double eps = 1e-6; // relative error tolerance
        int nreuse = 0; // number of samples to reuse
        double meanreuse = 0; // mean of reused samples
        System.Console.WriteLine("Doing heavy computations. Please be patient...");
        double result = StratifiedSamplingX(dim, integrand, a, b, acc, eps, nreuse, meanreuse);
        System.Console.WriteLine($"The result is {result}");
    }

    
    public static double StratifiedSamplingX(
        int dim, System.Func<int, double[], double> f, double[] a, double[] b,
        double acc, double eps, int nReuse, double meanReuse)
    {
        
        int N = 16 * dim;
        double V = 1.0;
        for (int k = 0; k < dim; k++)
        {
            V *= b[k] - a[k];
        }

        int[] nLeft = new int[dim];
        int[] nRight = new int[dim];
        
        double[] x = new double[dim];
        double[] meanLeft = new double[dim];
        double[] meanRight = new double[dim];
        
        
        double mean = 0.0;

        for (int i = 0; i < N; i++)
        {
            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + (new System.Random().NextDouble() * (b[k] - a[k]));
            }

            double fx = f(dim, x);
            mean += fx;
            for (int k = 0; k < dim; k++)
            {
                if (x[k] > (a[k] + b[k]) / 2.0)
                {
                    nRight[k]++;
                    meanRight[k] += fx;
                }
                else
                {
                    nLeft[k]++;
                    meanLeft[k] += fx;
                }
            }
        }

        mean /= N;
        for (int k = 0; k < dim; k++)
        {
            meanLeft[k] /= nLeft[k];
            meanRight[k] /= nRight[k];
        }

        int kDiv = 0;
        double maxVar = 0.0;
        for (int k = 0; k < dim; k++)
        {
            double var = System.Math.Abs(meanRight[k] - meanLeft[k]);
            if (var > maxVar)
            {
                maxVar = var;
                kDiv = k;
            }
        }

        double integral = (mean * N + meanReuse * nReuse) / (N + nReuse) * V;
        double error = System.Math.Abs(meanReuse - mean) * V;
        double tolerance = acc + System.Math.Abs(integral) * eps;

        if (error < tolerance)
        {
            return integral;
        }

        double[] a2 = new double[dim];
        double[] b2 = new double[dim];
        for (int k = 0; k < dim; k++)
        {
            a2[k] = a[k];
            b2[k] = b[k];
        }
        a2[kDiv] = (a[kDiv] + b[kDiv]) / 2.0;
        b2[kDiv] = (a[kDiv] + b[kDiv]) / 2.0;

        double integralLeft = StratifiedSamplingX(
            dim, f, a, b2, acc / System.Math.Sqrt(2), eps, nLeft[kDiv], meanLeft[kDiv]);

        double integralRight = StratifiedSamplingX(
            dim, f, a2, b, acc / System.Math.Sqrt(2), eps, nRight[kDiv], meanRight[kDiv]);

        return integralLeft + integralRight;
    }
    
    public static double StratifiedSampling(int dim, System.Func<vector, double> f, vector a, vector b,
        int nReuse, double meanReuse, double epsilon = 1e-4, double accuracy = 1e-4)
    {
        int N = 16 * dim;
        double V = 1.0;
        for (int k = 0; k < dim; k++)
        {
            V *= b[k] - a[k];
        }

        int[] nLeft = new int[dim];
        int[] nRight = new int[dim];
        vector x = new vector(dim);
        vector meanLeft = new vector(dim);
        vector meanRight = new vector(dim);
        double mean = 0.0;

        for (int i = 0; i < dim; i++)
        {
            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + (new System.Random().NextDouble() * (b[k] - a[k]));
            }

            double fx = f(x);
            mean += fx;

    
            for (int k = 0; k < dim; k++)
            {
                if (x[k] > (a[k] + b[k]) / 2.0)
                {
                    nRight[k]++;
                    meanRight[k] += fx;
                }
                else
                {
                    nLeft[k]++;
                    meanLeft[k] += fx;
                }
            }
        }

        
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        mean /= N;
        for (int k = 0; k < dim; k++)
        {
            meanLeft[k] /= nLeft[k];
            meanRight[k] /= nRight[k];
        }
        int kDiv = 0;
        double maxVar = 0.0;
        for (int k = 0; k < dim; k++)
        {
            double difference = System.Math.Abs(meanRight[k] - meanLeft[k]);
            if (difference > maxVar)
            {
                maxVar = difference;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        double integral = (mean * N + meanReuse * nReuse) / (N + nReuse) * V;
        double error = System.Math.Abs(meanReuse - mean) * V;
        double tolerance = accuracy + System.Math.Abs(integral) * epsilon;

        if (error < tolerance)
        {
            System.Console.WriteLine($"Returning the integral, {integral}");
            return integral;
        }
        vector a2 = a.copy();
        vector b2 = b.copy();

        a2[kDiv] = (a[kDiv] + b[kDiv]) / 2.0;
        b2[kDiv] = (a[kDiv] + b[kDiv]) / 2.0;

        double integralLeft = StratifiedSampling(dim, f, a, b2, nLeft[kDiv],
            meanLeft[kDiv], epsilon: epsilon, accuracy: accuracy / System.Math.Sqrt(2));
        double integralRight = StratifiedSampling(dim, f, a2, b, nRight[kDiv],
            meanRight[kDiv], epsilon: epsilon, accuracy: accuracy / System.Math.Sqrt(2));

        return integralLeft + integralRight;
    }
    
    
}