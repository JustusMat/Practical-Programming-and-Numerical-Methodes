using System.ComponentModel;

public class vec
{
    //Fields
    public double x;
    public double y;
    public double z;

    //Constructor
    public vec()
    {
        //If no numbers specified, create 0-vector
        x = y = z = 0;
    }
    public vec(double a, double b, double c)
    {
        //Assign the fields the numbers
        x = a;
        y = b;
        z = c;
    }
    
    
    //Operators
    //Multiplication
    public static vec operator *(vec v, double c)
    {
        return new vec(c * v.x, c * v.y, c * v.z);
    }
    //Second option for multiplication
    public static vec operator *(double c, vec v)
    {
        return v * c;
    }
    public static vec operator /(vec v, double c)
    {
        return new vec(v.x / c, v.y / c, v.z / c);
    }
    //Addition
    public static vec operator +(vec u, vec v)
    {
        return new vec(u.x + v.x, u.y + v.y, u.z + v.z);
    }
    //Subtraction
    public static vec operator -(vec u, vec v)
    {
        return new vec(u.x - v.x, u.y - v.y, u.z - v.z);
    }
    //Actually no clue what this should produce
    public static vec operator -(vec u)
    {
        return new vec(-u.x, u.y , u.z );
    }
    
    
    //dot product
    public static double dot(vec vec1, vec vec2)
    {
        return vec2.x * vec1.x + vec2.y * vec1.y + vec2.z * vec1.z;
    }
    //norm of vector
    public static double norm(vec v)
    {
        return System.Math.Sqrt(vec.dot(v, v));
    }
    public static vec wedgeProduct(vec vec1, vec vec2)
    {
        double x_wedge,y_wedge,z_wedge;
        x_wedge = vec1.y * vec2.z - vec1.z * vec2.y;
        y_wedge = vec1.z * vec2.x - vec1.x * vec2.z;
        z_wedge = vec1.x * vec2.y - vec1.y * vec2.x; 
        
        var wedge_vector = new vec(x_wedge,y_wedge,z_wedge);
        return wedge_vector;
    }
    
    
    static bool approx(double a, double b, double tau = 1e-9, double eps = 1e-9)
    {
        double absValue = System.Math.Abs(a - b);
        if (absValue < tau)
        {
            return true;
        }

        if (absValue / (System.Math.Abs(a) + System.Math.Abs(b)) < eps)
        {
            return true;
        }

        return false;
    }
    public bool approx(vec vec2)
    {
        if (!approx(x,vec2.x))
        {
            return false;
        }
        if (!approx(y,vec2.y))
        {
            return false;
        }
        if (!approx(z,vec2.z))
        {
            return false;
        }

        return true;
    }
    public static bool approx(vec u, vec v)
    {
        return u.approx(v);
    }
    
    //Print statement
    public void Print(string s = "")
    {
        System.Console.Write(s);
        System.Console.WriteLine($"{x} {y} {z}");
    }
    
    
}