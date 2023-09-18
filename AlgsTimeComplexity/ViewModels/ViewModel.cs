using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    
    public List<MethodInfo> Methods { get; set; }
    
    public MethodInfo SelectedMethod { get; set; }

    public ViewModel()
    {
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());
        Methods = typeof(TestingMethods).GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
        Methods.AddRange(
            typeof(TestingMatrixMethods).GetMethods(BindingFlags.Public | BindingFlags.Static)
            );
        
        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
    }

    private async void CalculateParallel(int maxSize)
    {
        if(TimePlot.List.Count != 0)
            TimePlot.List.Clear();

        for (var size = 1; size < maxSize; size++)
        {
            double ms = 0;

            var param = SelectedMethod.DeclaringType == typeof(TestingMethods) 
                ? new object?[] { GenerateArray(size), size } 
                : new object?[] { GenerateMatrix(size), GenerateMatrix(size), size };
            
            Parallel.Invoke(
                () =>
                {
                    TimeSpan ts = (TimeSpan)SelectedMethod.Invoke(null, param);
                    ms = ts.TotalMilliseconds;
                }
            );

            await Task.Run(() => { TimePlot.List.Add(ms); });
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
            list.Add(random.Next(0, 1000));
        }

        return list;
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
        
        for(var i = 0; i < size; i++)
            matrix[i] = GenerateArray(size);

        return matrix;
    }
}