public class inbuildmultiprocessing
{
    public static double Multithreading(System.Func<long,double> argmethod,long nthreads, long nterms)
    {
        double sum = 0;
        System.Threading.Tasks.Parallel.For(1,nterms+1, new System.Threading.Tasks.ParallelOptions{MaxDegreeOfParallelism = (int)nthreads},delegate(long l) { sum += argmethod(l);});
        return sum;
    }
}