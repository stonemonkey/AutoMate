using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using Ui.Wp8.Components.MainPage;

namespace Ui.Wp8
{
    public class Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer _container;

        protected override void Configure()
        {
            _container = new PhoneContainer();

            _container.RegisterPhoneServices(RootFrame);
            _container.PerRequest<MainPageViewModel>();
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