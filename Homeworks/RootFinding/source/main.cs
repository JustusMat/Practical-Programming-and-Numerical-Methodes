public class Program
{

    public static void Main(string[] args)
    {
        vector x = new vector(10.0,-10.0);
       /*
        vector result = f(x);
        matrix J = rootfindingclass.Jacobian(f, x);
        System.Console.WriteLine("Found the Jacobian of f");
        J.print();
        matrix JX = rootfindingclass.Jacobian(fX, x);
        System.Console.WriteLine("Found the Jacobian of fX");
        JX.print();
        Line();
        System.Console.WriteLine("Estimating the inverse of the Jacobian of f");
        System.Console.WriteLine($"f(x,y)= x² + y², (x0,y0)=({x[0]},{x[1]})");
        (new QRGS(J.T).PseudoInverse()).print();
        System.Console.WriteLine("Second estimation of a Jacobian");
        System.Console.WriteLine("Estimating the inverse of the Jacobian of fX");
        (new QRGS(JX.T).PseudoInverse()).print();
        Line();
        */ 
       Line();
       System.Console.WriteLine("Testing with a simple function");
        System.Console.WriteLine($"f(x,y)= x² + y², (x0,y0)=({x[0]},{x[1]})");
        (vector fminimum, int fiterations) = rootfindingclass.NewtonsMethod(f, x,lambdamin:1/64.0,maxIterations:(int)10e5);
        System.Console.WriteLine($"The minimum of f in {fiterations} iterations at:");
        fminimum.print();
        Line();
        (vector RosenbrockMin, int RosenbrockIterations) = rootfindingclass.NewtonsMethod(f2, x, lambdamin:1/64.0,maxIterations:(int)10e5);
        System.Console.WriteLine("PseudoInverse / Gradient descent method");
        System.Console.WriteLine($"The Rosenbrock minimum is in {RosenbrockIterations} iterations at:");
        RosenbrockMin.print();
        Line();
        System.Console.WriteLine("Newton's method with numerical Jacobian and back-tracking linesearch");
        (vector RosenbrockMin2, int Rosenbrock2Iterations) = rootfindingclass.NewtonsMethod(f3, x, lambdamin: 1 / 64.0, maxIterations: (int)10e5);
        System.Console.WriteLine($"The Rosenbrock minimum is in {Rosenbrock2Iterations} iterations at:");
        RosenbrockMin2.print();
        Line();
    }
    
    public static vector f(vector x)
    {
        vector result = new vector(x[0] * x[0] + x[1]*x[1]);
        return result;
    }

    public static vector fX(vector x)
    {
        vector result = new vector(x[0] * x[0] * x[1], 5 * x[0] + System.Math.Sin(x[1]));
        return result;
    }
    
    public static vector f2(vector x)
    {
        double a = 1; 
        double b = 100;
        double c = System.Math.Pow(a - x[0], 2);
        double d = b*System.Math.Pow(x[1] - x[0] * x[0], 2);
        return new vector(c +d);
    }

    public static vector f3(vector x)
    {
        double dfdx = -2 * (1 - x[0]) - 200 * x[0] * (x[1] - x[0] * x[0]);
        double dfdy = 200 * (x[1] - x[0] * x[0]);
        return new vector(dfdx, dfdy);
    }
    
    
    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('─',n));
    }
}