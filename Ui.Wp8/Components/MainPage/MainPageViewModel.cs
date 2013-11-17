using System.Threading.Tasks;
using Caliburn.Micro;
using Dto;
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

            var response = await SendStatistics();
            if (ContainsError(response))
            {
                //_statistics.MarkAsSent();
            }
        }

        private static bool ContainsError(string response)
        {
            return !string.IsNullOrEmpty(response) &&
                   !response.StartsWith("HTTP/1.1 2");
        }

        private async Task<string> SendStatistics()
        {
            var clientStatistics = new ClientStatistics
            {
                AgresivityRate = 90, //_statistics.Agresivity,
                EmailAddress = "test@test.com", //_userContext.Email,
                Location = "Lespezi", //_userContext.Location,
            };

            return await _serviceProxy.Post(clientStatistics);
        }
    }
}