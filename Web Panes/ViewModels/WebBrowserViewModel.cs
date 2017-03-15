using System;
using Caliburn.Micro;
using CefSharp;
using WebPanes.Model;
using WebPanes.Util;

namespace WebPanes.ViewModels
{
    public class WebBrowserViewModel : Screen, IHandle<WebBrowserLoadedMessage>
    {
        private readonly WebBrowserEventAggregator _webBrowserEventAggreator;
        private readonly AutoLoginScriptLoader _autoLoginScriptLoader;

        public IWebBrowser WebBrowser { get; set; }
        public string Url { get; set; }
        public Guid BrowserGuid { get; }
        
        public WebBrowserViewModel(WebBrowserEventAggregator webBrowserEventAggreator, AutoLoginScriptLoader autoLoginScriptLoader,
            IEventAggregator eventAggreator)
        {
            _webBrowserEventAggreator = webBrowserEventAggreator;
            _autoLoginScriptLoader = autoLoginScriptLoader;

            BrowserGuid = Guid.NewGuid();

            eventAggreator.Subscribe(this);
        }

        protected override void OnViewLoaded(object view)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                _webBrowserEventAggreator.WireUpBrowserLoadedEvent(WebBrowser, BrowserGuid);
                WebBrowser.Load(Url);
            }

            base.OnViewLoaded(view);
        }

        public void Handle(WebBrowserLoadedMessage message)
        {
            if (message.BrowserGuid != BrowserGuid)
            {
                return;
            }

            var browserFrame = WebBrowser.GetFocusedFrame();
            var autoLoginScript = _autoLoginScriptLoader.LoadScript("", "");

            browserFrame.ExecuteJavaScriptAsync(autoLoginScript);        
        }
    }
}
