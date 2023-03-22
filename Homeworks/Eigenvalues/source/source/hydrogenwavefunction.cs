public class hydrogenwavefunction
{
    public static void Main(string[] args)
    {
        var position = Linspace(0, 25, 1000);
        
        
        for (int i = 0; i < position.Length; i++)
        {
            System.Console.Write($"{position[i]} ");
            for (int j = 0; j < 3; j++)
            {
                var value = psi_R(position[i], j+1, 0);
                System.Console.Write($"{System.Math.Pow(value*position[i],2)} ");
            }
            System.Console.WriteLine();
        }


        double rmax = 25;
        double dr = 0.1;
        var hydrogenobject = new hydrogenhamiltonianclass(rmax, dr);
        var jacobiObject = new jacobieigenvaluealgorithm(hydrogenobject.Hamiltonian);
        jacobiObject.EigenValueDecomposition();
        var EigenVectorMatrix = jacobiObject.V.T;

        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("../data/NumericalSWaves.data"))
        {
            var S1 = EigenVectorMatrix.rows(0, 0);
            var S2 = EigenVectorMatrix.rows(1, 1);
            var S3 = EigenVectorMatrix.rows(2, 2);

            double f1, f2, f3;
            f1 = 4.6;
            f2 = 1.0/4.0;
            f3 = 1.0/16.0; 
            
                
            for (int i = 0; i < S1.size2; i++)
            {
                double r2 = hydrogenobject.r[i]*hydrogenobject.r[i] ;
                writer.WriteLine($"{hydrogenobject.r[i]} {f1*r2*S1[0,i]*S1[0,i]} {f2*r2*S2[0,i]*S2[0,i]} {f3*r2*S3[0,i]*S3[0,i]}");
            }
        }
        
        
    }
    
    public static double psi_R(double r, int n=1 , int l =0)
    {
        //Following the python example https://dpotoyan.github.io/Chem324/H-atom-wavef.html
        var coeff = System.Math.Sqrt(System.Math.Pow((2.0 / n), 3) * Factorial(n - l - 1) / (2.0 * n * Factorial(n + l)));
        var laguerre = GenLaguerre(n - l - 1, 2 * l + 1, 2.0 * r / n);
        return coeff * System.Math.Exp(-r / n) * System.Math.Pow((2.0 * r / n), l) * laguerre;
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

    public static int Factorial(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static double Laguerre(int n, double x)
    {
        if (n == 0)
        {
            return 1.0;
        }
        else if(n==1)
        {
            return 1.0 - x;
        }
        else
        {
            return ((2 * n - 1 - x) * Laguerre(n - 1, x) - (n - 1) * Laguerre(n - 2, x)) / n;
        }
    }

    public static double GenLaguerre(int n, int k, double x)
    {
        if (n == 0)
        {
            return 1.0;
        }
        else if (n == 1)
        {
            return 1.0 + k - x;
        }
        else
        {
            return ((2 * n + k - 1 - x) * GenLaguerre(n - 1, k, x) - (n + k - 1) * GenLaguerre(n - 2, k, x)) / n;        }
    }
    
}

