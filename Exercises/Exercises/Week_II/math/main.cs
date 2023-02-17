using System;

class math
{
    static void Main(string[] args)
    {
        const double e = Math.E;
        const double pi = Math.PI;

        double sqrt2 = Math.Sqrt(2);
        
        double ePowerPi = Math.Pow(e, pi);
        double ePowerPiPowerInvPi = Math.Pow(ePowerPi, 1 / pi);

        double piPowerE = Math.Pow(pi, e);
        double piPowerEPowerInvE = Math.Pow(piPowerE, 1 / e);

        Console.Write($"The square root of two is {sqrt2}\n");
        Console.Write($"Further, 2 = sqrt(2)*sqrt(2) = {sqrt2*sqrt2}\n");
        
        Console.Write("\n");
        Console.Write($"Euler's number to the power of pi is: {ePowerPi}\n");
        Console.Write($"Further, e = (e^pi)^(1/pi) = {ePowerPiPowerInvPi}\n");
        
        Console.Write("\n");
        Console.Write($"Pi to the power of Euler's number is: {piPowerE}\n");
        Console.Write($"Further, pi = (pi^e)^(1/e) = {piPowerEPowerInvE}\n");
        
        Console.Write("\nDone...");
    }
}