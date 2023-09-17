using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace AlgsTimeComplexity.Models;

public class PlotModel<T>
{
    public ISeries[] Series { get; set; }
    public LabelVisual Title { get; set; }
    public ObservableCollection<T> List { get; set; }
    public object Sync { get; } = new object();

    public PlotModel(ObservableCollection<T> list)
    {
        List = list;
        var meme = new List<double>();
        var meme2 = new List<double>();
        var meme3 = new List<double>();
        for (var i = 1; i < 2000; i += 1)
        {
            meme.Add(0.0001 * i);
            meme2.Add(0.0001 * i * Double.Log2(i));
            meme3.Add(0.0001 * i * i);
        }

        Series = new ISeries[]{
            new LineSeries<T>
            {
                Values = List,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0
            }
        };
        
        Title = new LabelVisual
        {
            Text = "Speed",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(15),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };
    }
}