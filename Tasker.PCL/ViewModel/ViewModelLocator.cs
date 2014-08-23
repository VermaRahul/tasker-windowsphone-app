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


            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TasksViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public HomeViewModel HomeViewModel
        {
            get { return ServiceLocator.Current.GetInstance<HomeViewModel>(); }
        }

        public AddNewItemViewModel AddNewItemViewModel
        {
            get { return ServiceLocator.Current.GetInstance<AddNewItemViewModel>(); }
        }

        public TasksViewModel TasksViewModel
        {
            get { return ServiceLocator.Current.GetInstance<TasksViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}