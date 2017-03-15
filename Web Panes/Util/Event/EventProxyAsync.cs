using System;
using System.Threading.Tasks;

namespace WebPanes.Util.Event
{
    public class EventProxyAsync<T> : EventProxy<T> where T : EventArgs
    {
        public EventProxyAsync(Action<EventHandler<T>> setUpAction, Action eventAction) : base(setUpAction, eventAction)
        {
        }

        public EventProxyAsync(Action<EventHandler<T>> setUpAction, Action eventAction, Action<EventHandler<T>> tearDownAction)
            : base(setUpAction, eventAction, tearDownAction)
        {
        }

        public new void ProxyHandler(object sender, T eventArgs)
        {
            Task.Run(() => base.ProxyHandler(sender, eventArgs));
        }
    }
}
