public class test
{
    public static void Main(string[] args)
    {
        Line();
        Line();
        var A = new randommatrix(10, 2);
        System.Console.WriteLine("Random matrix A given by");
        A.print();
        var qrgsObject = new QRGS(A);
        Line();
        System.Console.WriteLine("Q matrix given as");
        qrgsObject.Q.print();
        Line();
        System.Console.WriteLine("R matrix given as");
        qrgsObject.R.print();
        Line();
        
        var tmp = qrgsObject.Q * qrgsObject.R;
        tmp.print();
    }

    public static void Line(int n=50)
    {
        System.Console.WriteLine(new string('-', n));
    }
}