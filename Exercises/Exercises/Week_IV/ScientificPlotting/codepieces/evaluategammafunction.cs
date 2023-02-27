public class evaluategammafunction
{
    static void Main(string[] args)
    {
        for (double x = -5 + 1.0 / 64; x <= 5; x += 1.0 / 32)
        {
            System.Console.WriteLine($"{x} {sfuncs.gamma(x)}");
        }
    }
}