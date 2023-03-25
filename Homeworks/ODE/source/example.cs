public static class Program
{
    public static void Main(string[] args)
    {
        double a = 0;
        double b = 2 * System.Math.PI;
        var ya = new vector(0.0, 1.0);
        double h = 0.01;
        double acc = 1e-2;
        double eps = 1e-2;
        
        var (xs,ys) = odeclass.Driver(F, a, ya, b, h, acc, eps);
        
        for (int i = 0; i < xs.Size; i++)
        {
            System.Console.WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
        }
    }
    public static System.Func<double, vector, vector> F = delegate(double x, vector y) {
        return new vector(y[1], -y[0]);
    };
}