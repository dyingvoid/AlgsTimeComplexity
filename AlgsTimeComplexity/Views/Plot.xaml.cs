using System.Windows.Controls;
using AlgsTimeComplexity.ViewModels;

namespace AlgFirstLab.Views;

public partial class Plot : UserControl
{
    public Plot()
    {
        InitializeComponent();
        this.DataContext = new ViewModel();
    }
}