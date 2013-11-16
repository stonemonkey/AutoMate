using Caliburn.Micro;
using Ui.Wp8.Components.RecordingPage;

namespace Ui.Wp8.Components.MainPage
{
    public class MainPageViewModel : PageViewModelBase
    {
        private readonly UserContextViewModel _userContext;

        public UserContextViewModel UserContext
        {
            get { return _userContext; }
        }

        public MainPageViewModel(INavigationService navigationService, UserContextViewModel userContext)
            : base(navigationService)
        {
            _userContext = userContext;
        }

        public void Start()
        {
            NavigationService.UriFor<RecordingPageViewModel>()
                .Navigate();
        }
    }
}