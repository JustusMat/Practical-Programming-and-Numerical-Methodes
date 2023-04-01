using System.Xml;

public class rootfindingclass
{
    public static (vector,int) NewtonsMethod(System.Func<vector, vector> f, vector x, double epsilon = 1e-12, 
        int maxIterations = 1000, double alpha = 1.0, double beta = 0.5, double lambdamin = 1.0/32.0)
    {
        int n = x.size;
        vector dx = new vector(n);
        vector F = f(x);
    
        int iterations = 0;
        while (F.norm() > epsilon && iterations < maxIterations)
        {
            matrix J = Jacobian(f, x);
            //Using Pseudoinverse of the Jacobian J
            var PseudoInverseJacobian = new matrix(J.size1,J.size2);
            if (J.size1 != J.size2)
            {
                PseudoInverseJacobian = new QRGS(J.T).PseudoInverse().T;
                dx = -PseudoInverseJacobian * F;
            }
            else
            {
                dx = -new QRGS(J).Solve(F);
            }

            //backtracking line search
            double lambda = 1.0;
            while (f(x+lambda*dx).norm() >= (1-alpha*lambda)*F.norm() && lambda>lambdamin)
            {                
                lambda *= beta; 
            }

            x += lambda * dx;
            F = f(x);
            iterations += 1;
        }
        if (iterations >= maxIterations)
        {
            System.Console.WriteLine("Maximum number of iterations exceeded");
        }
        return (x,iterations);
    }
    
    public static matrix Jacobian(System.Func<vector, vector> f, vector x, double epsilon = 1e-8)
    {
        int n = x.size;
        int m = f(x).size;
        matrix J = new matrix(m, n);
        vector y = f(x);
        vector yEpsilon;
        
        
        for (int i = 0; i < n; i++)
        {
            
            double xi = x[i];
            double delta = System.Math.Abs(xi) * epsilon;
            double dx = System.Math.Max(delta * System.Math.Abs(xi), delta);

            x[i] = xi + dx;

            yEpsilon = f(x);

            for (int j = 0; j < m; j++)            
            {
                J[j,i] = (yEpsilon[j] - y[j]) / dx;
            }

            x[i] = xi;
            
        }
        return J;
    }
}