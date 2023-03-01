public static partial class sfuncs
{
    public static double lngamma(double x)
    {
        if (x <= 0)
        {
            throw new System.ArgumentException("lngamma: x<=0");
        }

        if (x <=9)
        {
            return lngamma(x + 1) - System.Math.Log(x);
        }

        return x * System.Math.Log(x + 1 / (12 * x - 1 / x / 10)) - x + System.Math.Log(2 * System.Math.PI / x) / 2;
    }
}