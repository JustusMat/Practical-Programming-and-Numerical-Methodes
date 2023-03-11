public class BenchmarkQRDecomposition
{
    public static void Main(string[] args)
    {
        var N = 1000;
        for (int i = 0; i < N; i+=10)
        {
            var A = new randommatrixclass(i, i);
            
            //Stopwactch does in fact not measure CPU usage time, but rather system time
            //For a single users on one pc stopwatch may approximate the cpu usage sufficiently, as no other processes influence too much
            //For a more accurate result consider the following posts and implement it
            // https://stackoverflow.com/questions/275957/can-a-c-sharp-program-measure-its-own-cpu-usage-somehow
            // https://www.codeproject.com/Articles/10258/How-to-Get-CPU-Usage-of-Processes-and-Threads
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var QRDecompositionObject = new QRGS(A);
            watch.Stop();
            var elapsedns = watch.Elapsed.TotalMilliseconds * 1000000;
            System.Console.WriteLine($"{i} {elapsedns}");
        }
        
    }        
}