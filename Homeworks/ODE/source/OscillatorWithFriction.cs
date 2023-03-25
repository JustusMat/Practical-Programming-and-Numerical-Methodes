public class OscillatorWithFriction
{
    public static void Main(string[] args)
    {
        var y0 = new vector(System.Math.PI - 0.1, 0.0);

        double a = 0;
        double b = 10;
        double h = 0.01;

        var (xs, ys) = odeclass.Driver(F2, a, y0, b, h);
        for (int i = 0; i < xs.Size; i++)
        {
            System.Console.WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
        }
    }


    public static System.Func<double, vector, vector> F2 = delegate(double x, vector y)
    {
        var b = 0.25;
        var c = 5.0;
        var theta = y[0];
        var omega = y[1];
        return new vector(omega, -b * omega - c * System.Math.Sin(theta));
    };
    
    public static vector F(double x, vector y, double b = 0.25, double c = 5.0)
    {
        var theta = y[0];
        var omega = y[1];
        return new vector(omega, -b * omega - c * System.Math.Sin(theta));
    }
    
}