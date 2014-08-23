using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Tasker.PCL.Enumerations;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Tasker.Controls
{
    public partial class PageHeaderControl : UserControl
    {
        public PageHeaderControl()
        {
            InitializeComponent();
        }

        #region Properties

        public PivotItem SelectedItem
        {
            get { return (PivotItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(PivotItem), typeof(PageHeaderControl), new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var header = d as PageHeaderControl;
            if(header == null || !(e.NewValue is PivotItem))
                return;

            header.TitleTextBlock.Text = ((PivotItem)e.NewValue).Header as string;

            if (header.TitleTextBlock.Text != null && header.TitleTextBlock.Text.ToLower() == "tasks")
            {
                SwitchButtonsVisibility(header, Visibility.Visible, Visibility.Collapsed);
            }
            else if (header.TitleTextBlock.Text != null && header.TitleTextBlock.Text.ToLower() == "categories")
            {
                SwitchButtonsVisibility(header, Visibility.Collapsed, Visibility.Visible);
            }
            else
            {
                SwitchButtonsVisibility(header, Visibility.Collapsed, Visibility.Collapsed);
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PageHeaderControl), new PropertyMetadata(null, OnHeaderTitleChangedCallback));

        private static void OnHeaderTitleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var header = d as PageHeaderControl;
            if (header == null)
                return;

            SwitchButtonsVisibility(header, Visibility.Collapsed, Visibility.Collapsed);
            header.TitleTextBlock.Text = e.NewValue as string;
        }

        #endregion

        #region Commands

        public RelayCommand<ObjectType> AddButtonPressedCommand
        {
            get { return (RelayCommand<ObjectType>)GetValue(AddButtonPressedCommandProperty); }
            set { SetValue(AddButtonPressedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddButtonPressedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddButtonPressedCommandProperty =
            DependencyProperty.Register("AddButtonPressedCommand", typeof(RelayCommand<ObjectType>), typeof(PageHeaderControl), new PropertyMetadata(null));

        #endregion

        #region Event Handlers

        private void AddTaskButton_OnTap(object sender, GestureEventArgs e)
        {
            if(AddButtonPressedCommand != null)
                AddButtonPressedCommand.Execute(ObjectType.Task);
        }

        private void AddCategoryButton_OnTap(object sender, GestureEventArgs e)
        {
            if (AddButtonPressedCommand != null)
                AddButtonPressedCommand.Execute(ObjectType.Category);
        }

        #endregion

        #region Helper Methods

        private static void SwitchButtonsVisibility(PageHeaderControl header, Visibility addTaskButtonVisibility, Visibility addCategoryButtonVisibility)
        {
            header.AddTaskButton.Visibility = addTaskButtonVisibility;
            header.AddCategoryButton.Visibility = addCategoryButtonVisibility;
        }

        #endregion
    }
}
