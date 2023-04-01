public class QRGS
{
    public matrix Q;
    public matrix R;
    public int m;

    public QRGS(matrix A)
    {
        m = A.size2;
        Q = A.copy();
        R = new matrix(m, m);
        Decomposition();
    }

    private void Decomposition()
    {
        for (int i = 0; i < m; i++)
        {
            R[i, i] = Q[i].norm();
            Q[i] /= R[i, i];
            
            for (int j = i+1; j < m; j++)
            {
                R[i, j] = Q[i].dot(Q[j]);
                Q[j] -= Q[i]*R[i, j];
            }
        }
    }

    
    public vector Solve(vector b)
    {
        //Rx = Q^T b (eq. 7)
        vector x = Q.T*b;

        for (int i = x.size -1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i+1; j < x.size; j++)
            {
                sum += R[i, j] * x[j];
            }
            x[i] = (x[i] - sum) / (R[i, i]);
        }
    
        return x;
    }
    
    public double Determinat()
    {
        double determinat = 1;
        for (int i = 0; i < R.size1; i++)
        {
            determinat *= R.get(i, i);
        }

        return determinat;
    }


    public matrix Inverse()
    {
        int n = Q.size1;
        //Solving eq. 46
        var ei = new vector(n);
        var Ainv = new matrix(n,n);
        for (int i = 0; i < n; i++)
        {  
           //make unit vector
           ei[i] = 1;
           //Solve for column in inverse matrix
           Ainv[i] = Solve(ei);
           //prepare empty unit vector for next loop
           ei[i] = 0;
        }
        
        return Ainv;
    }

    public matrix PseudoInverse()
    {
        int n = Q.size1;
        var Rinv = new QRGS(R).Inverse();
        var Qt = Q.T;
        var Ainv = Rinv * Qt;
        return Ainv;
    }
}