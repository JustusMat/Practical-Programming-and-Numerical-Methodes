using System.Linq;
public partial class montecarlo
{
    private static System.Func<vector, double> f;
    private static vector a;
    private static vector b;
    private static int N;
    
    
    public montecarlo(System.Func<vector, double> function, vector lower, vector upper, int npoints)
    {
        f = function;
        a = lower;
        b = upper;
        N = npoints;
    }
    
    public (double, double) plainmc()
    {
        int dim = a.size;
        double V = 1;
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0;
        double sumtwo = 0;
        var x = new vector(dim);
        
        for (int i = 0; i < N; i++)
        {
            this.ShowProgressBar(i,N-1,"plainmc");
            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + new System.Random().NextDouble() * (b[k] - a[k]);
            }
            double fx = f(x);
            sum += fx;
            sumtwo += fx * fx;
        }

        double mean = sum / N;
        double sigma = System.Math.Sqrt(sumtwo / N - mean * mean);
        var result = (mean * V, sigma * V / System.Math.Sqrt(N));
        return result;
    }

    public (double, double) PlainMCParallel()
    {
        int dim = a.size;
        double V = 1;
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0;
        double sumtwo = 0;
        var x = new vector(dim);
        int num_tasks = System.Environment.ProcessorCount;
        int chunk_size = N / num_tasks;
        System.Console.Error.WriteLine($"Available processors {num_tasks}");

        object progressLock = new object();
        int completedTasks = 0;
        
        
        System.Threading.Tasks.Task< (double, double)>[] tasks = new System.Threading.Tasks.Task<(double, double)>[num_tasks];
        for (int t = 0; t < num_tasks; t++)
        {
            int start = t * chunk_size;
            int end = (t == num_tasks - 1) ? N : (t + 1) * chunk_size;
            
            
            tasks[t] = System.Threading.Tasks.Task<(double, double)>.Factory.StartNew(() =>
            {
                double local_sum = 0;
                double local_sumtwo = 0;
                var local_rand = new System.Random();
                for (int i = start; i < end; i++)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        x[k] = a[k] + local_rand.NextDouble() * (b[k] - a[k]);
                    }

                    double fx = f(x);
                    local_sum += fx;
                    local_sumtwo += fx * fx;
                }

                //Update progress bar
                lock (progressLock)
                {
                    completedTasks++;
                    int progress = (int)(((double)completedTasks / num_tasks) * 100);
                    ShowProgressBar(progress, 100, "Plain MC...");
                }
                
                
                return (local_sum,local_sumtwo);
            });
        }
        System.Threading.Tasks.Task.WaitAll(tasks);

        for (int t = 0; t < num_tasks; t++)
        {
            sum += tasks[t].Result.Item1;
            sumtwo += tasks[t].Result.Item2;
        }

        double mean = sum / N;
        double sigma = System.Math.Sqrt(sumtwo / N - mean * mean);
        var result = (mean * V, sigma * V / System.Math.Sqrt(N));
        return result;
    }
    
    public (double, double) QuasiMCParallel()
    {
        int dim = a.size;
        int[] primes1 = GeneratePrimeSet(dim + 1,0);
        int[] primes2 = GeneratePrimeSet(dim + 1,1);
        double V = 1;
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0;
        double sumtwo = 0;
        double sumsq = 0;
        double sumsqtwo = 0;

        int numTasks = System.Environment.ProcessorCount;
        int chunkSize = N / numTasks;

        //Progressbar stuff
        System.Console.Error.WriteLine($"Available processors {numTasks}");
        object progressLock = new object();
        int completedTasks = 0;
        
        
        var tasks = new System.Threading.Tasks.Task<double[]>[numTasks];
        
        for (int t = 0; t < numTasks; t++)
        {
            int start = t * chunkSize;
            int end = (t == numTasks - 1) ? N : (t + 1) * chunkSize;
            tasks[t] = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                var x = new vector(dim);
                double[] results = new double[4];
                for (int i = start; i < end; i++)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        x[k] = a[k] + Halton(i + 1, primes1[k]) * (b[k] - a[k]);
                    }

                    double fx1 = f(x);
                    results[0] += fx1;
                    results[1] += fx1 * fx1;
                    
                    for (int k = 0; k < dim; k++)
                    {
                        x[k] = a[k] + Halton(i + 1, primes2[k]) * (b[k] - a[k]);
                    }
                    double fx2 = f(x);
                    results[2] += fx2;
                    results[3] += fx2 * fx2;
                }
                
                //Update progress bar
                lock (progressLock)
                {
                    completedTasks++;
                    int progress = (int)(((double)completedTasks / numTasks) * 100);
                    ShowProgressBar(progress, 100, "Quasi MC...");
                }
                
                return results;
            });
        }
        System.Threading.Tasks.Task.WaitAll(tasks);


        for (int t = 0; t < numTasks; t++)
        {
            double[] taskResults = tasks[t].Result;
            sum += taskResults[0];
            sumsq += taskResults[1];
            sumtwo += taskResults[2];
            sumsqtwo += taskResults[3];
        }
        
        
        double mean1 = sum / N;
        double mean2 = sumtwo / N;
        double var1 = sumsq / N - mean1 * mean1;
        double var2 = sumsqtwo / N - mean2 * mean2;
        double var_combined = (var1 + var2) / 2;
        double sigma = System.Math.Sqrt(var_combined);
        double SEM = sigma * V / System.Math.Sqrt(2 * N);
        
        //var result = (mean1 * V, sigma * V / System.Math.Sqrt(N));
        var result = ((mean1+mean2)/2 *V, SEM*V);
        return result;
    }

    double Halton(int index, int baseNumber)
    {
        double result = 0;
        double f = 1.0 / baseNumber;
        int i = index;
        while (i>0)
        {
            result += f * (i % baseNumber);
            i = (int)System.Math.Floor(i / (double)baseNumber);
            f /= baseNumber;
        }

        return result;
    }
    
    public static int[] GeneratePrimeSet(int n, int seed = 42)
    {
        int[] primes = new int[n];
        bool[] isPrime = new bool[n * n];
        int count = 0;

        int start = new System.Random(seed).Next(2, isPrime.Length);
        
        for (int i = start; i < isPrime.Length && count < n; i++)
        {
            if (!isPrime[i])
            {
                primes[count++] = i;
                for (int j = i*i; j < isPrime.Length; j +=i)
                {
                    isPrime[j] = true;
                }
            }
        }

        return primes;
    }
    
    //Cosmetic :)
    public void ShowProgressBar(int progress, int total, string name = " ", int width = 50, char symbol = '\u2588')
    {
        if (total == 0)
        {
            return;
        }
        double ratio = (double)progress / total;
        int numSymbols = (int)(ratio * width);
        string progressBar = "|" + new string(symbol, numSymbols) + new string(' ', width - numSymbols) + "|";
        string percent = ((int)(ratio * 100)).ToString("D2") + "%";
        string output = progressBar + " " + name + " " + percent;
        System.Console.Error.Write("\r" + output);
        System.Console.Error.Flush();
        if (progress == total)
        {
            System.Console.Error.WriteLine();
        }
    }
    
    
    
    /*     public (double, double) quasimc()
    {
        int dim = a.size;
        int[] primes = GeneratePrimeSet(dim+1);
        double V = 1;
        for (int i = 0; i < dim; i++)
        {
            V *= b[i] - a[i];
        }

        double sum = 0;
        double sumtwo = 0;
        var x = new vector(dim);
        for (int i = 0; i < N; i++)
        {
            for (int k = 0; k < dim; k++)
            {
                x[k] = a[k] + Halton(i + 1, primes[k]) * (b[k] - a[k]);
            }

            double fx = f(x);
            sum += fx;
            sumtwo += fx * fx;
        }

        double mean = sum / N;
        double sigma = System.Math.Sqrt(sumtwo / N - mean * mean);
        var result = (mean * V, sigma * V / System.Math.Sqrt(N));
        return result;
    }
*/
}