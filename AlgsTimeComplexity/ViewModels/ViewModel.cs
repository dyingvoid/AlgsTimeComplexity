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

    public RelayCommand CalculateCommand { get; set; }
    
    public List<MethodInfo> Methods { get; set; }
    
    public MethodInfo SelectedMethod { get; set; }

    public Calculator Calculator { get; set; }

    public ViewModel()
    {
        Calculator = new Calculator();
        TimePlot = new PlotModel<double>(new ObservableCollection<double>());
        Methods = typeof(TestingMethods).GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
        Methods.AddRange(
            typeof(TestingMatrixMethods).GetMethods(BindingFlags.Public | BindingFlags.Static)
            );
        
        if (Methods.Count != 0)
            SelectedMethod = Methods[0];
        
        CalculateCommand = new RelayCommand(
            execute => Calculator.Calculate(20, SelectedMethod, TimePlot.List)
            );
    }
}