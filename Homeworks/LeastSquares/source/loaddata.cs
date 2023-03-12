public class dataloader
{
    public static System.Collections.Generic.List<double[]> DataList = new System.Collections.Generic.List<double[]>();

    public dataloader(string infile)
    {
        LoadIntoArray(infile);
    }
    
    public static void LoadIntoArray(string infile)
    {

        using (var reader = new System.IO.StreamReader(infile))
        {
            var line = "";
            char[] separators = { ',', '!', ' ' };
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                var row = new double[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    row[i] = double.Parse(values[i]);
                }
                DataList.Add(row);
            }
        }

        var data = new double[DataList.Count][];
        for (int i = 0; i < DataList.Count; i++)
        {
            data[i] = DataList[i];
        }
    }
    
    
    
    public void print()
    {
        for (int i = 0; i < DataList.Count; i++)
        {
            for (int j = 0; j < DataList[i].Length; j++)
            {
                System.Console.Write(DataList[i][j] + " ");
            }
            System.Console.WriteLine();
        }
    }


    public double[] getRow(int i)
    {
        var x = new double[DataList[i].GetLength(0)];
        for (int j = 0; j < DataList[i].GetLength(0); j++)
        {
            x[j] = DataList[i][j];
        }

        return x;
    }

    public double[] getColumn(int j)
    {
        var x = new double[DataList.Count];
        for (int i = 0; i < DataList.Count; i++)
        {
            x[i] = DataList[i][j];
        }

        return x;
    }
    

}