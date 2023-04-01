public static class Program
{
    public static void Main(string[] args)
    {
        double a = 0;
        double b = 1;
        int n = 0;
        
        Line();
        Line();
        
        System.Func<double, double> f = (x) =>
        {
            n++;
            return System.Math.Sqrt(x);
        };
        n = 0;
        double integral = integrator.RAIntegrate(f, a, b);
        System.Console.WriteLine($"The integral of sqrt(x) from {a} to {b} is 2/3");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to {b} is {integral} npoints={n}");
        n = 0;
        integral = integrator.CCIntegrate(f, a, b);
        System.Console.WriteLine($"The open quadrature integral with Clenshaw-Curtis variable transformation from {a} to {b} is {integral} npoints={n}");
        
        
        Line();
        f = (x) =>
        {
            n++;
            return 1 / System.Math.Sqrt(x);
        };
        n = 0;
        integral = integrator.RAIntegrate(f, a, b);
        System.Console.WriteLine($"The integral of 1/sqrt(x) from {a} to {b} is 2");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to {b} is {integral} npoints={n}");
        n = 0;
        integral = integrator.CCIntegrate(f, a, b);
        System.Console.WriteLine($"The open quadrature integral with Clenshaw-Curtis variable transformation from {a} to {b} is {integral} npoints={n}");
        
        
        Line();
        f = (x) =>
        {
            n++;
            return 4 * System.Math.Sqrt(1 - System.Math.Pow(x, 2));
        };
        n = 0;
        integral = integrator.RAIntegrate(f, a, b);
        System.Console.WriteLine($"The integral of 4sqrt(1-x^2) from {a} to {b} is {System.Math.PI}");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to {b} is {integral} npoints={n}");
        n = 0;
        integral = integrator.CCIntegrate(f, a, b);
        System.Console.WriteLine($"The open quadrature integral with Clenshaw-Curtis variable transformation from {a} to {b} is {integral} npoints={n}");
        
        
        Line();
        f = (x) =>
        {
            n++;
            return System.Math.Log(x) / System.Math.Sqrt(x);
        };
        n = 0;
        integral = integrator.RAIntegrate(f, a, b);
        System.Console.WriteLine($"The integral of ln(x)/sqrt(x) from {a} to {b} is -4");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to {b} is {integral} npoints={n}");
        n = 0;
        integral = integrator.CCIntegrate(f, a, b);
        System.Console.WriteLine($"The open quadrature integral with Clenshaw-Curtis variable transformation from {a} to {b} is {integral} npoints={n}");
        Line();
        f = (x) =>
        {
            n++;
            return 1/System.Math.Pow(x, 2);
        };
        a = 1;
        b = System.Double.PositiveInfinity;
        n = 0;
        var results = integrator.ImprovedRAIntegrate(f, a, b);
        System.Console.WriteLine($"The integral of 1/x^2 from {a} to infinity is 1");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to infinity is {results.Item1} npoints={n}");
        System.Console.WriteLine($"The recursive adpative integral error is {results.Item2}");
        Line();
        f = (x) =>
        {
            n++;
            return System.Math.Exp(-System.Math.Pow(x,2));
        };
        a = System.Double.NegativeInfinity;
        b = System.Double.PositiveInfinity;
        n = 0;
        results = integrator.ImprovedRAIntegrate(f, a, b);
        System.Console.WriteLine($"The gaussian integral from {a} to {b} is {System.Math.Sqrt(System.Math.PI)}");
        System.Console.WriteLine($"The recursive adaptive integral from {a} to {b} is {results.Item1} npoints={n}");
        System.Console.WriteLine($"The recursive adpative integral error is {results.Item2}");
        Line();
        
    }

    
    
    public static void Line(int n = 120)
    {
        System.Console.WriteLine(new string('-',n));
    }
}