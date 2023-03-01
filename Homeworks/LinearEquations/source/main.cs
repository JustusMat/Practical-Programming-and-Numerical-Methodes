public class Program
{
    public static void Main(string[] args)
    {
        //A part
        System.Console.WriteLine(" A ");
        Line();
        var A = new matrix("1 2; 3 -5");
        System.Console.WriteLine("Matrix A:");
        A.print();
        Line(30);
        
        var matrixsolver = new QRGS(A);
        System.Console.WriteLine("Q matrix through decomposition:");
        matrixsolver.Q.print();
        Line(30);
        System.Console.WriteLine("R matrix through decomposition:");
        matrixsolver.R.print();
        Line(30);
        
        System.Console.WriteLine("Demonstration of orthogonality (Q^TQ = 1):");
        var Identity = matrixsolver.Q * matrixsolver.Q.T;
        Identity.print();
        Line(30);
        
        System.Console.WriteLine("Demonstration of decomposition elements constructing matrix A (QR=A):");
        var CompositionCheck = matrixsolver.Q * matrixsolver.R;
        CompositionCheck.print();
        Line(30);
        double determinat = matrixsolver.Determinat();
        System.Console.WriteLine($"|det(A)|={determinat}");
        Line(30);
        var b = new vector(4, 1);
        System.Console.WriteLine("Solving with vector b given by:");
        b.print();
        var x = matrixsolver.Solve(b);
        System.Console.WriteLine("Solution is x=");
        x.print();
        Line(30);
        System.Console.WriteLine("Evaluating A*x (=b):");
        var bDemonstration = A * x;
        bDemonstration.print();
        Line(30);
        
        
        //B part
        System.Console.WriteLine(" B ");
        Line();
        System.Console.WriteLine("Demonstration of inverse A:");
        var Ainv = matrixsolver.Inverse();
        Ainv.print();
        Line(30);
        System.Console.WriteLine("Demonstrating the identity condition (A*Ainv=1)");
        var identityDemonstration = A * Ainv;
        identityDemonstration.print();
        Line(30);
        System.Console.WriteLine("Done");
        
    }


    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('-',n));
    }
}