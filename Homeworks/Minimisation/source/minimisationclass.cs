public class minimisationclass
{
    public int iteration;
    public static vector x0;
    public static System.Func<vector, double> minFunction;

    public minimisationclass(System.Func<vector,double> f, vector initialVector)
    {
        minFunction = f;
        x0 = initialVector;
    }
    
    public vector MinimiseQN(double tolerance=1e-12,double epsilon=1e-8, int maxIteration=1000)
    {
        //Tolerance for f(x) < tolerance
        //Epsilon for lambdamax lambda<epsilon
        //maxIterations for max iterations allowed
        int n = x0.size;
        matrix B = matrix.id(n);
        vector x = x0.copy();
        double fx = minFunction(x);
        vector gradient = Gradient(x);

        while (gradient.norm() > epsilon & iteration<maxIteration)
        {
            vector deltaX = -B * gradient;
            
            //Line search
            double lambda = 1.0;
            bool stepAcceppted = false;
            while (!stepAcceppted)
            {
                vector xNew = x + lambda * deltaX;
                
                double fxNew = minFunction(xNew);
                if (fxNew < fx)
                {
                    vector s = lambda * deltaX;
                    vector y = Gradient(xNew) - gradient;
                    B = BUpdate(B,s,y);
                    x = xNew;
                    fx = fxNew;
                    gradient = Gradient(xNew);

                    stepAcceppted = true;
                }
                else
                {
                    lambda /= 2.0;
                    if (lambda < epsilon)
                    {
                        x = xNew;
                        fx = fxNew;
                        gradient = Gradient(x);
                        B = matrix.id(n);
                        stepAcceppted = true;
                    }
                }
            }
            iteration++;
        }
        return x;
    }
    
    public static matrix BUpdate(matrix B, vector s, vector y, double epsilon = 1e-8)
    {
        int n = s.size;
        vector u = s - B*y;
        matrix uuT = matrix.OuterProduct(u,u);
        double dotproduct = u.dot(y);

        matrix deltaB = new matrix(n,n);
        if (System.Math.Abs(dotproduct) > epsilon)
        {
            deltaB = uuT* 1.0/ dotproduct;
        }
        else
        {
            deltaB = uuT * 1 / epsilon;
        }

        return B + deltaB;
    }
    
    public static vector Gradient(vector x, double delta=1e-8)
    {
        int n = x.size;
        vector gradient = new vector(n);
        vector xP = x.copy();
        vector xM = x.copy();
        for (int i = 0; i < n; i++)
        {
            xP[i] += delta;
            xM[i] -= delta;
            gradient[i] = (minFunction(xP) - minFunction(xM)) / (2.0 * delta);
        }
        return gradient;
    }
}