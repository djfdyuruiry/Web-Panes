using Caliburn.Micro;

namespace WebPanes.ViewModels
{
    public class MainViewModel : Screen
    {
        public WebBrowserViewModel MainWebBrowserViewModel { get; }
        public WebBrowserViewModel LeftMiniWebBrowserViewModel { get; }
        public WebBrowserViewModel RightMiniWebBrowserViewModel { get; }

        public MainViewModel(WebBrowserViewModel mainWebBrowserViewModel,
            WebBrowserViewModel leftMiniWebBrowserViewModel,
            WebBrowserViewModel rightMiniWebBrowserViewModel)
        {
            MainWebBrowserViewModel = mainWebBrowserViewModel;
            LeftMiniWebBrowserViewModel = leftMiniWebBrowserViewModel;
            RightMiniWebBrowserViewModel = rightMiniWebBrowserViewModel;

            DisplayName = "Web Panes";
        }

        protected override void OnActivate()
        {
            MainWebBrowserViewModel.Url = "";
            LeftMiniWebBrowserViewModel.Url = "";
            RightMiniWebBrowserViewModel.Url = "";

            base.OnActivate();
        }
    }
}
