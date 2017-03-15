using Caliburn.Micro;
using WebPanes.Interface;
using WebPanes.ViewModels;

namespace WebPanes
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShellViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public ShellViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void ActivateItem(IScreen item)
        {
            DisplayName = item.DisplayName;

            base.ActivateItem(item);
        }

        protected override void OnInitialize()
        {
            ActiveItem = _mainViewModel;
            
            base.OnInitialize();
        }
    }
}
