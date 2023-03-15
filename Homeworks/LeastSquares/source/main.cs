using System.Linq;
public class Program
{
    public static void Main(string[] args)
    {
        Line();
        Line();

        string infile = null;
        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-input")
            {
                infile = words[1];
                System.Console.WriteLine($"Found infile: {infile}");
            }
        }
        if (infile == null)
        {
            return;
        }

        var data = new dataloader(infile);

        var Time = data.getColumn(0);
        var Activity = data.getColumn(1);
        var Uncertainty = data.getColumn(2);

        
        for (int i = 0; i < Time.Length; i++)
        {
            Uncertainty[i] = Uncertainty[i] / Activity[i];
            Activity[i] = System.Math.Log(Activity[i]);
        }
        
        var fs = new System.Func<double,double>[] { z => 1.0, z => -z };
        var (c, cUncertainties, S, covarianceMatrix) = leastsq.lsfit(fs, Time, Activity,Uncertainty);
        
        
        
        
        
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/FitResults.data"))
        {
            for (int i = 0; i < c.size; i++)
            {
                writer.WriteLine($"{c[i]} {cUncertainties[i]}");
            }
        }


        
        var range = Linspace(Time.Min(),Time.Max(),1000);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/data.data"))
        {
            for (int i = 0; i < range.Length; i++)
            {
                writer.WriteLine($"{range[i]} {Fc(fs,c,range[i])}");
            }
        }
        
        System.Console.WriteLine("Fitted parameters and uncertainties");
        c.print();
        cUncertainties.print();
        Line();
        System.Console.WriteLine("S-Matrix");
        S.print();
        Line();
        System.Console.WriteLine("Covariance matrix");        
        covarianceMatrix.print();
        Line();
        System.Console.WriteLine("The half life of ThX alias 224Ra is: t_(1/2) = 3.66(4) d ");
        //Error propagation :)
        var uncertainty = System.Math.Round(System.Math.Log(2) * System.Math.Pow(System.Math.Round(1 / c[1], 2), 2) * cUncertainties[1], 2);
        System.Console.WriteLine($"The fitted half life is: t_(1/2) = {System.Math.Round(System.Math.Log(2)/c[1],2)} +- {uncertainty} d");
    }

    public static double Fc(System.Func<double, double>[] fs, vector c, double x)
    {
        double s = 0;
        for (int i = 0; i < fs.Length; i++)
        {
            s += c[i] * fs[i](x);
        }
        return System.Math.Exp(s);
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
    
    public static void Line(int n = 50)
    {
        System.Console.WriteLine(new string('-', n));
    }
}