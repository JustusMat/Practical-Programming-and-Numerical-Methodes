public class vectors
{
    static void Main(string[] args)
    {
        //Initialize a vector with [x,y,z]=[1,2,3]
        var ABC = new vec(1,2,3);
        var CBA = new vec(3, 2, 1);
        
        System.Console.Write("The vector ABC is given by: ");
        ABC.Print();
        System.Console.Write("The vector CBA is given by: ");
        CBA.Print();
        Line();
        
        System.Console.WriteLine("Multiplication example with n = 10");
        int n = 10;
        ABC *= n;
        CBA *= n;
        System.Console.Write("ABC*n = ");
        ABC.Print();
        System.Console.Write("CBA*n = ");
        CBA.Print();
        Line();
        
        System.Console.WriteLine("Second option for multiplication with n = 10");
        ABC = n * ABC;
        CBA = n * CBA;
        System.Console.Write("ABC*n*n = ");
        ABC.Print();
        System.Console.Write("CBA*n*n = ");
        CBA.Print();
        Line();
        
        System.Console.WriteLine("Addition of ABC*n*n and CBA*n*n");
        var abccba = ABC + CBA;
        abccba.Print();
        Line();
        
        System.Console.WriteLine("Subtraction of CBA*n*n from ABC*n*n");
        (ABC-CBA).Print();
        Line();

        ABC /= n * n;
        CBA /= n * n;
        System.Console.WriteLine("Inner product between ABC and CBA");
        double inner_product= vec.dot(ABC,CBA);
        System.Console.WriteLine(inner_product);
        Line();
        
        double ABCNorm = vec.norm(ABC);
        double CBANorm = vec.norm(CBA);
        System.Console.WriteLine($"Norm of ABC = {ABCNorm}");
        System.Console.WriteLine($"Norm of CBA = {CBANorm}");
        Line();
        
        System.Console.WriteLine("Wedge product between ABC and CBA");
        var wedge_vec = vec.wedgeProduct(ABC,CBA);
        wedge_vec.Print();
        Line();
        
        
        
        bool abccbaBool = ABC.approx(CBA);
        bool cbaabcBool = CBA.approx(CBA);

        
        var rnd = new System.Random();
        double variation = rnd.NextDouble()*1e-9;
        bool abcBool = ABC.approx(ABC+variation*ABC);
        bool cbaBool = CBA.approx(CBA+variation*CBA);
        
        
        System.Console.WriteLine($"The variation is {variation}");
        if (abcBool && cbaBool)
        {
            equalTo("ABC");
            equalTo("CBA");
        }
        else
        {
            if (!abcBool & cbaBool)
            {
                notequalTo("ABC");
                equalTo("CBA");
            }

            else if (!cbaBool & abcBool)
            {
                equalTo("ABC");
                notequalTo("CBA");
            }
            else
            {
                notequalTo("ABC");
                notequalTo("CBA");
            }
        }

        if (!(cbaabcBool && abccbaBool))
        {
            System.Console.WriteLine("ABC is not equal to CBA");
            System.Console.WriteLine("CBA is not equal to ABC");
        }
        Line();
        System.Console.WriteLine("Done");
    }
    static void Line()
    {
        System.Console.WriteLine("-----------------------------------------");
    }
    static void equalTo(string s)
    {
        System.Console.WriteLine(s + " is equal to " + s+ " + " +s+ "*variation" );
    }
    static void notequalTo(string s)
    {
        System.Console.WriteLine(s + " is not equal to " + s+ " + "+ s+ "*variation" );
    }
}