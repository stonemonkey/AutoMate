using System.Windows;
using Caliburn.Micro;
using Ui.Wp8.Components.MainPage;

namespace Ui.Wp8.Components.RecordingPage
{
    public class RecordingPageViewModel : PageViewModelBase
    {
        private readonly MainPageViewModel _mainViewModel;
        public AccelerationViewModel AccelerationViewModel { get; private set; }
        public GpsDataViewModel GpsDataViewModel { get; private set; }

        public RecordingPageViewModel(INavigationService navigationService, MainPageViewModel mainViewModel,
            AccelerationViewModel accelerationViewModel, GpsDataViewModel gpsDataViewModel)
                : base(navigationService)
        {
            _mainViewModel = mainViewModel;
            AccelerationViewModel = accelerationViewModel;
            GpsDataViewModel = gpsDataViewModel;
        }

        public async void Stop()
        {
            if (IsActiveUserOkToStopRecording())
            {
                await _mainViewModel.Init();
                NavigationService.GoBack();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            AccelerationViewModel.Start();
            GpsDataViewModel.Start();
        }

        private static bool IsActiveUserOkToStopRecording()
        {
            var response = MessageBox.Show(
                RecordingPageResources.GoBackMessage,
                RecordingPageResources.ApplicationTitle, 
                MessageBoxButton.OKCancel);

            return (response == MessageBoxResult.OK);
        }
    }
}