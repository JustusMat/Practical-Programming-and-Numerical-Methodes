public class Program
{
    public static void Main(string[] args)
    {
        System.Func<vector, double> cFunc = (vector r) =>
        {
            return System.Math.Sqrt(System.Math.Pow(r[0], 2) + System.Math.Pow(r[1], 2));
        };
        vector a = new vector(-1, -1);
        vector b = new vector(1, 1);
        int N = (int)10e7;
        var mcobject = new montecarlo(cFunc, a, b, N);
        Line();
        Line();
        System.Console.WriteLine($"Using Npoints = {N}");
        Line();
        System.Console.WriteLine("Integrating function f(x,y) = sqrt(x^2 + y^2)");
        System.Console.WriteLine($"x bounds [{a[0]},{b[0]}]");
        System.Console.WriteLine($"y bounds [{a[1]},{b[1]}]");
        System.Console.WriteLine("The integral is ~ 3.0608");
        Line();
        /*
        var result = mcobject.plainmc();
        System.Console.WriteLine($"The plain montecarlo integral is = {result.Item1}");
        System.Console.WriteLine($"The plain montecarlo error is = {result.Item2}");
        Line();
        */
        var result_parallel = mcobject.PlainMCParallel();
        System.Console.WriteLine($"The plain montecarlo parallel integral is = {result_parallel.Item1}");
        System.Console.WriteLine($"The plain montecarlo parallel error is = {result_parallel.Item2}");
        var quasiresult = mcobject.QuasiMCParallel();
        System.Console.WriteLine($"The quasi montecarlo parallel integral is = {quasiresult.Item1}");
        System.Console.WriteLine($"The quasi montecarlo parallel error is = {quasiresult.Item2}");
        Line();
        int nReuse = 0;
        double meanReuse = 0.0;
        var StratifiedSamplingResult = mcobject.StratifiedSampling(nReuse,meanReuse);
        System.Console.WriteLine($"The StratifiedSampling integral is {StratifiedSamplingResult}");
        StratifiedSamplingResult = mcobject.StratifiedSamplingParallel(nReuse,meanReuse);
        System.Console.WriteLine($"The StratifiedSampling parallel integral is {StratifiedSamplingResult}");
        Line();
        cFunc = (vector r) =>
        {
            double argument = 1.0 - System.Math.Cos(r[0]) * System.Math.Cos(r[1]) * System.Math.Cos(r[2]);
            return 1/(System.Math.Pow(System.Math.PI,3)*argument);
        };
        a = new vector(0,0,0);
        b = new vector(System.Math.PI,System.Math.PI,System.Math.PI);
        mcobject = new montecarlo(cFunc, a, b, N);
        result_parallel = mcobject.PlainMCParallel();
        System.Console.WriteLine("The difficult integral is supposed to be 1.3932039296856768591842462603255");
        System.Console.WriteLine($"The plain montecarlo parallel integral is ={result_parallel.Item1}");
        System.Console.WriteLine($"The plain montecarlo parallel error is ={result_parallel.Item2}");
        Line();
        result_parallel = mcobject.QuasiMCParallel(); 
        System.Console.WriteLine($"The quasi montecarlo parallel integral is ={result_parallel.Item1}");
        System.Console.WriteLine($"The quasi montecarlo parallel error is ={result_parallel.Item2}");
        /* For some reason not working??
        StratifiedSamplingResult = mcobject.StratifiedSampling(nReuse,meanReuse);    
        System.Console.WriteLine($"The StratifiedSampling parallel montecarlo integral is ={StratifiedSamplingResult}");
        */
        Line();
        Line();
    }
    
    
    public static void Line(int n=88)
    {
        System.Console.WriteLine(new string('─',n));
    }
}