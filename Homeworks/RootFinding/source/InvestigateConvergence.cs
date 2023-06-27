public class InvestigateConvergence
{
    public static void Main(string[] args)
    {
        double acc = 1e-1;
        double eps = 1e-6;
        double rmin = 0.001;
        double rmax = 8.0;
        vector E0initial = new vector(-0.7);
        
        double[] accuracyRange = Linspace(acc, 1e-6, 25);
        System.Console.WriteLine("Testing accuracy convergence...");
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./accuracyConvergence.data"))
        {
            for (int i = 0; i < accuracyRange.Length; i++)
            {
                var resultE0 = rootfindingclass.NewtonsMethod(M(rmin, rmax, accuracy: accuracyRange[i], epsilon: eps), E0initial);
                writer.WriteLine($"{accuracyRange[i]} {resultE0.Item1[0]}");
            }
        }
        

        int N = 9;
        double[] epsilonRange = new double[N];
        for (int i = 0; i < N; i++)
        {
            epsilonRange[i] = 1.0/(i + 1.0);
        }
        System.Console.WriteLine("Testing epsilon convergence...");
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./epsilonConvergence.data"))
        {
            for (int i = 0; i < epsilonRange.Length; i++)
            {
                var resultE0 = rootfindingclass.NewtonsMethod(M(rmin, rmax, accuracy: acc, epsilon: epsilonRange[i]), E0initial);
                writer.WriteLine($"{epsilonRange[i]} {resultE0.Item1[0]}");
            }
        }
        
        System.Console.WriteLine("Testing rmin convergence...");
        double[] rminRange = Linspace(0.1, 1e-9, 25);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./rminConvergence.data"))
        {
            for (int i = 0; i < rminRange.Length; i++)
            {
                var resultE0 = rootfindingclass.NewtonsMethod(M(rminRange[i], rmax, accuracy: acc, epsilon: eps), E0initial);
                writer.WriteLine($"{rminRange[i]} {resultE0.Item1[0]}");
            }
        }
        
        System.Console.WriteLine("Testing rmax convergence...");
        double[] rmaxRange = Linspace(1, rmax, 25);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("./rmaxConvergence.data"))
        {
            for (int i = 0; i < rmaxRange.Length; i++)
            {
                var resultE0 = rootfindingclass.NewtonsMethod(M(rmin, rmaxRange[i], accuracy: acc, epsilon: eps), E0initial);
                writer.WriteLine($"{rmaxRange[i]} {resultE0.Item1[0]}");
            }
        }
    }

    public static System.Func<vector, vector> M(double rmin, double rmax, double accuracy = 1e-4, double epsilon = 1e-4)
    {
        vector f0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin);
        return delegate(vector E)
        {
            var result =
                odeclass.ImprovedDriver(DefineHydrogenProblem(E[0]), rmin, f0, rmax, acc: accuracy, eps: epsilon);
            return new vector(result.ylist[0][0]);
        };
    }

    public static System.Func<double, vector, vector> DefineHydrogenProblem(double V)
    {
        return delegate(double r, vector y)
        {
            double dfdr = y[1];
            double dfdrdr = -2 * (V + 1 / r) * y[0];
            return new vector(dfdr, dfdrdr);
        };
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