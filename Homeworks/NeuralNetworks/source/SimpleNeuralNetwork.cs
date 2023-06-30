public partial class SimpleNeuralNetwork
{
    protected int nHiddenNodes;
    private System.Func<double, double> activationFunction;
    private System.Func<double, double> activationFunctionDerivative;
    private System.Func<double, double> activationFunctionSecondDerivative;
    private System.Func<double, double> activationFunctionAntiDerivative;

    protected vector parameters;

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

    protected vector HiddenWeights
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

    protected vector HiddenScales
    {
        get { return parameters.Slice(nHiddenNodes, 2 * nHiddenNodes); }
        set{parameters.SetRange(nHiddenNodes,2*nHiddenNodes,value);}
    }

    protected vector HiddenShifts
    {
        get { return parameters.Slice(2 * nHiddenNodes, 3 * nHiddenNodes); }
        set{parameters.SetRange(2*nHiddenNodes,3*nHiddenNodes,value);}
    }


    public void InitiliaseParameters()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < 3*nHiddenNodes; i++)
        {
            if (i < nHiddenNodes)
            {
                parameters[i] = 2*random.NextDouble() - 1;
            }
            else
            {
                parameters[i] = 1.0;
            }
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
        var min = new Simplex(p => CostFunction(p, input,target), parameters);
        (vector parameterVector, int iterations) = min.Downhill();
        parameters = parameterVector;
        System.Console.Error.WriteLine($"Simplex downhill method terminated at {iterations}");
        HiddenWeights = parameters.Slice(0, nHiddenNodes);
        HiddenScales = parameters.Slice(nHiddenNodes, 2*nHiddenNodes);
        HiddenShifts = parameters.Slice(2*nHiddenNodes,3*nHiddenNodes);
    }
    
    public double CostFunctionDifferentialEquation2(vector p, System.Func<double, double, double, double, double> Phi, vector input)
    {
        HiddenWeights = p.Slice(0, nHiddenNodes);
        HiddenScales = p.Slice(nHiddenNodes, 2 * nHiddenNodes);
        HiddenShifts = p.Slice(2 * nHiddenNodes, 3 * nHiddenNodes);

        double a = input[0];
        double b = input[1];
        double c = input[2];
        double y_c = input[2];
        double yp_c = input[2];
        
        double alpha = 0.5;
        double beta = 0.25;
        double sum = 0.0;
        
        System.Func<double, double> ypp = x => ResponseSecondDerivative(x);
        System.Func<double, double> yp = x => ResponseDerivative(x);
        System.Func<double, double> y = x => Response(x);
        
        sum += alpha * System.Math.Pow(y(c) - y_c, 2);
        sum += beta * System.Math.Pow(yp(c) - yp_c, 2);

        int numIntervals = 100;
        double dx = (b - a) / numIntervals;
        double integral = 0.0;
        for (int i = 0; i < numIntervals; i++)
        {
            double x = a + i * dx;
            integral += System.Math.Pow(Phi(ypp(x), yp(x), y(x), x), 2);
        }

        sum += integral * dx;
        return sum;
    }
}