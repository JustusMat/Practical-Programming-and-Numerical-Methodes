public class randommatrix : matrix
{
    public void RandomEntries()
    {
        var random = new System.Random();
        for (int i = 0; i < this.size1; i++)
        {
            for (int j = 0; j < this.size2; j++)
            {
                this.set(i,j,2.0*random.NextDouble()-1.0);
            }
        }
    }
    
    public void RandomSymmetricEntries()
    {
        var random = new System.Random();
        for (int i = 0; i < this.size1; i++)
        {
            for (int j = i; j < this.size2; j++)
            {
                this.set(i,j,2.0*random.NextDouble()-1.0);
                this.set(j,i,2.0*random.NextDouble()-1.0);
            }
        }
    }

    public randommatrix(int n, int m) : base(n, m)
    {
        RandomEntries();
    }
    public randommatrix(string s) : base(s)
    {
    }
    public randommatrix(vector e) : base(e)
    {
    }
}