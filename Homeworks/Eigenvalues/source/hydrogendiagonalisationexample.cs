using System.Reflection;

public class hydrogendiagonalisationexample
{
    public static void Main(string[] args)
    {
        double rmax = 10;
        double dr = 1.0;
        
        var hydrogenObject = new hydrogenhamiltonianclass(rmax, dr);
        Line(100);
        Line(100);
        System.Console.WriteLine("Evaluating at points");
        hydrogenObject.r.print();
        Line(100);
        System.Console.WriteLine("Hamiltonian given by");
        hydrogenObject.Hamiltonian.print();
        Line(100);
        var jacobiObject = new jacobieigenvaluealgorithm(hydrogenObject.Hamiltonian);
        jacobiObject.EigenValueDecomposition();
        System.Console.WriteLine("Computed eigenvalues");
        jacobiObject.Eigenvalues.print();
        Line(100);
    }
    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('-',n));
    }
}