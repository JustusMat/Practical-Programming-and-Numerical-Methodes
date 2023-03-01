public class InputOutputClass
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Hello World");
        
        inputoutput.StandardReading(args);
        Line();
        inputoutput.FileReading(args);
        Line();
        inputoutput.CommandLineReading(args);
        Line();
        System.Console.Error.WriteLine("job done");
    }

    public static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }
}