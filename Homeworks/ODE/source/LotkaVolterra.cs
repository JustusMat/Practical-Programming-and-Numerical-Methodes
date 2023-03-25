public class LotkaVolterra
{
    public static void Main(string[] args)
    {
        var y0 = new vector(10.0,5.0);
        double a = 0;
        double b = 15;
        double h = 0.01;
        
        var xlist = new genlist<double>();
        var ylist = new genlist<vector>();
        var (xs, ys) = odeclass.ImprovedDriver(F2, a, y0, b, h,xlist:xlist,ylist:ylist);
        for (int i = 0; i < xs.Size; i++)
        {
            System.Console.WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
        }


        //Demonstration that only the endpoint is stored and returned
        var (xsendpoint, ysendpoint) = odeclass.ImprovedDriver(F2, a, y0, b, h);
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter("LotkaVolterraEndpoint.data"))
        {
            //Actually just a for loop for one element, but for demonstration purposes 
            for (int i = 0; i < xsendpoint.Size; i++)
            {
                writer.WriteLine($"{xsendpoint[i]} {ysendpoint[i][0]} {ysendpoint[i][1]}");
            }
        }
    }
    
    public static System.Func<double, vector, vector> F2 = delegate(double t, vector z)
    {
        var a = 1.5;
        var b = 1.0;
        var c = 3.0;
        var d = 1.0;
        
        var x = z[0];
        var y = z[1];
        double dxdt = a * x - b * x * y;
        double dydt = -c * y + d * x * y;
        
        return new vector(dxdt, dydt);
    };
}