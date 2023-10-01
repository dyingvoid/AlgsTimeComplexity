using System;
using System.Linq;
using System.Threading.Tasks;


namespace AlgsTimeComplexity.Models;

public class Filter
{
    public void FindPeaks(PlotModel<double> plot, int percentile)
    {
        if (plot.List.Count == 1)
            return;

        var sorted = plot.List.ToArray();
        Array.Sort(sorted);

        var elementIdx = percentile * (plot.List.Count + 1) / 100 - 1;
        var elementVal = sorted[elementIdx];

        Parallel.For(0, plot.List.Count, idx =>
        {
            if (plot.List[idx] > elementVal)
                plot.List[idx] = plot.Approximation[idx];
        });
    }
}