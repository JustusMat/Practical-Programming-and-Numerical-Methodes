public partial class vector
{
    public vector Slice(int start, int end)
    {
        if (start < 0) start = this.size + start;
        if (end < 0) end = this.size + end;

        if (start >= this.size || end > this.size)
        {
            throw new System.ArgumentOutOfRangeException("Slice indices out of range");
        }

        var sliceData = new double[end - start];
        System.Array.Copy(this.data, start, sliceData, 0, end-start);
        return new vector(sliceData);
    }

    public void SetRange(int startIndex, int endIndex, double[] values)
    {
        int offset = values.GetLowerBound(0);
        for (int i = startIndex; i < endIndex; i++)
        {
            data[i] = values[i - startIndex + offset];
        }
    }
}