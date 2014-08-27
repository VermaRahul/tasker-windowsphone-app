using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        public AppContext Context { get; set; }
        public INavigationService NavigationService { get; set; }
        public ISettingsManager SettingsManager { get; set; }

        public AppViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager)
        {
            Context = context;
            NavigationService = navigationService;
            SettingsManager = settingsManager;
        }

        public virtual void CleanUp()
        {
        }

        public virtual void SetData(object content)
        {
        }

        #region Properties

        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading = false;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { Set(IsLoadingPropertyName, ref _isLoading, value); }
        }

        #endregion

        #region Events

        public event EventHandler<AppError> OnError;

        public void InvokeError(AppError error)
        {
            if(OnError != null)
                OnError.Invoke(this, error);
        }

        public void InvokeError(string message, string title)
        {
            InvokeError(new AppError(message, title));
        }

        #endregion

        #region Commands

        private RelayCommand<Event> _removeEventCommand;

        /// <summary>
        /// Gets the RemoveEventCommand.
        /// </summary>
        public RelayCommand<Event> RemoveEventCommand
        {
            get
            {
                return _removeEventCommand
                    ?? (_removeEventCommand = new RelayCommand<Event>(ExecuteRemoveEventCommand));
            }
        }

        private async void ExecuteRemoveEventCommand(Event param)
        {
            var hvm = ServiceLocator.Current.GetInstance<HomeViewModel>();
            var evm = ServiceLocator.Current.GetInstance<EventsViewModel>();
            if (hvm == null)
                return;
            JsonData copy = null;

            if (hvm.AppData != null && hvm.AppData.Events != null && hvm.AppData.Events.Contains(param))
            {
                IsLoading = true;
                hvm.AppData.Events.Remove(param);
                copy = hvm.AppData.DeepCopy();
                copy.Events.Sort((x, y) => x.Date.CompareTo(y.Date));
                await SettingsManager.WriteJsonDataToFileAsync("Json.dat", copy);
                hvm.AppData = copy;
                IsLoading = false;
            }
            if (evm != null && !string.IsNullOrEmpty(evm.CategoryName) && copy != null && copy.Events != null)
            {
                var list = copy.Events != null
                ? copy.Events.FindAll(
                    t =>
                        t.Category != null && !string.IsNullOrEmpty(t.Category.Name) &&
                        t.Category.Name.Equals(evm.CategoryName))
                : new List<Event>();

                evm.Events = list;
            }
        }

        #endregion
    }

    public class AppError : EventArgs
    {
        public readonly string Message;
        public readonly string Title;

        public AppError(string message, string title)
        {
            Message = message;
            Title = title;
        }
    }
}
