using Caliburn.Micro;
using WebPanes.Interface;
using WebPanes.Model;
using WebPanes.Provider;

namespace WebPanes.ViewModels
{
    public class MainViewModel : Screen, IHandle<AutoLoginDetailsFormCompletedMessage>, IHandle<AutoLoginDetailsFormDismissedMessage>
    {
        private readonly UserCredentialsProvider _credentialsProvider;
        private readonly IWebPanesConfigurationProvider _webPanesConfigurationProvider;

        private bool _autoLoginIsVisible;

        public AutoLoginDetailsViewModel AutoLoginDetailsViewModel { get; }

        public WebBrowserViewModel MainWebBrowserViewModel { get; }
        public WebBrowserViewModel LeftMiniWebBrowserViewModel { get; }
        public WebBrowserViewModel RightMiniWebBrowserViewModel { get; }

        public bool AutoLoginIsVisible
        {
            get
            {
                return _autoLoginIsVisible;
            }
            private set
            {
                _autoLoginIsVisible = value;
                NotifyOfPropertyChange(nameof(AutoLoginIsVisible));
                NotifyOfPropertyChange(nameof(AutoLoginIsNotVisible));
            }
        }
        
        public bool AutoLoginIsNotVisible => !AutoLoginIsVisible;

        public MainViewModel(AutoLoginDetailsViewModel autoLoginDetailsViewModel, 
            WebBrowserViewModel mainWebBrowserViewModel,
            WebBrowserViewModel leftMiniWebBrowserViewModel,
            WebBrowserViewModel rightMiniWebBrowserViewModel,
            UserCredentialsProvider credentialsProvider,
            IWebPanesConfigurationProvider webPanesConfigurationProvider, 
            IEventAggregator eventAggregator)
        {
            AutoLoginDetailsViewModel = autoLoginDetailsViewModel;

            MainWebBrowserViewModel = mainWebBrowserViewModel;
            LeftMiniWebBrowserViewModel = leftMiniWebBrowserViewModel;
            RightMiniWebBrowserViewModel = rightMiniWebBrowserViewModel;

            _credentialsProvider = credentialsProvider;
            _webPanesConfigurationProvider = webPanesConfigurationProvider;

            eventAggregator.Subscribe(this);
        }

        protected override void OnActivate()
        {
            DisplayName = "Web Panes";

            var config = _webPanesConfigurationProvider.LoadConfiguration();
            
            if (!config.EnableAutoLogin)
            {
                LoadBrowsersWithoutAutoLogin();
            }
            else
            {
                AutoLoginIsVisible = true;
            }

            base.OnActivate();
        }

        private void LoadBrowsersWithoutAutoLogin()
        {
            AutoLoginIsVisible = false;
            _credentialsProvider.AutoLoginEnabled = false;
            LoadWebBrowsers();
        }

        private void LoadWebBrowsers()
        {
            var config = _webPanesConfigurationProvider.LoadConfiguration();

            MainWebBrowserViewModel.Url = config.MainWebBrowserUrl;
            LeftMiniWebBrowserViewModel.Url = config.LeftMinWebBrowserUrl;
            RightMiniWebBrowserViewModel.Url = config.RightMinWebBrowserUrl;
        }

        public void Handle(AutoLoginDetailsFormCompletedMessage message) 
        {
            _credentialsProvider.AutoLoginEnabled = true;
            _credentialsProvider.UserName = AutoLoginDetailsViewModel.UserName;
            _credentialsProvider.Password = AutoLoginDetailsViewModel.Password;

            AutoLoginIsVisible = false;

            LoadWebBrowsers();
        }

        public void Handle(AutoLoginDetailsFormDismissedMessage message) => LoadBrowsersWithoutAutoLogin();
    }
}
