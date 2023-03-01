public static partial class sfuncs
{
    public static double gamma(double x)
    {
        if (x < 0)
        {
            return System.Math.PI / System.Math.Sin(System.Math.PI * x) / gamma(1 - x);
        }

        if (x < 9)
        {
            return gamma(x + 1) / x;
        }

        double lngamma=x*System.Math.Log(x+1/(12*x-1/x/10))-x+System.Math.Log(2*System.Math.PI/x)/2;
        return System.Math.Exp(lngamma);
    }
}