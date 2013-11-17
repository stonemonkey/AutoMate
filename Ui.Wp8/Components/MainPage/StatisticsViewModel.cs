using Caliburn.Micro;

namespace Ui.Wp8.Components.MainPage
{
    public class StatisticsViewModel : Screen
    {
        private readonly UserContextViewModel _userContext;
        private int _agresivity;

        public int Agresivity
        {
            get { return _agresivity; } 
            set
            {
                _agresivity = value;
                NotifyOfPropertyChange(() => Agresivity);
            }
        }

        public StatisticsViewModel(UserContextViewModel userContext)
        {
            _userContext = userContext;
        }

        public void MarkAsSent()
        {
            // TODO:
        }
    }
}