using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Command;
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
            elc.NoItemsMessage.Visibility = groupedEvents != null && groupedEvents.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            elc.EventsListSelector.ItemsSource = groupedEvents;
        }



        public List<Event> Events
        {
            get { return (List<Event>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(List<Event>), typeof(EventsListControl), new PropertyMetadata(null, OnEventsChangedCallback));

        private static void OnEventsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var elc = d as EventsListControl;
            if (elc == null)
                return;
            var events = e.NewValue as List<Event>;
            elc.NoItemsMessage.Visibility = events != null && events.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            elc.GroupedEventsList = CustomKeyGroup<Event>.GetEventGroups(events);
        }



        public RelayCommand<Event> RemoveEventCommand
        {
            get { return (RelayCommand<Event>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("RemoveEventCommand", typeof(RelayCommand<Event>), typeof(EventsListControl), new PropertyMetadata(null));

        

        #endregion

        private void RemoveEventOnClick(object sender, RoutedEventArgs e)
        {
            if(!(sender is MenuItem))
                return;

            var selectedEvent = (sender as MenuItem).DataContext as Event;

            if(RemoveEventCommand != null)
                RemoveEventCommand.Execute(selectedEvent);
        }
    }
}
