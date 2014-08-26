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
        }

        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            var _vm = DataContext as AddNewItemViewModel;
            if(_vm == null)
                return;

            if (_vm.PageItemType == ObjectType.Category)
            {
                if (string.IsNullOrEmpty(ItemTitleTextBox.Text))
                    _vm.InvokeError("Please enter a title before saving", "ERROR");
                else
                    _vm.NavigationService.GoBack(new Category {Name = ItemTitleTextBox.Text});
            }
            else if (_vm.PageItemType == ObjectType.Event)
            {
                if (string.IsNullOrEmpty(ItemTitleTextBox.Text))
                    _vm.InvokeError("Please enter a title before saving", "Error");
                else
                {
                    var categoryItem = (Category) CategoryListPicker.SelectedItem;
                    var priorityItem = PriorityListPicker.SelectedItem.ToString();
                    _vm.NavigationService.GoBack(new Event
                    {
                        Title = ItemTitleTextBox.Text,
                        Date = DatePickerControl.Value ?? DateTime.Today,
                        Description = DescriptionTextBox.Text,
                        Category = categoryItem != null && categoryItem.Hide != true ? categoryItem : null,
                        Priority = priorityItem.ToLower().Equals("low") ? Priority.Normal : Priority.High
                    });
                }
            }
        }

        private void CancelButtonOnClick(object sender, EventArgs e)
        {
            var _vm = DataContext as AddNewItemViewModel;
            if (_vm == null)
                return;

            _vm.NavigationService.GoBack();
        }


        private void DatePickerValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {
//            var value = DatePickerControl.Value;
//            if(value == null)
//                return;
//            if (DateTime.Today.CompareTo(value) > 0)
//                DatePickerControl.Value = DateTime.Today;
        }
    }
}