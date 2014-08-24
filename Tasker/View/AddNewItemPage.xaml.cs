﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Model;
using Tasker.PCL.ViewModel;

namespace Tasker.View
{
    public partial class AddNewItemPage : AppPage
    {
        public AddNewItemPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ItemTitleTextBox.Focus();
        }

        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            var _vm = DataContext as AddNewItemViewModel;
            if(_vm == null)
                return;

            if (_vm.PageItemType == ObjectType.Category)
            {
                if (string.IsNullOrEmpty(ItemTitleTextBox.Text))
                    _vm.InvokeError("Enter something", "error");
                else
                    _vm.NavigationService.GoBack(new Category {Name = ItemTitleTextBox.Text});
            }
        }

        private void CancelButtonOnClick(object sender, EventArgs e)
        {
            var _vm = DataContext as AddNewItemViewModel;
            if (_vm == null)
                return;

            _vm.NavigationService.GoBack();
        }

        
    }
}