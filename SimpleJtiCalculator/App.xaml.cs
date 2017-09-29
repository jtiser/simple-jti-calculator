using System.Windows;
using Microsoft.Practices.Unity;
using SimpleJtiCalculator.Services;
using SimpleJtiCalculator.Views;

namespace SimpleJtiCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Dependencies injection, using Unity
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICalculatorService, CalculatorService>();

            // Create the ViewModel and expose it using the View's DataContext
            var mainWindow = container.Resolve<CalculatorView>();
            Current.MainWindow = mainWindow;
            Current.MainWindow.Show();
        }
    }
}
