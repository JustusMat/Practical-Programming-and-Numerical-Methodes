public class leastsq
{
    public static (vector, vector , matrix, matrix) lsfit(System.Func<double, double>[] fs, vector x, vector y, vector dy)
    {
        int n = x.size;
        int m = fs.Length;
        var A = new matrix(n, m);
        var b = new vector(n);
        for (int i = 0; i < n; i++)
        {
            b[i] = y[i] / dy[i];
            for (int k = 0; k < m; k++)
            {
                A[i, k] = fs[k](x[i]) / dy[i];
            }
        }

        var qra = new QRGS(A);
        vector c = qra.Solve(b);
        var S = A * A.T;
        var CovarianceMatrix = new QRGS(A.T * A).Inverse();
        vector cUncertainties = new vector(c.size);
        for (int i = 0; i < cUncertainties.size; i++)
        {
            cUncertainties[i] = System.Math.Sqrt(CovarianceMatrix[i, i]);
        }
        
        return (c, cUncertainties, S, CovarianceMatrix);
    }
    
    public (vector, matrix) lsfitparallel(System.Func<double, double>[] fs, vector x, vector y, vector dy)
    {
        int n = x.size;
        int m = fs.Length;
        var A = new matrix(n, m);
        var b = new vector(n);
        System.Threading.Tasks.Parallel.For(0, n, i =>
        {
            b[i] = y[i] / dy[i];
            for (int j = 0; j < m; j++)
            {
                A[i, j] = fs[j](x[i]) / dy[i];
            }
        });

        var qra = new QRGS(A);
        vector c = qra.Solve(b);
        var Ainv = qra.Inverse();
        var S = A * A.T;
        
        return (c, S);
    }
    
}