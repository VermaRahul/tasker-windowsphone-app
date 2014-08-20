using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Logging;
using Tasker.Extensions;
using Tasker.PCL.ViewModel;

namespace Tasker.View
{
    public class AppPage : PhoneApplicationPage
    {

        public AppPage()
        {
            InitAnimation();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void InitAnimation()
        {
            var navigateInTransition = new NavigationInTransition
            {
                Backward = new TurnstileTransition{Mode = TurnstileTransitionMode.BackwardIn},
                Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardIn}
            };
            var navigateOutTransition = new NavigationOutTransition
            {
                Backward = new TurnstileTransition { Mode = TurnstileTransitionMode.BackwardOut },
                Forward = new TurnstileTransition { Mode = TurnstileTransitionMode.ForwardOut },
            };
                
            TransitionService.SetNavigationInTransition(this, navigateInTransition);
            TransitionService.SetNavigationOutTransition(this, navigateOutTransition);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var data = ((PhoneApplicationFrame)Application.Current.RootVisual).GetNavigationData();
            var vm = DataContext as AppViewModel;
            if (vm != null)
            {
                vm.SetData(data);
            }
        }

        protected override void OnRemovedFromJournal(JournalEntryRemovedEventArgs e)
        {
            var vm = DataContext as AppViewModel;
            if (vm != null)
            {
                vm.CleanUp();
            }
            base.OnRemovedFromJournal(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AppViewModel;

            if (vm == null)
                return;

            vm.OnError += OnError;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AppViewModel;

            if (vm == null)
                return;

            vm.OnError -= OnError;
        }

        private void OnError(object sender, AppError error)
        {
            RunOnUiThread(() =>
            {
                if (error == null || string.IsNullOrEmpty(error.Message)) return;

                var result = MessageBox.Show(error.Title, error.Message, MessageBoxButton.OK);

            });
        }

        public void RunOnUiThread(Action action)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(action);
            }
        }

    }
}
