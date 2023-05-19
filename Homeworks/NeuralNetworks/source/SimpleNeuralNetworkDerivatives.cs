public partial class SimpleNeuralNetwork
{
    public double FirstDerivative(double x)
    {
        double sum = 0;
        for (int i = 0; i < nHiddenNodes; i++)
        {
            sum += HiddenWeights[i] * activationFunctionDerivative((x-HiddenShifts[i])/HiddenScales[i]/HiddenScales[i]);
        }

        return sum;
    }

    public double ResponseDerivative(double x)
    {
        double sum = 0;
        for (int i = 0; i < nHiddenNodes; i++)
        {
            double a = HiddenShifts[i];
            double b = HiddenScales[i];
            double w = HiddenWeights[i];
            double inputi = (x - a) / b;
            double activationDerivativei = activationFunctionDerivative(inputi);
            sum += activationDerivativei / b * w;
        }

        return sum;
    }

    public double ResponseSecondDerivative(double x)
    {
        double sum = 0;
        for (int i = 0; i < nHiddenNodes; i++)
        {
            double a = HiddenShifts[i];
            double b = HiddenScales[i];
            double w = HiddenWeights[i];
            double inputi = (x - a) / b;
            double activationDerivative_i = activationFunctionDerivative(inputi);
            double activationSecondDerivative_i = activationFunctionSecondDerivative(inputi);

            sum += (activationSecondDerivative_i * w) / (b * b) + (activationDerivative_i * w) / (b * b);
        }

        return sum;
    }
    
    public double ResponseAntiDerivative(double x)
    {
        double sum = 0;
        for (int i = 0; i < nHiddenNodes; i++)
        {
            double a = HiddenShifts[i];
            double b = HiddenScales[i];
            double w = HiddenWeights[i];
            double inputi = (x - a) / b;
            double activationIntegral_i = activationFunctionAntiDerivative(inputi);
            sum += b * activationIntegral_i * w;
        }

        return sum;
    }
    
}