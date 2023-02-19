public class genlistfilework
{
    public static void WriteGenlist(genlist<double> arg,System.Func<double,double> func)
    {
        for (int i = 0; i < arg.Size; i++)
        {
            double x = arg[i];
            System.Console.WriteLine($"{arg[i]} {func(arg[i])}");
        }
    }

    public static void IOFileGenlist(string[] args)
    {
        //Logic for filereading with argumet -input
        System.Console.WriteLine("reading from a given file");
        string infile = null;
        foreach (var arg in args)
        {
            System.Console.Out.WriteLine(arg);
            var words = arg.Split(':');
            if (words[0] == "-input")
            {
                infile = words[1];
            }
        }

        if (infile == null)
        {
            System.Console.WriteLine("no input file found");
            return;
        }
        
        //Reading from input file and writing to stdout 
        System.Console.WriteLine($"Found infile: {infile}");
        var inputstream = new System.IO.StreamReader(infile);
        var list = new genlist<double[]>();
        char[] delimiters = { ' ', '\t' };
        var options = System.StringSplitOptions.RemoveEmptyEntries;
        for (string line = inputstream.ReadLine(); line!=null; line=inputstream.ReadLine())
        {
            var words = line.Split(delimiters, options);
            int n = words.Length;
            var numbers = new double[n];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = System.Convert.ToDouble(words[i]);
            }
            list.add(numbers);
        }

        for (int i = 0; i < list.Size; i++)
        {
            var numbers = list[i];
            foreach (var number in numbers)
            {
                System.Console.WriteLine($"{number : 0.00e+00; -0.00e+00} ");
            }
        }
    }
}