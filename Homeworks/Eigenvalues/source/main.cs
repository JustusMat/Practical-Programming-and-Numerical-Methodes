public class Program
{
    public static void Main(string[] args)
    {
        Line();
        Line();
        var A = new randommatrix(3, 3);
        A.RandomSymmetricEntries();
        System.Console.WriteLine("Random matrix A given by");
        A.print();
        Line();
        var jacobiAlgorithmObject = new jacobieigenvaluealgorithm(A);
        jacobiAlgorithmObject.EigenValueDecomposition(tolerance:1e-12);
        System.Console.WriteLine("Eigenvalues");
        jacobiAlgorithmObject.Eigenvalues.print();
        System.Console.WriteLine($"Applied sweeps = {jacobiAlgorithmObject.sweeps}");
        Line();
        System.Console.WriteLine("Matrix D given by ");
        jacobiAlgorithmObject.DMatrix.print();
        Line();
        System.Console.WriteLine("Demonstration of V^T A V = D");
        var dvalidation = jacobiAlgorithmObject.V.T * A * jacobiAlgorithmObject.V; 
        dvalidation.print();
        Line();
        System.Console.WriteLine("Demonstration of V D V^T = A");
        var Avalidation = jacobiAlgorithmObject.V * jacobiAlgorithmObject.DMatrix * jacobiAlgorithmObject.V.T;
        Avalidation.print();
        Line();
        System.Console.WriteLine("Demonstration of orthogonality V^T V = V V^T = 1");
        var IdentityvalidationOne = jacobiAlgorithmObject.V.T * jacobiAlgorithmObject.V;
        var IdentityvalidationTwo = jacobiAlgorithmObject.V * jacobiAlgorithmObject.V.T;
        IdentityvalidationOne.print();
        IdentityvalidationTwo.print();
        Line();
    }



    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('-',n));
    }
}