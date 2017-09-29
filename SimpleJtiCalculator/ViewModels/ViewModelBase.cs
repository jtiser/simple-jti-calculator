using System.ComponentModel;

namespace SimpleJtiCalculator.ViewModels
{
    /// <summary>
    ///     This class provides common functionality for ViewModel class.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}