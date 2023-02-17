class epsilon
{
    static void Line() //Function to print a line and separate the values
    {
        System.Console.Write("----------------------------------------------\n");
    }
    
    static void Main(string[] args)
    {
        var watchWhileloop = System.Diagnostics.Stopwatch.StartNew();
        int i = 1;
        while (i+1>i)
        {
            i++;
        }
        watchWhileloop.Stop();
        System.Console.Write($"My max int = {i}\n");
        Line();
        System.Console.Write($"Elapsed time (while loop) = {watchWhileloop.ElapsedMilliseconds} ms.\n");
        Line();

        var watchForLoop = System.Diagnostics.Stopwatch.StartNew();
        i = 1;
        for (; i+1> i; i++)
        {
            ;
        }
        watchForLoop.Stop();
        System.Console.Write($"Elapsed time (for loop) = {watchForLoop.ElapsedMilliseconds} ms.\n");
        Line();
        
        var watchDoLoop = System.Diagnostics.Stopwatch.StartNew();
        i = 1;
        do
        {
            i++;
        } while (i+1>i);
        watchDoLoop.Stop();
        System.Console.Write($"Elapsed time (do while loop) = {watchDoLoop.ElapsedMilliseconds} ms.\n");
        Line();
    }
    
}