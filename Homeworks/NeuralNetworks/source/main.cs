public class Program
{
    public static void Main(string[] args)
    {
        var xtrain = Linspace(-1.0, 1.0, 12);
        var ytrain = new double[xtrain.Length];
        for (int i = 0; i < xtrain.Length; i++)
        {
            ytrain[i] = g(xtrain[i]);
        }
        
        //Network stuff
        int nHiddenNodes = 6;
        var myNetwork = new SimpleNeuralNetwork(nHiddenNodes, activation:"Gaussian Wavelet");
        Line();
        System.Console.WriteLine($"Training a network with {nHiddenNodes} hidden neurons");
        System.Console.WriteLine("with function g(x) = cos(5x-1)*exp(-x*x)");
        myNetwork.Train(new vector(xtrain), new vector(ytrain));
        Line();
        System.Console.WriteLine("x \t g(x) \t Response(x)");
        Line();
        for (int i = 0; i < xtrain.Length; i++)
        {
            System.Console.WriteLine($"{xtrain[i]} {ytrain[i]} {myNetwork.Response(xtrain[i])}");
        }
        
        
        //For plotting
        //--------------------------------------------------------------------------------------------------------------
        var X = Linspace(-1.0, 1.0, 500);
        string filePath = "../data/gData.data";
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
        {
            for (int i = 0; i < X.Length; i++)
            {
                writer.WriteLine($"{X[i]} {g(X[i])} {myNetwork.Response(X[i])}");
            }
        }
        filePath = "../data/gDataDerivative.data";
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
        {
            for (int i = 0; i < X.Length; i++)
            {
                writer.WriteLine($"{X[i]} {dgdx(X[i])} {myNetwork.ResponseDerivative(X[i])}");
            }
        }
        filePath = "../data/gDataDerivative2.data";
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
        {
            for (int i = 0; i < X.Length; i++)
            {
                writer.WriteLine($"{X[i]} {d2gdx2(X[i])} {myNetwork.ResponseSecondDerivative(X[i])}");
            }
        }
        filePath = "../data/gDataAntiDerivative.data";
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
        {
            for (int i = 0; i < X.Length; i++)
            {
                //To do... Need to implement the complex analytical integral of g(x)
                writer.WriteLine($"{X[i]} {G(X[i])} {-myNetwork.ResponseAntiDerivative(X[i])}");
            }
        }
    }


    public static double g(double x)
    {
        return System.Math.Cos(5 * x - 1) * System.Math.Exp(-x * x);
    }

    public static double dgdx(double x)
    {
        double a = 5 * System.Math.Sin(5 * x - 1);
        double b = 2 * x * System.Math.Cos(5 * x - 1);
        return -System.Math.Exp(-x * x) * (a + b);
    }
    public static double d2gdx2(double x)
    {
        double a = 20 * x * System.Math.Sin(5 * x - 1);
        double b = (4 * x * x - 27) * System.Math.Cos(5 * x - 1);
        return System.Math.Exp(-x * x) * (a + b);
    }

    public static double G(double x)
    {
        return integrator.CCIntegrate(g,-1,x) - integrator.CCIntegrate(g,-1,10000);
    }
    
    
    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('─',n));
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