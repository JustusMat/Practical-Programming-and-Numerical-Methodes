public class hydrogendiagonalisation
{
    public static void Main(string[] args)
    {
        double rmax = 0;
        double dr = 0;
        

        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-rmax")
            {
                rmax = double.Parse(words[1]);
            }

            if (words[0] == "-dr")
            {
                dr = double.Parse(words[1]);
            }
        }
        
        
        var hydrogenObject = new hydrogenhamiltonianclass(rmax, dr);
        var jacobiObject = new jacobieigenvaluealgorithm(hydrogenObject.Hamiltonian);
        jacobiObject.EigenValueDecomposition();

        int ifac = 5;
        double solutionFactor = jacobiObject.V[ifac, 0]/(hydrogenObject.r[ifac]*System.Math.Exp(-hydrogenObject.r[ifac]));
        for (int i = 0; i < jacobiObject.Eigenvalues.size; i++)
        {
            //System.Console.WriteLine($"{hydrogenObject.r[i]} {jacobiObject.Eigenvalues[i]}");
            System.Console.WriteLine($"{hydrogenObject.r[i]} {F(hydrogenObject.r[i],factor:solutionFactor)}");
        }
    }

    public static double F(double z, double factor = 2.0)
    {
        return z * System.Math.Exp(-z) * factor;
    }
}