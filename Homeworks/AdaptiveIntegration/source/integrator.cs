public class integrator
{
    public static double RAIntegrate(System.Func<double, double> f, double a, double b, double delta = 1e-4,
        double epsilon = 1e-4, double f2 = System.Double.NaN, double f3 = System.Double.NaN)
    {
        double h = b - a;
        if (System.Double.IsNaN(f2))
        {
            f2 = f(a + 2 * h / 6);
            f3 = f(a + 4 * h / 6);
        }

        double f1 = f(a + h / 6);
        double f4 = f(a + 5 * h / 6);
        double Q = (2 * f1 + f2 + f3 + 2 * f4) / 6 * (b - a);
        double q = (f1 + f2 + f3 + f4) / 4 * (b - a);

        double err = System.Math.Abs(Q - q);
        if (err <= delta + epsilon * System.Math.Abs(Q))
        {
            return Q;
        }
        else
        {
            double sqrt2 = System.Math.Sqrt(2);
            return RAIntegrate(f, a, (a + b) / 2, delta / sqrt2, epsilon, f1, f2) +
                   RAIntegrate(f, (a + b) / 2, b, delta / sqrt2, epsilon, f3, f4);
        }
    }
    
    public static (double, double) RAIntegrateWithErrors(System.Func<double, double> f, double a, double b, double delta = 1e-4,
        double epsilon = 1e-4, double f2 = System.Double.NaN, double f3 = System.Double.NaN)
    {
        double h = b - a;
        if (System.Double.IsNaN(f2))
        {
            f2 = f(a + 2 * h / 6);
            f3 = f(a + 4 * h / 6);
        }

        double f1 = f(a + h / 6);
        double f4 = f(a + 5 * h / 6);
        double Q = (2 * f1 + f2 + f3 + 2 * f4) / 6 * (b - a);
        double q = (f1 + f2 + f3 + f4) / 4 * (b - a);

        double err = System.Math.Abs(Q - q);
        if (err <= delta + epsilon * System.Math.Abs(Q))
        {
            return (Q,err);
        }
        else
        {
            double sqrt2 = System.Math.Sqrt(2);
            var left = RAIntegrateWithErrors(f, a, (a + b) / 2, delta / sqrt2, epsilon, f1, f2);
            var right = RAIntegrateWithErrors(f, (a + b) / 2, b, delta / sqrt2, epsilon, f3, f4);
            return (left.Item1 + right.Item1, left.Item2+right.Item2);
        }
    }
    
    public static (double, double) ImprovedRAIntegrate(System.Func<double, double> f, double a, double b, double delta = 1e-4,
        double epsilon = 1e-4)
    {
        System.Func<double, double> g;
        double lower, upper;
        
        if (System.Double.IsNegativeInfinity(a) && System.Double.IsPositiveInfinity(b))
        {
            g = t => f(t/(1-t*t))*(1+t*t)/System.Math.Pow(1-t*t,2);
            lower = -1;
            upper = 1;
        }
        else if (System.Double.IsNegativeInfinity(a))
        {
            g = t => f(b + t/(1+t))/System.Math.Pow(1+t,2);
            lower = -1;
            upper = 0;
        }
        else if (System.Double.IsPositiveInfinity(b))
        {
            g = t => f(a+t/(1-t))/System.Math.Pow(1-t,2);
            lower = 0;
            upper = 1;
        }
        else
        {
            return RAIntegrateWithErrors(f, a, b, delta, epsilon);
        }
        return RAIntegrateWithErrors(g, lower, upper, delta, epsilon);
    }
    
    public static double CCIntegrate(System.Func<double, double> f, double a, double b, double acc = 1e-4,
        double eps = 1e-4)
    {
        System.Func<double, double> fcc = t =>
            f((a + b) / 2 + (b - a) / 2 * System.Math.Cos(t)) * System.Math.Sin(t) * (b - a) / 2;
        return RAIntegrate(fcc, 0, System.Math.PI, acc, eps);
    }
    
    
    
    
    
    
    /*
    public static System.Tuple<double, double> ClenshawCurtisTransform(double a, double b)
    {
        double t0 = System.Math.Cos(System.Math.PI * (2 * a + b) / (2 * (b - a)));
        double t1 = System.Math.Cos(System.Math.PI * (a + 2 * b) / (2 * (b - a)));
        return System.Tuple.Create(t0, t1);
    }
    
    public static double CCintegration(System.Func<double, double> f, double a, double b, double tol=0.001, int maxDepth = 10)
    {
        //Transform the integration limits
        var tt = ClenshawCurtisTransform(a, b);
        double t0 = tt.Item1;
        double t1 = tt.Item2;
        double fa = f(t0);
        double fb = f(t1);
        
        //Evaluate the midpoint and trapezoid rules
        double m = (a + b) / 2;
        double fm = f(m);
        double trap = (b - a) * (fa + fb) / 2;

        //Weights
        double w0 = 2 / (1 - t0 * t0);
        double w1 = 2 / (1 - t1 * t1);

        double simpson = (w0 * fa + 4 * fm + w1 * fb) * (b - a) / 6;
        double err = System.Math.Abs(simpson - trap) / 15;

        if (err <= tol || maxDepth == 0)
        {
            return simpson;
        }
        else
        {
            double left = CCintegration(f, a, m, tol / 2, maxDepth - 1);
            double right = CCintegration(f, m, b, tol / 2, maxDepth - 1);
            return left + right;
        }
        
        
    }
    */
}