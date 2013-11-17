using System.Windows;
using Caliburn.Micro;

namespace Ui.Wp8.Components.RecordingPage
{
    public class RecordingPageViewModel : PageViewModelBase
    {
        public AccelerationViewModel AccelerationViewModel { get; private set; }
        public GpsDataViewModel GpsDataViewModel { get; private set; }

        public RecordingPageViewModel(INavigationService navigationService, 
            AccelerationViewModel accelerationViewModel, GpsDataViewModel gpsDataViewModel)
                : base(navigationService)
        {
            AccelerationViewModel = accelerationViewModel;
            GpsDataViewModel = gpsDataViewModel;
        }

        public void Stop()
        {
            if (IsActiveUserOkToStopRecording())
            {
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