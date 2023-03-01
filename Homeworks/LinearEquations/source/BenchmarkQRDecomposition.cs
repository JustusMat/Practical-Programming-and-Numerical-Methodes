public class BenchmarkQRDecomposition
{
    public static void Main(string[] args)
    {
        var N = 1000;
        for (int i = 0; i < N; i+=10)
        {
            var A = new randommatrixclass(i, i);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var QRDecompositionObject = new QRGS(A);
            watch.Stop();
            var elapsedMs = watch.ElapsedTicks;
            System.Console.WriteLine($"{i} {elapsedMs}");
        }
        
    }        
}