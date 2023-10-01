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
    public Calculator Calculator { get; set; }

    public PlotModel<double> TimePlot { get; set; }

    public int Size { get; set; } = 200;

    public List<MethodInfo> Methods { get; set; }

    public MethodInfo SelectedMethod { get; set; }

    public List<MethodInfo> Complexities { get; set; }

    public MethodInfo SelectedComplexity { get; set; }

    public RelayCommand CalculateCommand { get; set; }

    public bool MaxPerformance { get; set; } = false;

    public ViewModel()
    {
        Calculator = new Calculator();
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());

        Methods = GetTypeMethods(typeof(TestingMethods)).ToList();
        Methods.AddRange(GetTypeMethods(typeof(TestingMatrixMethods)));

        Complexities = GetTypeMethods(typeof(TimeComplexity)).ToList();

        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
        if (Complexities.Count != 0)
            SelectedComplexity = Complexities[0];

        CalculateCommand = new RelayCommand(
            execute =>
            {
                Calculator.Calculate(Size, SelectedMethod, TimePlot, MaxPerformance);
                Approximator.Approximate(TimePlot.Approximation, 
                    SelectedMethod, SelectedComplexity, Size, 100);
            });
    }

    private MethodInfo[] GetTypeMethods(Type type)
    {
        return type.GetMethods(BindingFlags.Public | BindingFlags.Static);
    }

}