using System.Linq;

namespace AlgsTimeComplexity.Models;

public static class ArrayExtensions
{
    public static double Mean(this double[] arr)
    {
        var s = arr.Sum();
        return s / arr.Length;
    }

    public static void Divide(this double[] arr, int number)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] /= number;
        }
    }

    public static void Divide(this double[] arr, double[] other)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            var division = arr[i] / other[i];
            if (double.IsPositiveInfinity(division) ||
                double.IsNegativeInfinity(division) ||
                double.IsNaN(division))
                arr[i] = 0;
            else
                arr[i] /= other[i];
        }
    }

    public static void Add(this double[] l, double[] r)
    {
        for (var i = 0; i < l.Length; i++)
        {
            l[i] += r[i];
        }
    }
}