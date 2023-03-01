public class pfor
{
    public static void Main(string[] args)
    {
        long nterms = (long)1e8;
        long nthreads = 1;

        foreach (var arg in args)
        {
            var words = arg.Split(':');
            if (words[0] == "-terms")
            {
                nterms = (long)float.Parse(words[1]);
            }
            if (words[0] == "-threads")
            {
                nthreads = (long)float.Parse(words[1]);
            }
        }
        Line();
        System.Console.WriteLine("Doing inbuild multithreading for loop");
        System.Console.WriteLine($"Input nterms={nterms} nthreads={nthreads}");
        var inbuildmultithreadingresult = inbuildmultiprocessing.Multithreading(Harmonic,nthreads,nterms);
        System.Console.WriteLine($"nterm{nterms} sum={inbuildmultithreadingresult}");
        System.Console.WriteLine("Done inbuild multiprocessing...");
        Line();

    }    
    
    public static double Harmonic(long i)
    {
        return 1.0 / i;
    }
    public static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }
}