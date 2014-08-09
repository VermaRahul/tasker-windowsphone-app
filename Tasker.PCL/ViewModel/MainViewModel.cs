using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;

namespace Tasker.PCL.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
           
        }

        #region Properties

        #endregion

        #region Commands

        private RelayCommand _tasksButtonCommand;

        /// <summary>
        /// Gets the TasksButtonCommand.
        /// </summary>
        public RelayCommand TasksButtonCommand
        {
            get
            {
                return _tasksButtonCommand
                       ?? (_tasksButtonCommand = new RelayCommand(ExecuteTasksButtonCommand));
            }
        }

        private void ExecuteTasksButtonCommand()
        {
            var a = 1;
        }

        #endregion


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}