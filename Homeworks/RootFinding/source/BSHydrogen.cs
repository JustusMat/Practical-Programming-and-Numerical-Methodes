public class BSHydrogen
{
    public static void Main(string[] args)
    {
        double acc = 1e-1;
        double eps = 1e-6;
        double rmin = 0.001;
        double rmax = 8.0;
        vector E0initial = new vector(-0.7);
        
        var resultE0 = rootfindingclass.NewtonsMethod(M(rmin, rmax,accuracy:acc,epsilon:eps), E0initial);
        System.Console.WriteLine($"The minimised E0 in {resultE0.Item2} iterations is {resultE0.Item1[0]}");
        genlist<double> rsNew = new genlist<double>();
        genlist<vector> ysNew = new genlist<vector>();
        vector f0plot = new vector(rmin-rmin*rmin, 1-2*rmin);
        var (xsGenlist, ysGenlist) = odeclass.ImprovedDriver(DefineHydrogenProblem(resultE0.Item1[0]), rmin, f0plot, rmax,
            acc: acc, eps: eps, xlist: rsNew, ylist: ysNew);
        for (int i = 0; i < xsGenlist.Size; i++)
        {
            string vals = $"{xsGenlist[i]} ";
            for (int j = 0; j < ysGenlist[i].size; j++)
            {
                vals += $"{ysGenlist[i][j]} ";
            }
            System.Console.WriteLine(vals);
        }
    }

    public static System.Func<vector, vector> M(double rmin, double rmax, double accuracy = 1e-4, double epsilon = 1e-4)
    {
        vector f0 = new vector(rmin - rmin * rmin, 1 - 2 * rmin);
        return delegate(vector E)
        {
            var result = odeclass.ImprovedDriver(DefineHydrogenProblem(E[0]), rmin, f0, rmax, 
                acc:accuracy, eps:epsilon);
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
}