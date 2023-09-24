using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace AlgsTimeComplexity.Models;

public class Calculator
{
    public void Calculate(int size, MethodInfo methodInfo, ObservableCollection<double> plot)
    {
        if (plot.Count != 0)
            plot.Clear();

        var parameters = GenerateParameters(methodInfo, size);

        for (var i = 0; i < size; i++)
        {
            var closureTemp = i;

            Task.Run(() =>
            {
                var timeSpan = (TimeSpan)methodInfo.Invoke(null, (object[])parameters[closureTemp]);
                plot.Add(timeSpan.TotalMilliseconds);
            });
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