using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace Tasker.PCL.ViewModel
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(object data = null) where TViewModel : ViewModelBase;
        void GoBack(object data = null);
        string GetAppVersion();
        void RemoveBackEntry();
        void ClearBackStack();
    }

    public abstract class NavigationService<TViewId> : INavigationService
    {
        readonly Dictionary<Type, TViewId> _map = new Dictionary<Type, TViewId>();

        public void Register(Type type, TViewId id)
        {
            _map.Add(type, id);
        }

        public void NavigateTo<TViewModel>(object data = null) where TViewModel : ViewModelBase
        {
            // Get the View that is mapped to the ViewModel
            TViewId id;

            if (!_map.TryGetValue(typeof(TViewModel), out id))
                throw new ArgumentException("No View Mapped To " + typeof(TViewModel).FullName);

            // Use the platform specific implementation to perform the View navigation
            NavigateTo(id, data);
        }

        public abstract void RemoveBackEntry();
        public abstract void ClearBackStack();

        protected abstract void NavigateTo(TViewId id, object data);
        public abstract void GoBack(object data);
        public abstract string GetAppVersion();
    }
}
