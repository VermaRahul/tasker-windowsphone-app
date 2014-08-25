using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class EventsViewModel : AppViewModel
    {
        private ISettingsManager _settingsManager;

        public EventsViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager)
            : base(context, navigationService)
        {
            _settingsManager = settingsManager;
        }

        public override void SetData(object content)
        {
            if (!(content is PageNavigationData))
                return;

            var navData = content as PageNavigationData;

            var events = navData.Events ?? new List<Event>();

            Events = events;
            CategoryName = navData.Category != null ? navData.Category.Name : string.Empty;
        }

        #region Properties
        /// <summary>
        /// The <see cref="Events" /> property's name.
        /// </summary>
        public const string EventsPropertyName = "Events";

        private List<Event> _events = null;

        /// <summary>
        /// Sets and gets the Events property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<Event> Events
        {
            get { return _events; }
            set { Set(EventsPropertyName, ref _events, value); }
        }

        /// <summary>
        /// The <see cref="CategoryName" /> property's name.
        /// </summary>
        public const string CategoryNamePropertyName = "CategoryName";

        private string _categoryName = null;

        /// <summary>
        /// Sets and gets the CategoryName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { Set(CategoryNamePropertyName, ref _categoryName, value); }
        }

        #endregion
    }
}
