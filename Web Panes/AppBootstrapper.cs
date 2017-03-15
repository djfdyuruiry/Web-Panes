using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using WebPanes.Interface;

namespace WebPanes
{
    public class AppBootstrapper : BootstrapperBase
    {
        private readonly AppContainer _appContainer;

        public AppBootstrapper()
        {
            _appContainer = new AppContainer();

            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e) => DisplayRootViewFor<IShellViewModel>();

        protected override void Configure() => _appContainer.Configure();
        
        protected override object GetInstance(Type service, string key) => _appContainer.GetInstance(service);

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            IServiceProvider provider = _appContainer;
            var collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(collectionType);

            return services ?? Enumerable.Empty<object>();
        }

        protected override void BuildUp(object instance)
        {
            var registration = _appContainer.GetRegistration(instance.GetType(), true);
            registration.Registration.InitializeInstance(instance);
        }
    }
}
