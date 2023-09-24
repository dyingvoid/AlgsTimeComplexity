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
    
    public ObservableCollection<T> Approximation { get; set; }
    
    public object Sync { get; } = new object();

    public PlotModel(ObservableCollection<T> list)
    {
        List = list;
        Approximation = new ObservableCollection<T>();
        
        Series = new ISeries[]{
            new LineSeries<T>
            {
                Values = List,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0
            },
            new LineSeries<T>
            {
                Values = Approximation,
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