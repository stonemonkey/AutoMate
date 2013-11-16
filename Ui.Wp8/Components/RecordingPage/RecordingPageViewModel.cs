using Caliburn.Micro;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8.Components.RecordingPage
{
    public class RecordingPageViewModel : PageViewModelBase
    {
        public AccelerationViewModel AccelerationViewModel { get; private set; }
        public GpsDataViewModel GpsDataViewModel { get; private set; }

        public RecordingPageViewModel(INavigationService navigationService, AccelerationViewModel accelerationViewModel, GpsDataViewModel gpsDataViewModel)
            : base(navigationService)
        {
            AccelerationViewModel = accelerationViewModel;
            GpsDataViewModel = gpsDataViewModel;
        }

        public Acceleration acceleration;

        public void Stop()
        {
        }
    }
}