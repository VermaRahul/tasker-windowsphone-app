using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Model;
using Tasker.PCL.ViewModel;

namespace Tasker.View
{
    public partial class HomePage : AppPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void CategoriesListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _vm = DataContext as HomeViewModel;
            if(_vm == null || CategoriesLongListSelector.SelectedItem == null)
                return;

            var item = (Category) CategoriesLongListSelector.SelectedItem;
            CategoriesLongListSelector.SelectedItem = null;

            var list = _vm.Events != null
                ? _vm.Events.FindAll(
                    t =>
                        t.Category != null && !string.IsNullOrEmpty(t.Category.Name) &&
                        t.Category.Name.Equals(item.Name))
                : new List<Event>();

            var navData = new PageNavigationData
            {
                Events = list,
                Category = item,
                Mode = ObjectType.Event
            };

            _vm.NavigationService.NavigateTo<EventsViewModel>(navData);

        }
    }
}