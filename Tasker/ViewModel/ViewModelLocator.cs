using GalaSoft.MvvmLight.Ioc;
using Tasker.PCL.Utils;
using Tasker.PCL.ViewModel;
using Tasker.Utils;

namespace Tasker.ViewModel
{
    public class ViewModelLocator : Tasker.PCL.ViewModel.ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ISettingsManager, SettingsManager>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
        }
    }
}
