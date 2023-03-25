public class ThreeBodyProblem
{
    public static void Main(string[] args)
    {   // x1 y1 vx1 vy1 ...
        double[] y0Array = new[] { 0,0,-0.93240737, -0.86473146,
            -0.97000436, 0.24308753, 0.4662036850, 0.4323657300, 
            0.97000436, -0.24308753, 0.4662036850, 0.4323657300};
        var y0 = new vector(y0Array);
        double a = 0;
        double b = 6.3259;
        double h = 0.01;

        var xlist = new genlist<double>();
        var ylist = new genlist<vector>();
        //var (xs, ys) = odeclass.ImprovedDriver(TBP, a, y0, b, h, xlist: xlist, ylist: ylist);
        var (xs, ys) = odeclass.ImprovedDriver(TBP, a, y0, b, h,xlist:xlist, ylist:ylist);
        for (int i = 0; i < xs.Size; i++)
        {
            System.Console.Write($"{xs[i]} ");
            for (int j = 0; j < ys[i].size; j++)
            {
                System.Console.Write($"{ys[i][j]} ");
            }
            System.Console.Write("\n");
        }
    }

    public static System.Func<double, vector, vector> TBP = delegate(double t, vector z)
    {
        double m1, m2, m3;
        m1 = m2 = m3 = 1;

        vector r1 = new vector(z[0], z[1]);
        vector dr1dt = new vector(z[2], z[3]);

        vector r2 = new vector(z[4], z[5]);
        vector dr2dt = new vector(z[6], z[7]);

        vector r3 = new vector(z[8], z[9]);
        vector dr3dt = new vector(z[10], z[11]);

        vector ddr1dtnew = Gravitation(r1, r2, r3, mbody1: m2, mbody2: m3);
        vector ddr2dtnew = Gravitation(r2, r1, r3, mbody1: m1, mbody2: m3);
        vector ddr3dtnew = Gravitation(r3, r1, r2, mbody1: m1, mbody2: m2);

        double[] steppArray = new[]
        {
            dr1dt[0], dr1dt[1], ddr1dtnew[0], ddr1dtnew[1], dr2dt[0], dr2dt[1], ddr2dtnew[0], ddr2dtnew[1], dr3dt[0],
            dr3dt[1], ddr3dtnew[0], ddr3dtnew[1]
        };
        return new vector(steppArray);
    };


    public static vector Gravitation(vector raffected, vector rbody1, vector rbody2,
        double mbody1 = 1, double mbody2 = 1)
    {
        const double G = 1;
        vector ddrdt = G * mbody1 * (rbody1 - raffected) / System.Math.Pow((rbody1 - raffected).norm(), 3) +
                       G * mbody2 * (rbody2 - raffected) / System.Math.Pow((rbody2 - raffected).norm(), 3);
        return ddrdt;
    }
    
}