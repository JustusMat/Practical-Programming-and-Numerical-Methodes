public partial class matrix
{
    public static matrix OuterProduct(vector v1, vector v2)
    {
        matrix result = new matrix(v1.size, v2.size);
        for (int i = 0; i < v1.size; i++)
        {
            for (int j = 0; j < v2.size; j++)
            {
                result[i, j] = v1[i] * v2[j];
            }
        }

        return result;
    }
}