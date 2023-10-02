using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using AlgsTimeComplexity.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AlgsTimeComplexity.ViewModels;

public class ViewModel : ObservableObject
{
    public PlotModel<double> TimePlot { get; set; }

    public int Size { get; set; } = 200;

    public List<MethodInfo> Methods { get; set; }

    public MethodInfo SelectedMethod { get; set; }

    public List<MethodInfo> Complexities { get; set; }

    public MethodInfo SelectedComplexity { get; set; }

    public RelayCommand CalculateCommand { get; set; }

    public RelayCommand FilterCommand { get; set; }

    public int Percentile { get; set; } = 95;

    public int Iterations { get; set; } = 50;

    public ViewModel()
    {
        var filter = new Filter();
        
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());

        Methods = GetTypeMethods(typeof(TestingMethods)).ToList();
        Methods.AddRange(GetTypeMethods(typeof(TestingMatrixMethods)));
        Methods.AddRange(GetTypeMethods(typeof(SecondTaskTestingMethods)));

        Complexities = GetTypeMethods(typeof(TimeComplexity)).ToList();

        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
        if (Complexities.Count != 0)
            SelectedComplexity = Complexities[0];

        CalculateCommand = new RelayCommand(
            execute =>
            {
                TimePlot.ChangeYAxesName(SelectedMethod);
                Calculator.Calculate(Size, SelectedMethod, TimePlot);
                Approximator.Approximate(TimePlot.Approximation,
                    SelectedMethod, SelectedComplexity, Size, Iterations);
            });
        FilterCommand = new RelayCommand(execute => filter.FindPeaks(TimePlot, Percentile));
    }

    private MethodInfo[] GetTypeMethods(Type type)
    {
        return type.GetMethods(BindingFlags.Public | BindingFlags.Static);
    }
}