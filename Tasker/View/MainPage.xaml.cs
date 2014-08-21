using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Tasker.PCL.ViewModel;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Tasker.View
{
    public partial class MainPage : AppPage
    {
        private MainViewModel _vm;

        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _vm = DataContext as MainViewModel;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if(_vm == null)
                return;

            _vm.PropertyChanged -= OnPropertyChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null)
                return;

            _vm.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_vm == null)
                return;

            if (e.PropertyName == MainViewModel.FileContentPropertyName)
            {
                
            }
        }

        private void SaveButton_OnTap(object sender, GestureEventArgs e)
        {
            if(_vm == null)
                return;

            _vm.SaveTextCommand.Execute(InputBox.Text);
        }
    }
}