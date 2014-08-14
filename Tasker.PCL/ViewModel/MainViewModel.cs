using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Tasker.PCL.Model;

namespace Tasker.PCL.ViewModel
{
    public class MainViewModel : AppViewModel
    {

        public MainViewModel(AppContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

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