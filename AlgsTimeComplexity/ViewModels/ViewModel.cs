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
    
    public RelayCommand CalculateCommand { get; set; }

    public bool MaxPerformance { get; set; } = false;

    public ViewModel()
    {
        Calculator = new Calculator();
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());
        
        Methods = typeof(TestingMethods)
            .GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
        Methods.AddRange(
            typeof(TestingMatrixMethods).GetMethods(BindingFlags.Public | BindingFlags.Static)
            );
        
        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
        
        CalculateCommand = new RelayCommand(
            execute =>
            {
                Calculator.Calculate(Size, SelectedMethod, TimePlot, MaxPerformance);
                Approximator.Approximate(TimePlot.Approximation, SelectedMethod, Size, 100);
            });
    }
}