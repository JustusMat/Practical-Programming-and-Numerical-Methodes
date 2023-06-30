public class DifferentialEquationNetwork : SimpleNeuralNetwork
{
    private System.Func<System.Func<double, double>, System.Func<double, double>, System.Func<double, double>, double,
        double> Phi;

    public DifferentialEquationNetwork(int n, string activation = "Sigmoid",
        System.Func<System.Func<double, double>, System.Func<double, double>, System.Func<double, double>, double,
            double> Phi = null) : base(n, activation)
    {
        this.Phi = Phi;
    }
     
    public double CostFunctionDifferentialEquation(vector p, vector input, double alpha, double beta)
    {
        HiddenWeights = p.Slice(0, nHiddenNodes);
        HiddenScales = p.Slice(nHiddenNodes, 2 * nHiddenNodes);
        HiddenShifts = p.Slice(2 * nHiddenNodes, 3 * nHiddenNodes);

        double a = input[0];
        double b = input[1];
        double c = input[2];
        double y_c = input[3];
        double yp_c = input[4];

        //double alpha = 2.0;
        //double beta = 2.0;
        double sum = 0.0;

        System.Func<double, double> ypp = x => ResponseSecondDerivative(x);
        System.Func<double, double> yp = x => ResponseDerivative(x);
        System.Func<double, double> y = x => Response(x);

        System.Func<System.Func<double, double>, System.Func<double, double>, System.Func<double, double>, double,
            double> Phi2Delegate =
            (yppFunc, ypFunc, yFunc, xVal) =>
            {
                var phiResult = Phi(yppFunc, ypFunc, yFunc, xVal);
                return phiResult*phiResult;
            };


        sum += alpha * System.Math.Pow(y(c) - y_c, 2);
        sum += beta * System.Math.Pow(yp(c) - yp_c, 2);

        
        /*
        var placeHolderNetwork = new SimpleNeuralNetwork(10);
        double[] xArray = new double[25];
        double[] yArray = new double[25];
        double dX = (b - a) / 25;
        double xval = 0;
        for (int i = 0; i < 25; i++)
        {
            xArray[i] = dX * i;
            yArray[i] = Phi2Delegate(ypp, yp, y, xArray[i]);
        }
        placeHolderNetwork.Train(xArray, yArray);

        
        sum += placeHolderNetwork.ResponseAntiDerivative(b) - placeHolderNetwork.ResponseAntiDerivative(a);
        */
        
        sum += integrator.RAIntegrate(x => Phi2Delegate(ypp, yp, y, x), a, b);
        return sum;
    }

    
    public void TrainDifferentialEquation(vector input, double alpha = 0.5, double beta = 0.5)
    {
        
        var min = new Simplex(p => CostFunctionDifferentialEquation(p, input, alpha, beta), parameters);
        (vector parameterVector, int iterations) = min.Downhill();
        parameters = parameterVector;
        System.Console.Error.WriteLine($"Simplex downhill method terminated at {iterations}");
        HiddenWeights = parameters.Slice(0, nHiddenNodes);
        HiddenScales = parameters.Slice(nHiddenNodes, 2*nHiddenNodes);
        HiddenShifts = parameters.Slice(2*nHiddenNodes,3*nHiddenNodes);
    }
}