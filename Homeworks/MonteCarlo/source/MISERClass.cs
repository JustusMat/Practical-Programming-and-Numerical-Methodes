//Implementation of the Recursive Stratified Sampling Code MISER
//Credits go to: https://www.maxwellrules.com/math/montecarlo_integration.html
//and the provided Python code
//Notice that in the code above there must be included the volume term as otherwise ranges like [-1,1] only will be counted as [0,1]


using System.Linq;
public class MISER
{
    /*
     * MNBS: If less than MNBS evaluations we do vanilla MC
     * MNPTS: Minimum number of points used for variance
     * pfac: fraction of the remaining points used in each recursive call
     * alpha: factor used to estimate the optimal points to be used on each side of a bisection
     */
    private int MNBS;
    private int MNPTS;
    private double pfac;
    private double alpha;

    public MISER(int MNBSparam, int MNPTSparam, double pfacparam, double alhpaparam)
    {
        MNBS = MNBSparam;
        MNPTS = MNPTSparam;
        pfac = pfacparam;
        alpha = alhpaparam;
    }
    
    public (double ave, double var) Integrate(System.Func<double[], double> f, int Npoints, double[] xl, double[] xu)
    {
        int dim = xl.Length;
        double V = 1;
        for (int i = 0; i < dim; i++)
        {
            V *= xu[i] - xl[i];
        }
        var integralEstimate = MiserKernel(f, Npoints, xl, xu);
        return (integralEstimate.ave*V, integralEstimate.var*V*V);
    }

    private (double ave, double var) MiserKernel(System.Func<double[], double> f, int Npoints, double[] xl, double[] xu)
    {
        int ndims = xu.Length;
        if (Npoints < MNBS) //Plain monte carlo
        {
            (double ave, double var) = McKernel(f,Npoints,xl,xu);
            return (ave, var);
        }
        else //Recursion path
        {
            //number of points for variance exploration step
            int npre = (int)System.Math.Max(pfac * Npoints, MNPTS);
            //Best split
            (double[] newxu, double[] newxl, double var_l, double var_u) = SplitDomain(f, npre, xl, xu);

            int NpointsRemaining = Npoints - npre;
            int[] pointAllocations = AllocatePoints(var_l, var_u, NpointsRemaining);
            int Npointsl = pointAllocations[0];
            int Npointsu = pointAllocations[1];

            (double avel, double varlnew) = MiserKernel(f, Npointsl, xl, newxu);
            (double aveu, double varunew) = MiserKernel(f, Npointsu, newxl, xu);

            double ave = 0.5 * (avel + aveu);
            double var = 0.25 * (varlnew + varunew);
            return (ave, var);
        }
    }

    private (double ave, double var) McKernel(System.Func<double[], double> f, int NPoints, double[] xl, double[] xu)
    {
        int ndims = xu.Length;
        
        double[] evaluations = new double[NPoints];
        double[][] points = new double[ndims][];
        for (int i = 0; i < ndims; i++)
        {
            points[i] = new double[NPoints];
        }

        System.Random random = new System.Random();
        for (int i = 0; i < NPoints; i++)
        {
            for (int j = 0; j < ndims; j++)
            {
                points[j][i] = random.NextDouble() * (xu[j] - xl[j]) + xl[j];
            }
        }

        for (int i = 0; i < NPoints; i++)
        {
            evaluations[i] = f(points.Select(p => p[i]).ToArray());
        }
        
        double ave = Mean(evaluations);
        double var = Variance(evaluations) / NPoints;
        return (ave, var);

    }


    private (double[] newxu, double[] newxl, double varl, double varu) SplitDomain(System.Func<double[], double> f, 
        int Npoints, double[] xl, double[] xu)
    {
        double epsilon = 1e-25;
        int Ndims = xu.Length;
        
        double[][] points = new double[Npoints][];
        for (int i = 0; i < Npoints; i++)
        {
            points[i] = new double[Ndims];
        }
        for (int i = 0; i < Ndims; i++)
        {
            for (int j = 0; j < Npoints; j++)
            {
                points[j][i] = GetUniformRandomNumber(xu[i], xl[i]);;
            }
        }
        
        double[] fEvaluations = new double[Npoints];
        for (int i = 0; i < Npoints; i++)
        {
            fEvaluations[i] = f(points[i]);
        }
        
        //Find midpoints for the bisections
        double[] xmid = new double[Ndims];
        for (int i = 0; i < Ndims; i++)
        {
            xmid[i] = 0.5 * (xl[i] + xu[i]);
        }
        
        //Storing variances
        double[,] stdSubRegions = new double[Ndims, 2];
        for (int i = 0; i < Ndims; i++)
        {
            bool[] mskLU = new bool[Npoints];
            for (int j = 0; j < Npoints; j++)
            {
                mskLU[j] = points[j][i] < xmid[i];
            }

            double stdlow = StandardDeviationSubset(fEvaluations, mskLU);
            double stdup = StandardDeviationSubset(fEvaluations, mskLU, invert: true);
            stdSubRegions[i, 0] = stdlow;
            stdSubRegions[i, 1] = stdup;
        }

        int bisectionDim = FindMinIndex(stdSubRegions);
        double varlow = stdSubRegions[bisectionDim, 0] * stdSubRegions[bisectionDim, 0] + epsilon;
        double varup = stdSubRegions[bisectionDim, 1] * stdSubRegions[bisectionDim, 1] + epsilon;
        double[] newxup = (double[])xu.Clone();
        double[] newxlow = (double[])xl.Clone();
        newxup[bisectionDim] = xmid[bisectionDim];
        newxlow[bisectionDim] = xmid[bisectionDim];
        return (newxup, newxlow, varlow, varup);
    }

    private int[] AllocatePoints(double varlow, double varup, int NpointsRemaining)
    {
        double exponent = 1.0 / (alpha + 1);
        double lowerFraction = System.Math.Pow(varlow, exponent) /
                               (System.Math.Pow(varlow, exponent) + System.Math.Pow(varup, exponent));
        int NpointsLow = (int)(MNPTS + (NpointsRemaining - 2 * MNPTS) * lowerFraction);
        int NpointsUp = NpointsRemaining - NpointsLow;
        return new int[] { NpointsLow, NpointsUp };
    }

    
    //Helper functions
    //----------------------------------------------------------------------------------------------
    private double GetUniformRandomNumber(double min, double max)
    {
        System.Random random = new System.Random();
        return random.NextDouble() * (max - min) + min;
    }

    private double StandardDeviationSubset(double[] array, bool[] mask, bool invert = false)
    {
        double sum = 0.0;
        double sum2 = 0.0;
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (mask[i] != invert)
            {
                sum += array[i];
                sum2 += array[i] * array[i];
                count++;
            }
        }

        if (count ==0)
        {
            return 0.0;
        }

        double mean = sum / count;
        double variance = (sum2 / count) - (mean * mean);
        return System.Math.Sqrt(variance);
    }

    private int FindMinIndex(double[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        int minIndex = 0;
        double minValue = array[0, 0];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (array[i,j] < minValue)
                {
                    minIndex = i;
                    minValue = array[i, j];
                }
            }
        }

        return minIndex;
    }

    private static double Mean(double[] array)
    {
        double sum = 0.0;
        foreach (var value in array)
        {
            sum += value;
        }

        return sum / array.Length;
    }

    private static double Variance(double[] array)
    {
        double mean = Mean(array);
        double sum2diff = 0.0;
        foreach (var value in array)
        {
            double diff = value - mean;
            sum2diff += diff * diff;
        }

        return sum2diff / array.Length;
    }
}        