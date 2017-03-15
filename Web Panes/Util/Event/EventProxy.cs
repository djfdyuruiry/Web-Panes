using System;

namespace WebPanes.Util.Event
{
    public class EventProxy<T> where T : EventArgs
    {
        private readonly Action<EventHandler<T>> _setUpAction;
        private readonly Action _eventAction;
        private readonly Action<EventHandler<T>> _tearDownAction;
        private readonly bool _onlyConsumeFirstEvent;
        private readonly EventHandler<T> _eventHandler;

        public EventProxy (Action<EventHandler<T>> setUpAction, Action eventAction)
        {
            _setUpAction = setUpAction;
            _eventAction = eventAction;
            _onlyConsumeFirstEvent = false;
            _eventHandler = ProxyHandler;
        }

        public EventProxy (Action<EventHandler<T>> setUpAction, Action eventAction, Action<EventHandler<T>> tearDownAction)
            : this(setUpAction, eventAction)
        {
            _onlyConsumeFirstEvent = true;
            _tearDownAction = tearDownAction;
        }

        public void Initialise()
        {
            _setUpAction?.Invoke(_eventHandler);
        }

        public void ProxyHandler(object sender, T eventArgs)
        {
            _eventAction?.Invoke();

            if (_onlyConsumeFirstEvent)
            {
                _tearDownAction?.Invoke(_eventHandler);
            }
        }
    }
}
