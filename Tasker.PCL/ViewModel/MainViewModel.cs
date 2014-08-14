using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Tasker.PCL.Model;

namespace Tasker.PCL.ViewModel
{
    public class MainViewModel : AppViewModel
    {
        public MainViewModel(AppContext context, INavigationService navigationService)
            : base(context, navigationService)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

        #region Properties

        #endregion

        #region Commands

        private RelayCommand _navigateToTasksCommand;

        /// <summary>
        /// Gets the NavigateToTasksCommand.
        /// </summary>
        public RelayCommand NavigateToTasksCommand
        {
            get
            {
                return _navigateToTasksCommand
                    ?? (_navigateToTasksCommand = new RelayCommand(ExecuteNavigateToTasksCommand));
            }
        }

        private void ExecuteNavigateToTasksCommand()
        {
            NavigationService.NavigateTo<TasksViewModel>();
        }

        #endregion


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}