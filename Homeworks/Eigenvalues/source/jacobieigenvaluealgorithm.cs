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
                    double apq = matrix.get(DMatrix, p, q);
                    double app = matrix.get(DMatrix, p, p);
                    double aqq = matrix.get(DMatrix, q, q);
                    double theta = 0.5 * System.Math.Atan2(2 * apq, aqq - app);
                    double c = System.Math.Cos(theta);
                    double s = System.Math.Sin(theta);

                    double new_app = c * c * app - 2 * s * c * apq + s * s * aqq;
                    double new_aqq = s * s * app - 2 * s * c * apq + c * c * aqq;

                    if (System.Math.Abs(new_app - app) > tolerance || System.Math.Abs(new_aqq - aqq) > tolerance)
                    {
                        changed = true;
                        System.Threading.Thread threadAJ = new System.Threading.Thread(() => timesJ(DMatrix, p, q, theta));
                        System.Threading.Thread threadJA = new System.Threading.Thread(() => Jtimes(DMatrix, p, q, -theta));
                        System.Threading.Thread threadVJ = new System.Threading.Thread(() => timesJ(V, p, q, theta));
                        threadAJ.Start();
                        threadJA.Start();
                        threadVJ.Start();
                        
                        threadAJ.Join();
                        threadJA.Join();
                        threadVJ.Join();
                    }
                }
            }
        } while (changed);
        for (int i = 0; i < DMatrix.size1; i++)
        {
            Eigenvalues[i] = DMatrix[i, i];
        }
    }
}