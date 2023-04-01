
public partial class montecarlo
{
    public double StratifiedSamplingParallel(int nReuse, double meanReuse, vector lowerBounds = null,
        vector upperBounds = null, double accuracy = 1e-4, double epsilon = 1e-4)
    {
        if (lowerBounds == null)
        {
            lowerBounds = a;
            upperBounds = b;
        }

        var dim = lowerBounds.size;
        int NSamples = 16 * dim;
        //int NSamples = N;
        double V = 1.0;
        for (int k = 0; k < dim; k++)
        {
            V *= upperBounds[k] - lowerBounds[k];
        }

        int numTasks = System.Environment.ProcessorCount;
        int chunkSize = NSamples / numTasks;
        double mean = 0.0;
        
        int[] nLeft = new int[dim];
        int[] nRight = new int[dim];
        double[] meanLeft = new double[dim];
        double[] meanRight = new double[dim];

        object meanLock = new object();

        System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[numTasks];
        
        for (int t = 0; t < numTasks; t++)
        {
            int start = t * chunkSize;
            int end = (t == numTasks - 1) ? NSamples : (t + 1) * chunkSize;
            tasks[t] = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                var localMean = 0.0;
                var localNLeft = new int[dim];
                var localNRight = new int[dim];
                var localMeanLeft = new vector(dim);
                var localMeanRight = new vector(dim);
                var localRandom = new System.Random();
                for (int i = start; i < end; i++)
                {
                    var x = new vector(dim);
                    for (int k = 0; k < dim; k++)
                    {
                        x[k] = lowerBounds[k] + (localRandom.NextDouble() * (upperBounds[k] - lowerBounds[k]));
                    }

                    double fx = f(x);
                    localMean += fx;
                    for (int k = 0; k < dim; k++)
                    {
                        if (x[k] > (lowerBounds[k] + upperBounds[k])/2.0)
                        {
                            localNRight[k]++;
                            localMeanRight[k] += fx;
                        }
                        else
                        {
                            localNLeft[k]++;
                            localMeanLeft[k] += fx;
                        }
                    }
                }

                lock (meanLock)
                {
                    mean += localMean;
                }

                lock (nLeft)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        nLeft[k] += localNLeft[k];
                    }
                }

                lock (nRight)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        nRight[k] += localNRight[k];
                    }
                }

                lock (meanLeft)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        meanLeft[k] += localMeanLeft[k];
                    }
                }

                lock (meanRight)
                {
                    for (int k = 0; k < dim; k++)
                    {
                        meanRight[k] += localMeanRight[k];
                    }
                }
            });
        }
        System.Threading.Tasks.Task.WaitAll(tasks);


        mean /= NSamples;
        for (int k = 0; k < dim; k++)
        {
            meanLeft[k] /= nLeft[k];
            meanRight[k] /= nRight[k];
        }

        int kDiv = 0;
        double maxVar = 0.0;
        for (int k = 0; k < dim; k++)
        {
            double difference = System.Math.Abs(meanRight[k] - meanLeft[k]);
            if (difference > maxVar)
            {
                maxVar = difference;
                kDiv = k;
            }
        }

        double integral = (mean * NSamples + meanReuse * nReuse) / (NSamples + nReuse) * V;
        double error = System.Math.Abs(meanReuse - mean) * V;
        double tolerance = accuracy + System.Math.Abs(integral) * epsilon;

        if (error < tolerance)
        {
            return integral;
        }

        vector newLowerBounds = lowerBounds.copy();
        vector newUpperBounds = upperBounds.copy();
        newLowerBounds[kDiv] = (lowerBounds[kDiv] + upperBounds[kDiv]) / 2.0;
        newUpperBounds[kDiv] = (lowerBounds[kDiv] + upperBounds[kDiv]) / 2.0;
        
        double newAccuracy = accuracy / System.Math.Sqrt(2);
        
        
        double integralLeft = StratifiedSampling(nLeft[kDiv], meanLeft[kDiv], lowerBounds: lowerBounds,
            upperBounds: newUpperBounds,accuracy: newAccuracy,epsilon: epsilon);
        
        double integralRight = StratifiedSampling(nRight[kDiv], meanRight[kDiv], lowerBounds: newLowerBounds,
            upperBounds: upperBounds, accuracy: newAccuracy, epsilon: epsilon);
        
        return integralLeft + integralRight;
    }
    
    public double StratifiedSampling(int nReuse, double meanReuse, vector lowerBounds = null,
        vector upperBounds = null, double accuracy = 1e-4, double epsilon = 1e-4)
    {
        if (lowerBounds == null)
        {
            lowerBounds = a;
            upperBounds = b;
        }

        var dim = lowerBounds.size;
        int NSamples = 16 * dim;
        //int NSamples = N;
        double V = 1.0;
        for (int k = 0; k < dim; k++)
        {
            V *= upperBounds[k] - lowerBounds[k];
        }
        
        int[] nLeft = new int[dim];
        int[] nRight = new int[dim];

        vector x = new vector(dim);
        double[] meanLeft = new double[dim];
        double[] meanRight = new double[dim];

        double mean = 0.0;
        
        for (int i = 0; i < NSamples; i++)
        {
            for (int k = 0; k < dim; k++)
            {
                x[k] = lowerBounds[k] + (new System.Random().NextDouble() * (upperBounds[k] - lowerBounds[k]));
            }
            
            double fx = f(x);
            mean += fx;
            for (int k = 0; k < dim; k++)
            {
                if (x[k] > (lowerBounds[k] + upperBounds[k]) / 2.0)
                {
                    nRight[k]++;
                    meanRight[k] += fx;
                }
                else
                {
                    nLeft[k]++;
                    meanLeft[k] += fx;
                }
            }
        }

        mean /= NSamples;
        for (int k = 0; k < dim; k++)
        {
            meanLeft[k] /= nLeft[k];
            meanRight[k] /= nRight[k];
        }

        int kDiv = 0;
        double maxVar = 0.0;
        for (int k = 0; k < dim; k++)
        {
            double difference = System.Math.Abs(meanRight[k] - meanLeft[k]);
            if (difference > maxVar)
            {
                maxVar = difference;
                kDiv = k;
            }
        }

        double integral = (mean * NSamples + meanReuse * nReuse) / (NSamples + nReuse) * V;
        double error = System.Math.Abs(meanReuse - mean) * V;
        double tolerance = accuracy + System.Math.Abs(integral) * epsilon;

        if (error < tolerance)
        {
            return integral;
        }

        vector newLowerBounds = lowerBounds.copy();
        vector newUpperBounds = upperBounds.copy();
        newLowerBounds[kDiv] = (lowerBounds[kDiv] + upperBounds[kDiv]) / 2.0;
        newUpperBounds[kDiv] = (lowerBounds[kDiv] + upperBounds[kDiv]) / 2.0;
        
        double newAccuracy = accuracy / System.Math.Sqrt(2);
        
        
        double integralLeft = StratifiedSampling(nLeft[kDiv], meanLeft[kDiv], lowerBounds: lowerBounds,
            upperBounds: newUpperBounds,accuracy: newAccuracy,epsilon: epsilon);
        
        double integralRight = StratifiedSampling(nRight[kDiv], meanRight[kDiv], lowerBounds: newLowerBounds,
            upperBounds: upperBounds, accuracy: newAccuracy, epsilon: epsilon);
        
        return integralLeft + integralRight;
    }
}