public class inputoutput
{
    public static void StandardReading(string[] args)
    {
        System.Console.WriteLine("reading from stdin");
        System.Console.WriteLine("writing to stdout");

        for (string line = System.Console.In.ReadLine(); line != null; line = System.Console.In.ReadLine())
        {
            double x = System.Convert.ToDouble(line);
            System.Console.Out.WriteLine($"${{x}} {System.Math.Sin(x)}");
        }
    }
    
    public static void FileReading(string[] args)
    {
        System.Console.WriteLine("reading from a given file");
        System.Console.WriteLine("writing to a given file");
        string infile = null, outfile = null;
        foreach (var arg in args)
        {
            System.Console.Out.WriteLine(arg);
            var words = arg.Split(':');
            if (words[0] == "-input")
            {
                infile = words[1];
            }

            if (words[0] == "-output")
            {
                outfile = words[1];
            }
        }

        if (infile == null || outfile == null) return;
        System.Console.WriteLine($"Found infile: {infile}");
        System.Console.WriteLine($"Found outfile: {outfile}");
        var inputstream = new System.IO.StreamReader(infile);
        var outputstream = new System.IO.StreamWriter(outfile,append:false);
        for (var line = inputstream.ReadLine(); line != null; line = inputstream.ReadLine())
        {
            //Actually Double.Parse is called in .ToDouble, but an extra check is applied whether the input is correct or not
            var x = System.Convert.ToDouble(line);
            outputstream.WriteLine($"{x} {System.Math.Sin(x)}");
        }
        inputstream.Close();
        outputstream.Close();
    }

    public static void CommandLineReading(string[] args)
    {
        System.Console.WriteLine("reading from the command line\n");
        double[] result = null;
        foreach (var arg in args)
        {
            string[] words = arg.Split(':');
            if (words[0] == "-numbers")
            {
                var numbers = words[1].Split(',');
                result = new double[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    result[i] = System.Convert.ToDouble(numbers[i]);
                }
            }
        }
        System.Console.WriteLine("done reading from the command line\n");
        System.Console.WriteLine($"result.Length={result.Length}");
        foreach (var x in result)
        {
            System.Console.WriteLine($"{x:0.00e+00}");
        }
        System.Console.WriteLine("done writing");
    }
}