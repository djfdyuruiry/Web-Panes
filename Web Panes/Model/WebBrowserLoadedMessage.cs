using CefSharp;
using System;

namespace WebPanes.Model
{
    public class WebBrowserLoadedMessage
    {
        public Guid BrowserGuid { get; set; }
        public IWebBrowser Browser { get; set; }
    }
}
