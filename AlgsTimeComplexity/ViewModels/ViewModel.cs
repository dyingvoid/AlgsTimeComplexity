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

    private MethodInfo _complexity;

    private int _size = 500;

    private double _time = 0.00005;
    public MethodInfo SelectedComplexity
    {
        get => _complexity;
        set
        {
            _complexity = value;
            CalculateApproximation();
        }
    }

    public int Size
    {
        get => _size;
        set
        {
            _size = value;
            CalculateApproximation();
        } 
    }

    public double Time
    {
        get => _time;
        set
        {
            CalculateApproximation();
        } 
    }
    
    public List<MethodInfo> Methods { get; set; }
    
    public MethodInfo SelectedMethod { get; set; }
    
    public RelayCommand CalculateCommand { get; set; }

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
            execute => Calculator.Calculate(Size, Time,
                SelectedMethod, SelectedComplexity, TimePlot, MaxPerformance));
    }

    private void CalculateApproximation()
    {
        if(TimePlot.Approximation.Count > 0)
            TimePlot.Approximation.Clear();
        
        Calculator.CalculateApproximation(Size, Time, _complexity, TimePlot.Approximation);
    }
}