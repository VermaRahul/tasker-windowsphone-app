using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Tasker.PCL.Model;

namespace Tasker.PCL.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        public AppContext Context { get; set; }
        public INavigationService NavigationService { get; set; }

        public AppViewModel(AppContext context, INavigationService navigationService)
        {
            Context = context;
            NavigationService = navigationService;
        }

        public virtual void CleanUp()
        {
        }

        public virtual void SetData(object context)
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
