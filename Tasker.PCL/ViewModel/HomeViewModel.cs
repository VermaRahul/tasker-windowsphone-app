using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class HomeViewModel : AppViewModel
    {
        private ISettingsManager _settingsManager;

        public HomeViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager) : base(context, navigationService)
        {
            _settingsManager = settingsManager;
        }

        #region Commands

        private RelayCommand<ObjectType> _addNewCommand;

        /// <summary>
        /// Gets the AddNewButtonPressedCommand.
        /// </summary>
        public RelayCommand<ObjectType> AddNewButtonPressedCommand
        {
            get
            {
                return _addNewCommand
                    ?? (_addNewCommand = new RelayCommand<ObjectType>(ExecuteAddNewCommand));
            }
        }

        private void ExecuteAddNewCommand(ObjectType type)
        {
            int a;
            switch (type)
            {
                case ObjectType.Task:
                    a = 1;
                    break;
                case ObjectType.Category:
                    a = 2;
                    break;
            }
        }

        #endregion
    }
}
