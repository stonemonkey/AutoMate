using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Ui.Wp8.Components.RecordingPage;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8.Components.MainPage
{
    public class MainPageViewModel : PageViewModelBase
    {
        private readonly UserContextViewModel _userContext;
        private readonly StatisticsViewModel _statistics;
        private readonly WebServiceProxy _serviceProxy;

        public UserContextViewModel UserContext
        {
            get { return _userContext; }
        }

        public StatisticsViewModel Statistics
        {
            get { return _statistics; }
        }

        public MainPageViewModel(
            INavigationService navigationService, UserContextViewModel userContext, StatisticsViewModel statistics)
                : base(navigationService)
        {
            _serviceProxy = new WebServiceProxy("http://automatewebui.azurewebsites.net/api/automate")
            {
                Encoder = new JsonEncoder()
            };
            _userContext = userContext;
            _statistics = statistics;
        }

        public void Start()
        {
            NavigationService.UriFor<RecordingPageViewModel>()
                .Navigate();
        }

        protected async override void OnActivate()
        {
            base.OnActivate();

            if (_statistics.IsUnsent && IsNetworkAvailable())
            {
                await TryUpdateStatistics();
            }
        }

        private async Task TryUpdateStatistics()
        {
            var response = await _serviceProxy.Post(_statistics.Data);
            if (WasSuccessfullySent(response))
            {
                _statistics.MarkAsSent();
            }
            else
            {
                MessageBox.Show(response);
            }
        }

        private static bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        private static bool WasSuccessfullySent(string response)
        {
            return string.IsNullOrEmpty(response) ||
                   response.StartsWith("HTTP/1.1 2");
        }
    }
}