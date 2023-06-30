using System.Linq;
public class Simplex
{
    private const double Epsilon = 1e-6;
    private const int MaxIterations = 100000;

    private readonly System.Func<vector, double> _phiObjectiveFunction;
    private readonly int _dimension;
    private vector[] _simplex;
    private double[] _evaluationArray;

    public Simplex(System.Func<vector, double> phiObjectiveFunction, vector initialGuess)
    {
        _phiObjectiveFunction = phiObjectiveFunction;
        _dimension = initialGuess.size;
        InitialiseSimplex(initialGuess);
    }

    private void InitialiseSimplex(vector initialGuess, double step = 1.0)
    {
        _simplex = new vector[_dimension + 1];
        _evaluationArray = new double[_dimension + 1];
        _simplex[_dimension] = initialGuess.copy();
        _evaluationArray[_dimension] = _phiObjectiveFunction(_simplex[_dimension]);
        for (int i = 0; i < _dimension; i++)
        {
            initialGuess[i] += step;
            _simplex[i] = initialGuess.copy();
            _evaluationArray[i] = _phiObjectiveFunction(_simplex[i]);
            initialGuess[i] -= step;
        }
    }
    
    public (vector, int) Downhill()
    {
        int indexLowestPoint = 0;
        
        int iterations = 0;
        while (TerminationCriteria() && iterations++ < MaxIterations)
        {
            int indexHighestPoint;
            DetermineHighestAndLowestIndices(out indexHighestPoint, out indexLowestPoint);
            double highestValue = _evaluationArray[indexHighestPoint];
            double lowestValue = _evaluationArray[indexLowestPoint];
            
            //simplex centroid
            vector centroid = CalculateCentroid(indexHighestPoint);

            //Expansion
            vector expansion = 3.0 * centroid - 2.0 * _simplex[indexHighestPoint];
            double expansionValue = _phiObjectiveFunction(expansion);
            if (expansionValue < lowestValue) //expansion
            {
                _simplex[indexHighestPoint] = expansion;
                _evaluationArray[indexHighestPoint] = expansionValue;
            }
            else //try reflection
            {
                vector reflection = 2.0 * centroid - _simplex[indexHighestPoint];
                double reflectionValue = _phiObjectiveFunction(reflection);
                if (reflectionValue < highestValue) //reflection
                {
                    _simplex[indexHighestPoint] = reflection;
                    _evaluationArray[indexHighestPoint] = reflectionValue;
                }
                else // try contraction
                {
                    vector contraction = (centroid + _simplex[indexHighestPoint]) / 2.0;
                    double contractionValue = _phiObjectiveFunction(contraction);
                    if (contractionValue < highestValue)  // Contraction
                    {
                        _simplex[indexHighestPoint] = contraction;
                        _evaluationArray[indexHighestPoint] = contractionValue;
                    }
                    else //Reduction
                    {
                        PerformReduction(indexLowestPoint);
                    }
                }
            }
        }

        return (_simplex[indexLowestPoint], iterations);
    }


    private void DetermineHighestAndLowestIndices(out int indexHighestPoint, out int indexLowestPoint)
    {
        indexHighestPoint = _evaluationArray.ToList().IndexOf(_evaluationArray.Max());
        indexLowestPoint = _evaluationArray.ToList().IndexOf(_evaluationArray.Min());
    }
    
    private vector CalculateCentroid(int indexHighestPoint)
    {
        vector vec = new vector(_dimension);
        for (int i = 0; i < _simplex.Length; i++)
        {
            if (i!=indexHighestPoint)
            {
                vec += _simplex[i];
            }
        }

        return vec / vec.size;
    }
    
    private void PerformReduction(int indexLowestPoint)
    {
        for (int i = 0; i < _simplex.Length; i++)
        {
            if (i != indexLowestPoint)
            {
                _simplex[i] = (_simplex[i] + _simplex[indexLowestPoint]) / 2.0;
                _evaluationArray[i] = _phiObjectiveFunction(_simplex[i]);
            }
        }
    }
    
    public bool TerminationCriteria()
    {
        double val = 0.0;
        for (int i = 1; i < _dimension; i++)
        {
            val = System.Math.Max(val, (_simplex[0] - _simplex[i]).norm());
        }

        return val > Epsilon;
    }
}