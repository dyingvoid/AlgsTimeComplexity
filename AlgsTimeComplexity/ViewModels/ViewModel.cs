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
    
    public List<MethodInfo> TimeComplexities { get; set; }
    
    public MethodInfo SelectedComplexity { get; set; }
    
    public List<MethodInfo> Methods { get; set; }
    
    public MethodInfo SelectedMethod { get; set; }
    
    public RelayCommand CalculateCommand { get; set; }

    public int Size { get; set; } = 1000;

    public double Time { get; set; } = 0.00005;

    public bool MaxPerformance { get; set; } = false;

    public ViewModel()
    {
        Calculator = new Calculator();
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());
        
        TimeComplexities = typeof(TimeComplexity)
            .GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
        
        Methods = typeof(TestingMethods)
            .GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
        Methods.AddRange(
            typeof(TestingMatrixMethods).GetMethods(BindingFlags.Public | BindingFlags.Static)
            );
        
        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
        if (TimeComplexities.Count != 0)
            SelectedComplexity = TimeComplexities[0];
        
        CalculateCommand = new RelayCommand(
            execute => Execute());
    }

    private void Execute()
    {
        Calculator.Calculate(Size, Time,
            SelectedMethod, SelectedComplexity, TimePlot, MaxPerformance);
    }
}