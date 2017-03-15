using Caliburn.Micro;
using CefSharp;
using System;
using WebPanes.Model;
using WebPanes.Util.Event;

namespace WebPanes.Util
{
    public class WebBrowserEventAggregator
    {
        private readonly IEventAggregator _eventAggreator;

        public WebBrowserEventAggregator(IEventAggregator eventAggreator)
        {
            _eventAggreator = eventAggreator;
        }

        public void WireUpBrowserLoadedEvent(IWebBrowser webBrowser, Guid browserId)
        {
            var eventProxyAsync = new EventProxyAsync<FrameLoadEndEventArgs>(
                (e) => webBrowser.FrameLoadEnd += e, 
                () => PublishBrowserLoaded(webBrowser, browserId), 
                (e) => webBrowser.FrameLoadEnd -= e);

            eventProxyAsync.Initialise();
        }

        private void PublishBrowserLoaded(IWebBrowser webBrowser, Guid browserId)
        {
            _eventAggreator.PublishOnBackgroundThread(new WebBrowserLoadedMessage
            {
                BrowserGuid = browserId,
                Browser = webBrowser
            });
        }
    }
}
