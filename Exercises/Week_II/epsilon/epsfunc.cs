public class Epsfunc
{
    public static bool Approx(double x, double y, double acc = 1e-9,double eps=1e-9)
    {
        //System.Console.WriteLine($"Accuracy: {acc}");
        //System.Console.WriteLine($"Epsilon: {eps}");
        if (System.Math.Abs(y - x) < acc)
        {
            return true;
        }

        if (System.Math.Abs(y-x) < System.Math.Max(System.Math.Abs(x),System.Math.Abs(y)*eps))
        {
            return true;
        }

        return false;
    }
    
    public static System.Tuple<double, double> TinyMachineEpsilon()
    {
        int n = (int)1e6;
        double epsilon = System.Math.Pow(2, -52);
        double tiny = epsilon/2;
        double sumA = 0, sumB = 0;
        
        sumA += 1;
        for (int i = 0; i < n; i++)
        {
            sumA += tiny;
            sumB += tiny;
        }
        sumB += 1;
        return System.Tuple.Create(sumA, sumB);
    }
    public static double MachineEpsilonDouble()
    {
        double x = 1;
        while (1+x != 1)
        {
            x /= 2;
        }
        x *= 2;
        return x;
    }
    public static float MachineEpsilonFloat()
    {
        float machEps = 1.0f;

        do {
            machEps /= 2.0f;
        }
        while ((float)(1.0 + machEps) != 1.0);
        
        machEps *=2F;
        return machEps;
    }
}