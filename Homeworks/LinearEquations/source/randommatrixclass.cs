public class randommatrixclass : matrix
{
    public void RandomEntries()
    {
        var random = new System.Random();
        for (int i = 0; i < this.size1; i++)
        {
            for (int j = 0; j < this.size2; j++)
            {
                this.set(i,j,random.NextDouble());
            }
        }
    }

    public randommatrixclass(int n, int m) : base(n, m)
    {
        RandomEntries();
    }
    public randommatrixclass(string s) : base(s)
    {
    }
    public randommatrixclass(vector e) : base(e)
    {
    }
}