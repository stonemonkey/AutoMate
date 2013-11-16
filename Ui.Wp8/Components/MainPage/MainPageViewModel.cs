using System.Windows;
using Caliburn.Micro;

namespace Ui.Wp8.Components.MainPage
{
    public class MainPageViewModel : PropertyChangedBase
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                NotifyOfPropertyChange(() => IsEnabled);
                NotifyOfPropertyChange(() => CanShowName);
            }
        }

        public bool CanShowName
        {
            get { return IsEnabled; }
        }

        public void ShowName()
        {
            MessageBox.Show("Clicked");
        }
    }
}