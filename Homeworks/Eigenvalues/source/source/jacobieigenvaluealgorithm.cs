public class jacobieigenvaluealgorithm
{
    
    public vector Eigenvalues;
    public matrix V;
    public int sweeps;
    public matrix DMatrix;


    public jacobieigenvaluealgorithm(matrix inputMatrix)
    {
        DMatrix = inputMatrix.copy();
        V = matrix.id(DMatrix.size1);
        Eigenvalues = new vector(DMatrix.size1);
    }
    static void timesJ(matrix X,int p, int q, double theta)
    {
        double c = System.Math.Cos(theta);
        double s = System.Math.Sin(theta);
        for (int i = 0; i < X.size1; i++)
        {
            double aip = X[i, p];
            double aiq = X[i, q];
            X[i, p] = c * aip - s * aiq;
            X[i, q] = s * aip + c * aiq;
        }
    }
    static void Jtimes(matrix X,int p, int q, double theta)
    {
        double c = System.Math.Cos(theta);
        double s = System.Math.Sin(theta);
        for (int i = 0; i < X.size1; i++)
        {
            double api = X[p, i];
            double aqi = X[q, i];
            X[p, i] = c * api + s * aqi;
            X[q, i] = -s * api + c * aqi;
        }
    }
    public void EigenValueDecomposition(double tolerance = 1e-9)
    {
        bool changed;

        do
        {
            changed = false;
            sweeps++;
            for (int p = 0; p < DMatrix.size1-1; p++)
            {
                for (int q = p+1; q < DMatrix.size1; q++)
                {
                    double apq = DMatrix[p, q];
                    double app = DMatrix[p, p];
                    double aqq = DMatrix[q, q];
                    double theta = 0.5 * System.Math.Atan2(2 * apq, aqq - app);
                    double c = System.Math.Cos(theta);
                    double s = System.Math.Sin(theta);

                    double new_app = c * c * app - 2 * s * c * apq + s * s * aqq;
                    double new_aqq = s * s * app + 2 * s * c * apq + c * c * aqq;

                    if (System.Math.Abs(new_app - app) > tolerance || System.Math.Abs(new_aqq - aqq) > tolerance)
                    {
                        changed = true;
                        
                        timesJ(DMatrix,p,q,theta);
                        Jtimes(DMatrix,p,q,-theta);
                        timesJ(V,p,q,theta);
                        
                        /*
                        System.Threading.Thread threadAJ = new System.Threading.Thread(() => timesJ(DMatrix, p, q, theta));
                        System.Threading.Thread threadJA = new System.Threading.Thread(() => Jtimes(DMatrix, p, q, -theta));
                        System.Threading.Thread threadVJ = new System.Threading.Thread(() => timesJ(V, p, q, theta));
                        threadAJ.Start();
                        threadJA.Start();
                        threadVJ.Start();
                        
                        threadAJ.Join();
                        threadJA.Join();
                        threadVJ.Join();
                        */
                    }
                }
            }
        } while (changed);
        for (int i = 0; i < DMatrix.size1; i++)
        {
            Eigenvalues[i] = DMatrix[i, i];
        }
    }
    
    
    /*
    public void setElements(int i, int j, double s, double c)
    {
        System.Threading.Tasks.TaskFactory factory = new System.Threading.Tasks.TaskFactory();
        int numTasks = DMatrix.size1 -j;
        
        System.Threading.Tasks.Task[] tasks = new System.Threading.Tasks.Task[numTasks];
        
        for (int k = 0; k < numTasks; k++)
        {
            int index = j + k;
            tasks[k] = factory.StartNew(() =>
            { DMatrix.set(i, index, c * DMatrix[i, k] + s * DMatrix[j, k]);
                DMatrix.set(j,index,-s*DMatrix[i,k]+ c*DMatrix[j,k]);
            });
        }

        System.Threading.Tasks.Task.WaitAll(tasks);
    }
    public void UpperTriangleRotation()
    {
        for (int j = 0; j < DMatrix.size1; j++)
        {
            for (int i = 0; i < j; i++)
            {
                if (DMatrix[i, j] != 0)
                {
                    double c = DMatrix[j, j] / System.Math.Sqrt(System.Math.Pow(DMatrix[j, j],2) + System.Math.Pow(DMatrix[i, j],2));
                    double s = DMatrix[i, j] / System.Math.Sqrt(System.Math.Pow(DMatrix[j, j],2) + System.Math.Pow(DMatrix[i, j],2));
                    setElements(i,j,s,c);
                }
            }
        }
    }

    public void MaintainSymmetry(matrix inputMatrix)
    {
        int numTasks = System.Environment.ProcessorCount;
        int chunkSize = inputMatrix.size1 / numTasks;
        if (chunkSize == 0) chunkSize = 1;
        System.Threading.Tasks.Parallel.For(0, numTasks, taskIndex =>
        {
            int start = taskIndex * chunkSize;
            int end = (taskIndex == numTasks + 1) ? inputMatrix.size1 : (taskIndex + 1) * chunkSize;

            for (int i = start; i < end; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    inputMatrix[i, j] = inputMatrix[j, i];
                }
            }
        });

    }
    
    public matrix upperDMatrixTriangle()
    {
        matrix upperTriangle = new matrix(DMatrix.size1, DMatrix.size2);
        for (int i = 0; i < upperTriangle.size1; i++)
        {
            for (int j = i+1; j < upperTriangle.size2; j++)
            {
                upperTriangle[i, j] = DMatrix[i, j];
            }
        }

        return upperTriangle;
    }
    
    public void OptimisedEigenValueDecomposition()
    {
        int Iterations = 10;
        for (int iteration = 0; iteration < Iterations; iteration++)
        {
            matrix upperTriangle = upperDMatrixTriangle();
            matrix givensRotationMatrix = matrix.id(upperTriangle.size1);
            UpperTriangleRotation();
            upperTriangle = givensRotationMatrix * upperTriangle;
            DMatrix = (givensRotationMatrix.T * DMatrix) * givensRotationMatrix;
        }
        
        for (int i = 0; i < DMatrix.size1; i++)
        {
            Eigenvalues[i] = DMatrix[i, i];
        }
    }
    
    */
    
    
    
    
}