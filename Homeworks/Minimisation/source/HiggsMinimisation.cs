public class HiggsMinimisation
{
    public static void Main(string[] args)
    {
        string dataPath = null;
        foreach (string arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-input")
            {
                dataPath = words[1];
                System.Console.Error.WriteLine($"Found infile: {dataPath}");
            }
        }
        
        var separators = new char[] {' ','\t'};
        var options = System.StringSplitOptions.RemoveEmptyEntries;
        
        if (dataPath == null) return;
        using (var reader = new System.IO.StreamReader(dataPath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(separators, options);
                energy.Add(double.Parse(values[0]));
                signal.Add(double.Parse(values[1]));
                error.Add(double.Parse(values[2]));
            }
        }
        vector x0 = new vector(10.0, 12.0, 15.0); 
        var HiggsMinimisationObject = new minimisationclass(chi2min, x0);
        vector xsolution = HiggsMinimisationObject.MinimiseQN(maxIteration:(int)10e4);
        System.Console.WriteLine($"{xsolution[0]} {xsolution[1]} {xsolution[2]}");
        
        System.Console.Error.WriteLine($"The minimisation succeded in {HiggsMinimisationObject.iteration} iterations at");
        System.Console.Error.WriteLine($"A: {xsolution[0]}");
        System.Console.Error.WriteLine($"m: {xsolution[1]}");
        System.Console.Error.WriteLine($"Gamma: {xsolution[2]}");
        
    }

    static System.Collections.Generic.List<double> energy = new System.Collections.Generic.List<double>();
    static System.Collections.Generic.List<double> signal = new System.Collections.Generic.List<double>();
    static System.Collections.Generic.List<double> error = new System.Collections.Generic.List<double>();
    
    public static double chi2min(vector p)
    {
        return ChiSquare(energy, signal, error, p, BreitWigner);
    }
    
    static double ChiSquare(System.Collections.Generic.List<double> x, System.Collections.Generic.List<double> y,
        System.Collections.Generic.List<double> yerr, vector parameters, System.Func<double, vector, double> f)
    {
        double chi2 = 0.0;
        for (int i = 0; i < x.Count; i++)
        {
            double yExpected = f(x[i],parameters);
            double diff = y[i] - yExpected;
            double sigma = yerr[i];
            chi2 += diff * diff / (sigma * sigma);
        }

        return chi2;
    }
    
    public static double BreitWigner(double E,vector p)
    {
        double A = p[0];
        double m = p[1];
        double Gamma = p[2];
        double denominator = System.Math.Pow(E - m, 2) + Gamma * Gamma / 4.0;
        return A / denominator;
    }
}