using System;
using System.Diagnostics;

namespace AlgsTimeComplexity.Models;

public static class TestingMatrixMethods
{
    public static double Product(int[][] m1, int[][] m2, int size)
    {
        var emptyMatrix = CreateEmptyMatrix(size);

        var watch = Stopwatch.StartNew();
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var temp = 0;

                for (var k = 0; k < size; k++)
                {
                    temp += m1[i][k] * m2[k][j];
                }

                emptyMatrix[i][j] = temp;
            }
        }
        watch.Stop();

        return watch.Elapsed.TotalMilliseconds;
    }

    private static int[][] CreateEmptyMatrix(int size)
    {
        var matrix = new int[size][];
        
        for(var i = 0; i < size; i++)
            matrix[i] = new int[size];

        return matrix;
    }
}