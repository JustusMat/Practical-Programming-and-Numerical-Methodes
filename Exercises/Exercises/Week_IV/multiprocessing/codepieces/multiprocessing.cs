public static class multiprocessing
{
    public static double MultiThreading(System.Action<data> argmethod,long nthreads, long nterms)
    {
        data[] x = new data[nthreads];
        
        for (long i = 0; i < nthreads; i++)
        {
            x[i] = new data();
            x[i].a = 1 + (i * nterms) / nthreads;
            x[i].b = (i + 1) * nterms / nthreads;
            System.Console.WriteLine($"i={i} x.a={x[i].a} x.b={x[i].b}");
        }

        
        
        System.Threading.Thread[] threads = new System.Threading.Thread[nthreads];
        for (long i = 0; i < nthreads; i++)
        {
            //The harmonic function is here passed to the thread
            var i1 = i;
            threads[i] = new System.Threading.Thread(()=>argmethod(x[i1]));
            //Actually no longer need: threads[i].Start(x[i]) since already given above
            threads[i].Start();
            threads[i].Name = $"thread number {i + 1}";
            //threads[i].Start(x[i]);
        }

        for (long i = 0; i < nthreads; i++)
        {
            threads[i].Join();
        }

        double total = 0;
        for (long i = 0; i < nthreads; i++)
        {
            total += x[i].sumab;
        }

        return total;
    }
    
    

    public class data
    {
        public long a, b;
        public double sumab;
    }
}