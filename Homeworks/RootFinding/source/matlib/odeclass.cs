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
    }

    public static (genlist<double> xlist, genlist<vector> ylist) ImprovedDriver(System.Func<double, vector, vector> f, double a,
        vector ya, double b, double h = 0.01, double acc = 0.01, double eps = 0.01, genlist<double> xlist = null, genlist<vector> ylist = null)
    {
        double x = a;
        vector y = ya.copy();
        
        do
        {
            double[] tol = new double[y.size];
            double[] err = new double[y.size];
            
            if (x >= b)
            {
                if (xlist == null && ylist == null)
                {
                    xlist = new genlist<double>();
                    xlist.add(x);
                    ylist = new genlist<vector>();
                    ylist.add(y);
                    return (xlist, ylist);
                }
                else
                {
                    return (xlist, ylist);
                }
            }
            if (x+h>b)
            {
                h = b - x;
            }
            
            var (yh, erv) = Rkstep12(f, x, y, h);
            for (int i = 0; i < y.size; i++)
            {
                tol[i] = System.Math.Max(acc, yh.norm() * eps) * System.Math.Sqrt(h / (b - a));
            }
            bool ok = true;
            for (int i = 0; i < y.size; i++)
            {
                if (!(err[i] < tol[i]))
                {
                    ok = false;
                }
            }
            if (ok)
            {
                x += h;
                y = yh;
                if (xlist != null && ylist != null)
                {
                    xlist.add(x);
                    ylist.add(y);
                }
                double factor = tol[0] / System.Math.Abs(erv[0]);
                for (int i = 1; i < y.size; i++)
                {
                    factor = System.Math.Min(factor, tol[i] / System.Math.Abs(erv[i]));
                }
                h *= System.Math.Min(System.Math.Pow(factor, 0.25) * 0.95, 2);
            }
        } while (true);
        
    }
}