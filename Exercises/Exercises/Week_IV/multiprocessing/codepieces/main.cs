public class ProgramMultiprocessing
{
    static int Main(string[] args)
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
        Line();
        System.Console.WriteLine("Doing own multithreading for loop");
        System.Console.WriteLine($"Input nterms={nterms} nthreads={nthreads}");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        var multithreadingresult = multiprocessing.MultiThreading(Harm, nthreads, nterms);
        System.Console.WriteLine($"nterm{nterms} sum={multithreadingresult}");
        System.Console.WriteLine("Done self made multiprocessing...");
        Line();
        
        return 0;

    }

    
    
    public static void Harm(object obj)
    {
        multiprocessing.data x = (multiprocessing.data)obj;
        System.Console.WriteLine($"{System.Threading.Thread.CurrentThread.Name} a={x.a} b={x.b}");
        x.sumab = 0;
        for (long i = x.a; i < x.b; i++)
        {
            x.sumab += 1.0 / i;
        }
        System.Console.WriteLine($"{System.Threading.Thread.CurrentThread.Name} sum={x.sumab}");
    }

    public static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }
}