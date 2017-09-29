using System.Windows;
using Microsoft.Practices.Unity;
using SimpleJtiCalculator.ViewModels;

namespace SimpleJtiCalculator.Views
{
    /// <summary>
    /// Interaction logic for CalculatorView.xaml
    /// </summary>
    public partial class CalculatorView : Window
    {
        [Dependency]
        public CalculatorViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public CalculatorView()
        {
            InitializeComponent();
        }
    }
}