using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace AlgsTimeComplexity.Models;

public class PlotModel<T> : ObservableObject
{
    public ISeries[] Series { get; set; }
    
    public LabelVisual Title { get; set; }
    
    public ObservableCollection<T> List { get; set; }
    
    public ObservableCollection<T> Approximation { get; set; }
    
    public object Sync { get; } = new object();

    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            Name = "Number of elements",
        }
    };

    private Axis[] _yAxes;
    
    public Axis[] YAxes
    {
        get => _yAxes;
        set
        {
            _yAxes = value;
            SetProperty(ref _yAxes, value);
            OnPropertyChanged();
        } 
    }

    public PlotModel(ObservableCollection<T> list)
    {
        List = list;
        Approximation = new ObservableCollection<T>();
        YAxes = new[] { new Axis { Name = "Time, milliseconds(ms)" } };
        
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

    public void ChangeYAxesName(MethodInfo method)
    {
        string name;
        name = method.DeclaringType == typeof(SecondTaskTestingMethods) ? 
            "Number of operations." : "Time, milliseconds(ms)";
       
        YAxes = new[] { new Axis { Name = name } };
    }
}