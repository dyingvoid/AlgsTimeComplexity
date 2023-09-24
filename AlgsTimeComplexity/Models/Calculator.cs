using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace AlgsTimeComplexity.Models;

public class Calculator
{
    public void Calculate(int size, double time, 
        MethodInfo testMethod, MethodInfo approximationMethod, PlotModel<double> plot)
    {
        if (plot.List.Count != 0 || plot.Approximation.Count != 0)
        {
            plot.List.Clear();
            plot.Approximation.Clear();
        }

        bool check = GC.TryStartNoGCRegion(Int32.MaxValue, true);
        CalculateExperimentTime(size, testMethod, plot.List);
        CalculateApproximation(size, time, approximationMethod, plot.Approximation);
        GC.EndNoGCRegion();
    }

    private void CalculateApproximation(int size, double time, 
        MethodInfo complexity, ObservableCollection<double> complexityPlot)
    {
        for (var i = 1; i < size; i++)
        {
            var timeApproximation = (double)complexity.Invoke(null, new object[] { time, i });
            complexityPlot.Add(timeApproximation);
        }
    }

    private async void CalculateExperimentTime(int size, MethodInfo methodInfo, ObservableCollection<double> plot)
    {
        var parameters = GenerateParameters(methodInfo, size);

        for (var i = 0; i < size; i++)
        {
            var closureTemp = i;
            TimeSpan timeSpan = TimeSpan.Zero;

            await Task.Run(() =>
            {
                timeSpan = (TimeSpan)methodInfo.Invoke(null, (object[])parameters[closureTemp]);
            });
            
            plot.Add(timeSpan.TotalMilliseconds);
        }
    }

    private object[] GenerateParameters(MethodInfo methodInfo, int size)
    {
        var parameters = new object[size];

        if (methodInfo.DeclaringType == typeof(TestingMethods))
        {
            for (var i = 0; i < size; i++)
            {
                parameters[i] = new object[] { GenerateArray(i + 1), i + 1 };
            }
        }
        else
        {
            for (var i = 0; i < size; i++)
            {
                parameters[i] = new object[] { GenerateMatrix(size), GenerateMatrix(size), i + 1 };
            }
        }

        return parameters;
    }

    private int[] GenerateArray(int size)
    {
        var arr = new int[size];
        var random = new Random();

        for (var i = 0; i < size; i++)
            arr[i] = random.Next(0, 1000);

        return arr;
    }

    private int[][] GenerateMatrix(int size)
    {
        var matrix = new int[size][];

        for (var i = 0; i < size; i++)
            matrix[i] = GenerateArray(size);

        return matrix;
    }
}