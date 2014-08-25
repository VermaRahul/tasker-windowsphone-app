using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Tasker.PCL.Model;

namespace Tasker.PCL.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<AppContext>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<AddNewItemViewModel>();
            SimpleIoc.Default.Register<EventsViewModel>();

        }

        public EventsViewModel EventsViewModel
        {
            get { return ServiceLocator.Current.GetInstance<EventsViewModel>(); }
        }

        public HomeViewModel HomeViewModel
        {
            get { return ServiceLocator.Current.GetInstance<HomeViewModel>(); }
        }

        public AddNewItemViewModel AddNewItemViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AddNewItemViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}