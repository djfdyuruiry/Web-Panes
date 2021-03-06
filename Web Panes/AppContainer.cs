﻿using Caliburn.Micro;
using SimpleInjector;
using WebPanes.Interface;
using WebPanes.Provider;
using WebPanes.Util;

namespace WebPanes
{
    public class AppContainer : Container
    {
        public void Configure()
        {
            Register<IWindowManager, WindowManager>();
            Register<IShellViewModel, ShellViewModel>();

            RegisterSingleton<IEventAggregator, EventAggregator>();
            RegisterSingleton<WebBrowserEventAggregator>();

            RegisterSingleton<IWebPanesConfigurationProvider, YamlWebPanesConfigurationProvider>();
            RegisterSingleton<UserCredentialsProvider>();

            Verify();
        }
    }
}
