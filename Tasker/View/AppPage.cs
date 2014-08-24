using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Logging;
using Microsoft.Phone.Shell;
using Tasker.Extensions;
using Tasker.PCL.ViewModel;

namespace Tasker.View
{
    public class AppPage : PhoneApplicationPage
    {

        public AppPage()
        {
            InitAnimation();
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
                vm.OnError += OnError;
                vm.SetData(data);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            var vm = DataContext as AppViewModel;
            if (vm != null)
            {
                vm.OnError -= OnError;
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

        private void OnError(object sender, AppError error)
        {
            RunOnUiThread(() =>
            {
                if (error == null || string.IsNullOrEmpty(error.Message)) return;

                var result = MessageBox.Show(error.Message, error.Title, MessageBoxButton.OK);

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
