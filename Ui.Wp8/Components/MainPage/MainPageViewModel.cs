using Caliburn.Micro;
using Ui.Wp8.Components.RecordingPage;

namespace Ui.Wp8.Components.MainPage
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public void Start()
        {
            NavigationService.UriFor<RecordingPageViewModel>()
                .Navigate();
        }
    }
}