using Caliburn.Micro;
using WebPanes.Model;

namespace WebPanes.ViewModels
{
    public class AutoLoginDetailsViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public AutoLoginDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public void Ok() => _eventAggregator.PublishOnBackgroundThread(new AutoLoginDetailsFormCompletedMessage());
        public void Cancel() => _eventAggregator.PublishOnBackgroundThread(new AutoLoginDetailsFormDismissedMessage());
    }
}
