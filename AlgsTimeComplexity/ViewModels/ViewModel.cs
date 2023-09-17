using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using AlgsTimeComplexity.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AlgsTimeComplexity.ViewModels;

public class ViewModel : ObservableObject
{
    public PlotModel<double> TimePlot { get; set; }

    public RelayCommand CalculateCommand => 
        new RelayCommand(execute => CalculateParallel(2000));
    
    public MethodInfo[] Methods { get; set; }= 
        typeof(TestingMethods).GetMethods(BindingFlags.Public | BindingFlags.Static);
    
    public MethodInfo SelectedMethod { get; set; }

    public ViewModel()
    {
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());
        if (Methods.Length != 0)
            SelectedMethod = Methods[0];
    }

    private async void CalculateParallel(int maxSize)
    {
        if(TimePlot.List.Count != 0)
            TimePlot.List.Clear();
        
        var mainList = GenerateList(maxSize);
        
        for (var size = 1; size < maxSize; size++)
        {
            var list = GenerateList(size);
            TimeSpan ts = TimeSpan.Zero;
            double ms = 0;
            
            Parallel.Invoke(
                () =>
                {
                    ts = (TimeSpan)SelectedMethod.Invoke(null, new object?[] { list, size });
                    ms = ts.TotalMilliseconds;
                }
            );

            await Task.Run(() => { TimePlot.List.Add(ms); });
        }
    }

    private async void CalculateAsync(int maxSize)
    {
        if(TimePlot.List.Count != 0)
            TimePlot.List.Clear();
        
        var mainList = GenerateList(maxSize);
        
        for (var size = 1; size < maxSize; size++)
        {
            var list = GenerateList(size);
            TimeSpan ts = TimeSpan.Zero;
            double ms = 0;
            
            await Task.Run(() =>
                {
                    ts = (TimeSpan)SelectedMethod.Invoke(null, new object?[] { list, size });
                    ms = ts.TotalMilliseconds;
                }
            );

            lock (TimePlot.Sync)
            {
                TimePlot.List.Add(ms);
            }
        }
    }
    
    private List<int> GenerateList(int size)
    {
        var list = new List<int>();
        var random = new Random();
        
        if (size is < 0 or > 2000)
            return list;

        for (var i = 0; i < size; i++)
        {
            list.Add(random.Next(0, 1000000));
        }

        return list;
    }
}