using WebPanes.Model;

namespace WebPanes.Interface
{
    public interface IWebPanesConfigurationProvider
    {
        WebPanesConfiguration LoadConfiguration();
    }
}
