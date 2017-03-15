using System;
using Caliburn.Micro;
using CefSharp;
using WebPanes.Model;
using WebPanes.Util;
using WebPanes.Provider;

namespace WebPanes.ViewModels
{
    public class WebBrowserViewModel : Screen, IHandle<WebBrowserLoadedMessage>
    {
        private readonly WebBrowserEventAggregator _webBrowserEventAggreator;
        private readonly UserCredentialsProvider _userCredentialsProvider;
        private readonly AutoLoginScriptLoader _autoLoginScriptLoader;

        private string _url;

        public IWebBrowser WebBrowser { get; set; }

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;

                WireUpAutoLoginIfEnabled();
                NotifyOfPropertyChange(nameof(Url));
            }
        }

        public Guid BrowserGuid { get; }

        public WebBrowserViewModel(WebBrowserEventAggregator webBrowserEventAggreator, UserCredentialsProvider userCredentialsProvider, 
            AutoLoginScriptLoader autoLoginScriptLoader, IEventAggregator eventAggreator)
        {
            _webBrowserEventAggreator = webBrowserEventAggreator;

            _userCredentialsProvider = userCredentialsProvider;
            _autoLoginScriptLoader = autoLoginScriptLoader;

            BrowserGuid = Guid.NewGuid();

            eventAggreator.Subscribe(this);
        }

        private void WireUpAutoLoginIfEnabled()
        {
            if (_userCredentialsProvider.AutoLoginEnabled)
            {
                _webBrowserEventAggreator.WireUpBrowserLoadedEvent(WebBrowser, BrowserGuid);
            }
        }

        public void Handle(WebBrowserLoadedMessage message)
        {
            if (message.BrowserGuid != BrowserGuid)
            {
                return;
            }

            var browserFrame = WebBrowser.GetFocusedFrame();
            var autoLoginScript = _autoLoginScriptLoader.LoadScript(_userCredentialsProvider.UserName, _userCredentialsProvider.Password);

            browserFrame.ExecuteJavaScriptAsync(autoLoginScript);        
        }
    }
}
