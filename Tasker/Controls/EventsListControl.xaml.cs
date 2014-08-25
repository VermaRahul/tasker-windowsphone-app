using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Tasker.PCL.Model;
using Event = Tasker.PCL.Model.Event;

namespace Tasker.Controls
{
    public partial class EventsListControl : UserControl
    {
        public EventsListControl()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public List<CustomKeyGroup<Event>.Group<Event>> GroupedEventsList
        {
            get { return (List<CustomKeyGroup<Event>.Group<Event>>)GetValue(GroupedEventsListProperty); }
            set { SetValue(GroupedEventsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GroupedEventsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupedEventsListProperty =
            DependencyProperty.Register("GroupedEventsList", typeof(List<CustomKeyGroup<Event>.Group<Event>>), typeof(EventsListControl), new PropertyMetadata(null, OnGroupedEventsChangedCallback));

        private static void OnGroupedEventsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elc = d as EventsListControl;
            if (elc == null)
                return;

            var groupedEvents = e.NewValue as List<CustomKeyGroup<Event>.Group<Event>>;
            elc.EventsListSelector.ItemsSource = groupedEvents;
        }

        #endregion
    }
}
