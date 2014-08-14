using GalaSoft.MvvmLight.Ioc;
using Tasker.PCL.Utils;
using Tasker.Utils;

namespace Tasker.ViewModel
{
    public class ViewModelLocator : Tasker.PCL.ViewModel.ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ISettingsManager, SettingsManager>();
        }
    }
}
