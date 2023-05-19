public partial class SimpleNeuralNetwork
{
    private int nHiddenNodes;
    private System.Func<double, double> activationFunction;
    private System.Func<double, double> activationFunctionDerivative;
    private System.Func<double, double> activationFunctionSecondDerivative;
    private System.Func<double, double> activationFunctionAntiDerivative;
    
    private vector parameters;

    public SimpleNeuralNetwork(int n, string activation="Sigmoid")
    {
        nHiddenNodes = n;
        if (activation == "Gaussian")
        {
            activationFunction = x => System.Math.Exp(-x * x);
        }
        else if (activation == "Wavelet")
        {
            activationFunction = x => System.Math.Cos(5 * x) *System.Math.Exp(-x * x);
        }
        else if (activation == "Gaussian Wavelet")
        {
            activationFunction = x => System.Math.Exp(-x * x) * x;
            activationFunctionDerivative = x => System.Math.Exp(-x * x) * (1 - 2 * x * x);
            activationFunctionSecondDerivative = x => System.Math.Exp(-x * x) * (4 * x * x * x - 6 * x);
            activationFunctionAntiDerivative = x => System.Math.Exp(-x * x)/2;
        }
        else
        {
            activationFunction = x => 1.0 / (1.0 + System.Math.Exp(-x));
        }
        System.Console.Error.WriteLine($"Using activation function {activation}");
        int nParams = 3 * nHiddenNodes;
        parameters = new vector(nParams);
        InitiliaseParameters();
    }

    private vector HiddenWeights
    {
        get { return parameters.Slice(0, nHiddenNodes);}
        set
        {
            if (value.size != nHiddenNodes)
            {
                throw new System.ArgumentException($"The size of the value parameter ({value.size}) does not match nHiddenNodes ({nHiddenNodes})");
            }
            parameters.SetRange(0, nHiddenNodes, value);
        }    }

    private vector HiddenScales
    {
        get { return parameters.Slice(nHiddenNodes, 2 * nHiddenNodes); }
        set{parameters.SetRange(nHiddenNodes,2*nHiddenNodes,value);}
    }

    private vector HiddenShifts
    {
        get { return parameters.Slice(2 * nHiddenNodes, 3 * nHiddenNodes); }
        set{parameters.SetRange(2*nHiddenNodes,3*nHiddenNodes,value);}
    }


    public void InitiliaseParameters()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < 3*nHiddenNodes; i++)
        {
            parameters[i] = random.NextDouble() - 0.5;
        }
    }

    public double Response(double x)
    {
        double sum = 0;
        for (int i = 0; i < nHiddenNodes; i++)
        {
            double a = HiddenShifts[i];
            double b = HiddenScales[i];
            double w = HiddenWeights[i];
            double y = activationFunction((x - a) / b) * w;
            sum += y;
        }
        return sum;
    }

    public double CostFunction(vector p, vector input, vector target)
    {
        HiddenWeights = p.Slice(0, nHiddenNodes);
        HiddenScales = p.Slice(nHiddenNodes, 2 * nHiddenNodes);
        HiddenShifts = p.Slice(2 * nHiddenNodes, 3 * nHiddenNodes);

        double sum = 0;
        for (int i = 0; i < input.size; i++)
        {
            sum += System.Math.Pow(Response(input[i]) - target[i], 2);
        }
        return sum;
    }
    
    
    public void Train(vector input, vector target)
    {
        var min = new minimisationclass(p => CostFunction(p, input,target), parameters);
        parameters = min.MinimiseQN();
        HiddenWeights = parameters.Slice(0, nHiddenNodes);
        HiddenScales = parameters.Slice(nHiddenNodes, 2*nHiddenNodes);
        HiddenShifts = parameters.Slice(2*nHiddenNodes,3*nHiddenNodes);
    }

}