using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class MainViewModel : AppViewModel
    {
        private readonly ISettingsManager _settingsManager;

        public MainViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager)
            : base(context, navigationService)
        {
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>

        #region Properties

        /// <summary>
        /// The <see cref="FileContent" /> property's name.
        /// </summary>
        public const string FileContentPropertyName = "FileContent";

        private string _fileContent = null;

        /// <summary>
        /// Sets and gets the FileContent property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FileContent
        {
            get { return _fileContent; }
            set { Set(FileContentPropertyName, ref _fileContent, value); }
        }

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

        private async void ExecuteNavigateToTasksCommand()
        {
            await _settingsManager.WriteDataToFileAsync(AppUtils.LocalAppFileName, DateTime.Now.ToString());

            FileContent = await _settingsManager.ReadFileContentsAsync(AppUtils.LocalAppFileName);

            NavigationService.NavigateTo<TasksViewModel>();
        }

        private RelayCommand<string> _saveTextCommand;

        /// <summary>
        /// Gets the SaveTextCommand.
        /// </summary>
        public RelayCommand<string> SaveTextCommand
        {
            get
            {
                return _saveTextCommand
                    ?? (_saveTextCommand = new RelayCommand<string>(ExecuteSaveTextCommand));
            }
        }

        private async void ExecuteSaveTextCommand(string parameter)
        {
            await _settingsManager.WriteDataToFileAsync(AppUtils.LocalAppFileName, DateTime.Now + " " + parameter);

            FileContent = await _settingsManager.ReadFileContentsAsync(AppUtils.LocalAppFileName);
        }

        #endregion


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}