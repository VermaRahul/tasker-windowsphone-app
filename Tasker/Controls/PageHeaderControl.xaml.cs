using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Tasker.Controls
{
    public partial class PageHeaderControl : UserControl
    {
        public PageHeaderControl()
        {
            InitializeComponent();
        }



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
            if(header == null)
                return;

            header.TitleTextBlock.Text = ((PivotItem)e.NewValue).Header as string;
        }
    }
}
