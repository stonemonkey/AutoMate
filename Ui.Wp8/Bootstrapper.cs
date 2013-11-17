using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Ui.Wp8.Components.MainPage;
using Ui.Wp8.Components.RecordingPage;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8
{
    public class Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer();

            _container.RegisterPhoneServices(RootFrame);

            _container.Singleton<UserContextViewModel>();
            
            _container.PerRequest<AgresivityCalculator>();
            _container.PerRequest<StatisticsRepository>();

            _container.PerRequest<MainPageViewModel>();
            _container.PerRequest<RecordingPageViewModel>();
            _container.PerRequest<AccelerationViewModel>();
            _container.PerRequest<GpsDataViewModel>();
            _container.PerRequest<StatisticsViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}