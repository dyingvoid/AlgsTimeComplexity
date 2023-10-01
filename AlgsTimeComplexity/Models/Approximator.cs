using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AlgsTimeComplexity.Models;

public static class Approximator
{
    public static void Approximate(ObservableCollection<double> list,
        MethodInfo algorithm, MethodInfo complexity,
        int size, int iterations)
    {
        var averageTimings = GetAverageTimings(algorithm, size, iterations);
        var complexityValues = GetComplexityValues(complexity, size);

        MakeRelations(averageTimings, complexityValues);
        
        var mean = averageTimings.Mean();
        
        FillApproximationLine(list, mean, complexityValues);
    }

    private static void FillApproximationLine(ObservableCollection<double> line, double coefficient,
        double[] complexityValues)
    {
        foreach (var complexityValue in complexityValues)
        {
            line.Add(coefficient * complexityValue);
        }
    }
    
    private static double[] GetComplexityValues(MethodInfo complexity, int size)
    {
        var values = new double[size];
        
        for (var i = 1; i <= size; i++)
        {
            values[i - 1] = GetComplexityValue(complexity, i);
        }

        return values;
    }

    private static void MakeRelations(double[] timings, double[] complexityValues)
    {
        timings.Divide(complexityValues);
    }

    private static double[] GetAverageTimings(MethodInfo algorithm, int size, int iterations)
    {
        var timings = new double[size];
        var parameters = Enumerable.Range(1, size);

        for (var i = 0; i < iterations; i++)
        {
            var results = Calculator.CalculateMethod(algorithm, size);
            timings.Add(results);
        }

        timings.Divide(iterations);

        return timings;
    }

    private static double GetComplexityValue(MethodInfo complexityFunction, int size)
    {
        return (double)complexityFunction.Invoke(null, new object?[] { size });
    }
}