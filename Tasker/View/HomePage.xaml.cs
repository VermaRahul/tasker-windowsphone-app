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
        private ApplicationBarIconButton _addEventButton;
        private ApplicationBarIconButton _addCategoryButton;
        public HomePage()
        {
            InitializeComponent();
            CreateApplicationBar();
        }

        private void CreateApplicationBar()
        {
            ApplicationBar = new ApplicationBar
            {
                BackgroundColor = (Color) (Application.Current.Resources["PageHeaderBackgroundColor"]),
                ForegroundColor = Colors.White
            };

            _addEventButton = new ApplicationBarIconButton
            {
                IconUri = new Uri("/Assets/AppBar/ApplicationBar.Add.png", UriKind.Relative),
                Text = "Event"
            };
            _addEventButton.Click += AddEventButtonClicked;
            ApplicationBar.Buttons.Add(_addEventButton);
            _addCategoryButton = new ApplicationBarIconButton
            {
                IconUri = new Uri("/Assets/AppBar/ApplicationBar.Add.png", UriKind.Relative),
                Text = "Category"
            };
            _addCategoryButton.Click += AddCategoryButtonClicked;

        }

        private void AddCategoryButtonClicked(object sender, EventArgs e)
        {
            var _vm = DataContext as HomeViewModel;
            if (_vm == null)
                return;

            _vm.AddNewButtonPressedCommand.Execute(ObjectType.Category);
        }

        private void AddEventButtonClicked(object sender, EventArgs e)
        {
            var _vm = DataContext as HomeViewModel;
            if(_vm == null)
                return;

            _vm.AddNewButtonPressedCommand.Execute(ObjectType.Event);
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

        private void OnPivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.Buttons.Clear();
            var pivotItem = ContentPivot.SelectedItem as PivotItem;
            if (pivotItem == null)
                return;
            if(pivotItem.Header.Equals("Events"))
            {
                ApplicationBar.Buttons.Add(_addEventButton);
            }
            else if (pivotItem.Header.Equals("Categories"))
            {
                ApplicationBar.Buttons.Add(_addCategoryButton);
                
            }
        }
    }
}