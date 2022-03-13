using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.UI.ViewModels.AbstractMediator
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Mediator-Pattern Colleague element with single reference to Mediator element (interface).
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
