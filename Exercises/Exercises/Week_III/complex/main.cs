class ComplexExercise
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Doing complex calculations with cmath.cs and complex.cs");
        Line();

        //Variable definitions
        complex negativeOne = -complex.One;
        complex imaginaryUnit = complex.I;
        
        //Doing the calculations
        complex sqrtNegativeOne = cmath.sqrt(negativeOne);
        complex sqrtImaginaryUnit = cmath.sqrt(imaginaryUnit);
        complex exponentialImaginaryUnit = cmath.exp(imaginaryUnit);
        complex exponentialImaginaryUnitPi = cmath.exp(imaginaryUnit * System.Math.PI);
        complex imaginaryUnitPowerImaginaryUnit = cmath.pow(imaginaryUnit, imaginaryUnit);
        complex logarithmImaginaryUnit = cmath.log(imaginaryUnit);
        complex sinImaginaryUnitPi = cmath.sin(imaginaryUnit * System.Math.PI);
        complex sinhImaginaryUnit = cmath.sinh(imaginaryUnit);
        complex coshImaginaryUnit = cmath.cosh(imaginaryUnit);
        
        
        //Printing
        PrintMyNumber("Sqrt(-1)",sqrtNegativeOne);
        PrintMyNumber("Sqrt(i)",sqrtImaginaryUnit);
        PrintMyNumber("exp(i)",exponentialImaginaryUnit);
        PrintMyNumber("exp(i*pi)",exponentialImaginaryUnitPi);
        PrintMyNumber("i**i",imaginaryUnitPowerImaginaryUnit);
        PrintMyNumber("ln(i)",logarithmImaginaryUnit);
        PrintMyNumber("sin(i*pi)",sinImaginaryUnitPi);
        PrintMyNumber("sinh(i)",sinhImaginaryUnit);
        PrintMyNumber("cosh(i)",coshImaginaryUnit);
        Line();
    }

    static void Line()
    {
        System.Console.WriteLine(new string('-',50));
    }

    static void PrintMyNumber(string s,complex number)
    {
        System.Console.WriteLine($"{s} = {number.Re} + ({number.Im})i");
    }
    
}