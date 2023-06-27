public class MonteCarloConvergenceTest
{
    public static void Main(string[] args)
    {
        System.Func<vector, double> cFunc = (vector r) =>
        {
            return System.Math.Sqrt(System.Math.Pow(r[0], 2) + System.Math.Pow(r[1], 2));
        };
        vector a = new vector(-1, -1);
        vector b = new vector(1, 1);


        int start = 100;
        long stop = (long)10e6;
        int[] NpointsArray = LogLinspace(start, stop, 15);

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./NpointsPlain.data"))
        {
            for (int i = 0; i < NpointsArray.Length; i++)
            {
                montecarlo MonteCarloObject = new montecarlo(cFunc, a, b, NpointsArray[i]);
                (double result, double error) = MonteCarloObject.PlainMCParallel();
                writer.WriteLine($"{NpointsArray[i]} {result} {error}");
            }
        }
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./NpointsQuasi.data"))
        {
            for (int i = 0; i < NpointsArray.Length; i++)
            {
                montecarlo MonteCarloObject = new montecarlo(cFunc, a, b, NpointsArray[i]);
                (double result, double error) = MonteCarloObject.QuasiMCParallel();
                writer.WriteLine($"{NpointsArray[i]} {result} {error}");
            }
        }

        System.Func<double[], double> Cfunc = (double[] r) =>
        {
            return System.Math.Sqrt(System.Math.Pow(r[0], 2) + System.Math.Pow(r[1], 2));
        };
        double[] xlow = new double[2] { -1.0, -1.0};
        double[] xup = new double[2] { 1.0, 1.0 };
        MISER miserObject = new MISER(32 * 16 * 2, 16 * 2, 0.1, 2.0);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./NpointsStratifiedSampling.data"))
        {
            for (int i = 0; i < NpointsArray.Length; i++)
            {
                System.Console.Error.WriteLine($"Doing points {NpointsArray[i]}");
                (double StratifiedSamplingResult, double StratifiedSamplingError) =
                    miserObject.Integrate(Cfunc, NpointsArray[i], xlow, xup);  
                writer.WriteLine($"{NpointsArray[i]} {StratifiedSamplingResult} {System.Math.Sqrt(StratifiedSamplingError)}");
            }
        }
    }

    public static int[] LogLinspace(double start, double stop, int num)
    {
        int[] result = new int[num];
        double logStart = System.Math.Log10(start);
        double logEnd = System.Math.Log10(stop);

        for (int i = 0; i < num; i++)
        {
            double fraction = (double)i / (num - 1);
            double logValue = logStart + fraction * (logEnd - logStart);
            int value = (int)System.Math.Round(System.Math.Pow(10, logValue));
            result[i] = value;
        }

        return result;
    }
}