public class odeclass
{
    public static (vector yh, vector er) Rkstep12(System.Func<double, vector, vector> f, double x, vector y, double h)
    {
        vector k0 = f(x, y);
        vector k1 = f(x + h / 2, y + k0 * (h / 2));
        vector yh = y + k1 * h;
        vector er = (k1 - k0) * h;
        return (yh, er);
    }

    public static (genlist<double> x, genlist<vector> y) Driver(System.Func<double, vector, vector> f, double a, vector ya,
        double b, double h = 0.01, double acc = 0.01, double eps = 0.01)
    {
        if (a > b)
        {
            throw new System.SystemException("driver: a>b");
        }

        double x = a;
        vector y = ya.copy();
        var xlist = new genlist<double>();
        xlist.add(x);
        var ylist = new genlist<vector>();
        ylist.add(y);

        do
        {
            if (x >= b)
            {
                return (xlist, ylist);
            }

            if (x + h > b)
            {
                h = b - x;
            }
            var (yh, erv) = Rkstep12(f, x, y, h);
            double tol = System.Math.Max(acc,yh.norm()*eps)*System.Math.Sqrt(h/(b-a));
            double err = erv.norm();
            if (err<=tol)
            {
                x += h;
                y = yh;
                xlist.add(x);
                ylist.add(y);
            }
            h *= System.Math.Min(System.Math.Pow(tol/err,0.25)*0.95,2);
            
        } while (true);

        return (xlist, ylist);
    }
    
    
}