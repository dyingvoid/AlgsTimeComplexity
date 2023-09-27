using System;
using System.Reflection;

namespace AlgsTimeComplexity.Models;

public static class Generator
{
    public static object[] GenerateParameters(MethodInfo methodInfo, int size)
    {
        if (methodInfo.DeclaringType == typeof(TestingMethods))
        {
            
            return new object[] { GenerateArray(size + 1), size + 1 };
            
        }
        else
        {
            return new object[] { GenerateMatrix(size + 1), GenerateMatrix(size + 1), size + 1 };
        }
    }

    private static int[] GenerateArray(int size)
    {
        var arr = new int[size];
        var random = new Random();

        for (var i = 0; i < size; i++)
            arr[i] = random.Next(0, 1000);

        return arr;
    }

    private static int[][] GenerateMatrix(int size)
    {
        var matrix = new int[size][];

        for (var i = 0; i < size; i++)
            matrix[i] = GenerateArray(size);

        return matrix;
    }
}