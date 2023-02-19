public class ProgramGenlist
{
    public static void Main(string[] args)
    {
        genlist<double> listDouble = new genlist<double>();
        for (int i = 0; i < 10; i++)
        {
            listDouble.add(i);
        }

        System.Func<double, double> f;
        f = System.Math.Sin;
        
        genlistfilework.WriteGenlist(listDouble,f);
        Line();
        //////////////////////////////////////////////
        genlistfilework.IOFileGenlist(args);
        Line();
        //////////////////////////////////////////////
        System.Console.WriteLine("Removing the second and second last element from the array");
        listDouble.remove(2);
        listDouble.remove(listDouble.Size-2);
        listDouble.Print();
        Line();
        //////////////////////////////////////////////
        System.Console.WriteLine("Removing the second last element with generic list to list conversion method");
        listDouble.Removebyconverting(listDouble.Size-2);
        listDouble.Print();
        Line();
        //////////////////////////////////////////////
        System.Console.WriteLine("Testing addition methods");
        var listDoubleForComparison = new genlist<double>();
        int Nmax = 10000;
        System.Console.WriteLine($"Elapsed times for adding {Nmax} elements to the genlist");
        MakeBigGenlistByAddMethod(listDoubleForComparison,Nmax);
        
        //Notice that the new creation of size 8 genlist is not included in the genlist.cs and thus here has to be done manually
        //Should probably be implemented in a newer definition without the first add method.
        double[] a = new double[8];
        genlist<double> listDoubleForComparison2 = new genlist<double>(a);
        MakeBigGenlistByAddquickMethod(listDoubleForComparison2,Nmax);
        Line();
        //////////////////////////////////////////////
        System.Console.WriteLine("Chain of nodes example");
        genlistlinkednotes<int> b = new genlistlinkednotes<int>();
        for (int i = 0; i < 3; i++)
        {
            b.add(i);
        }

        for (b.start(); b.current != null; b.next())
        {
            System.Console.WriteLine(b.current.item);
        }            
        Line();
        System.Console.WriteLine("Done");
    }

    static void MakeBigGenlistByAddMethod(genlist<double> list,int N)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < N; i++)
        {
            list.add(i);
        }
        watch.Stop();
        var elapsedMsAddMethod = watch.Elapsed;
        System.Console.WriteLine($"add-method: \t \t \t{elapsedMsAddMethod} sec.");

    }

    static void MakeBigGenlistByAddquickMethod(genlist<double> list, int N)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < N; i++)
        {
            list.addquick(i);
        }
        watch.Stop();
        var elapsedMsAddquickMethod = watch.Elapsed;
        System.Console.WriteLine($"addquick-method: \t \t{elapsedMsAddquickMethod} sec.");
    }
    
    static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }
}