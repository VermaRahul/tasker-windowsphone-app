using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Extensions;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class HomeViewModel : AppViewModel
    {
        private ISettingsManager _settingsManager;

        public HomeViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager) : base(context, navigationService)
        {
            _settingsManager = settingsManager;
            InitData();
        }

        private async void InitData()
        {
            IsLoading = true;
            if (AppData == null)
                InitializeJsonData();

            if(AppData == null)
                AppData = new JsonData();
            IsLoading = false;
        }

        private async void InitializeJsonData()
        {
            var data = await _settingsManager.ReadJsonDataFileAsync<JsonData>("Json.dat");
            if(data != null && data.Events != null && data.Events.Any())
                data.Events.Sort((x,y) => x.Date.CompareTo(y.Date));
            if (data != null && data.Categories != null && data.Categories.Any())
                data.Categories.Sort((x, y) => x.Name.CompareTo(y.Name));
            AppData = data;
        }

        public async override void SetData(object content)
        {
            if(AppData == null)
                return;

            if (content is Category)
            {
                var item = content as Category;

                if (AppData.Categories.FirstOrDefault(t => t.Name.Equals(item.Name)) == null)
                {
                    IsLoading = true;
                    var copy = AppData.DeepCopy();
                    copy.Categories.Add(item);
                    copy.Categories.Sort((x,y) => x.Name.CompareTo(y.Name));
                    await _settingsManager.WriteJsonDataToFileAsync("Json.dat", copy);
                    AppData = copy;
                    IsLoading = false;
                }
                else
                {
                    InvokeError("A category with the same name already exists", "ERROR");
                }
            }
            else if (content is Event)
            {
                var item = content as Event;

                if (AppData.Events.FirstOrDefault(t => t.Title.Equals(item.Title)) == null)
                {
                    IsLoading = true;
                    var copy = AppData.DeepCopy();
                    copy.Events.Add(item);
                    copy.Events.Sort((x, y) => x.Date.CompareTo(y.Date));
                    await _settingsManager.WriteJsonDataToFileAsync("Json.dat", copy);
                    AppData = copy;
                    IsLoading = false;
                }
                else
                {
                    InvokeError("An event with the same name already exists", "ERROR");
                }
            }
        }

        #region Commands

        private RelayCommand<ObjectType> _addNewCommand;

        /// <summary>
        /// Gets the AddNewButtonPressedCommand.
        /// </summary>
        public RelayCommand<ObjectType> AddNewButtonPressedCommand
        {
            get
            {
                return _addNewCommand
                    ?? (_addNewCommand = new RelayCommand<ObjectType>(ExecuteAddNewCommand));
            }
        }

        private void ExecuteAddNewCommand(ObjectType type)
        {
            int a;
            var navData = new PageNavigationData
            {
                Mode = type,
                Categories = AppData != null ? AppData.Categories : new List<Category>()
            };
            switch (type)
            {
                case ObjectType.Event:
                    NavigationService.NavigateTo<AddNewItemViewModel>(navData);
                    break;
                case ObjectType.Category:
                    NavigationService.NavigateTo<AddNewItemViewModel>(navData);
                    break;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The <see cref="AppData" /> property's name.
        /// </summary>
        public const string AppDataPropertyName = "AppData";

        private JsonData _appData = null;

        /// <summary>
        /// Sets and gets the AppData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public JsonData AppData
        {
            get
            {
                return _appData;
            }
            set
            {
                Set(AppDataPropertyName, ref _appData, value);
                if (value == null)
                {
                    _appData = new JsonData();
                }
                Categories = _appData.Categories;
                Events = _appData.Events;
            }
        }

        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private List<Category> _categories = null;

        /// <summary>
        /// Sets and gets the Categories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<Category> Categories
        {
            get { return _categories; }
            set { Set(CategoriesPropertyName, ref _categories, value); }
        }

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
            set
            {
                Set(EventsPropertyName, ref _events, value);
                GroupEvents();
            }
        }

        /// <summary>
        /// The <see cref="GroupedEvents" /> property's name.
        /// </summary>
        public const string GroupedEventsPropertyName = "GroupedEvents";

        private List<CustomKeyGroup<Event>.Group<Event>> _groupedEventsList = null;

        /// <summary>
        /// Sets and gets the GroupedEvents property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<CustomKeyGroup<Event>.Group<Event>> GroupedEvents
        {
            get { return _groupedEventsList; }
            set { Set(GroupedEventsPropertyName, ref _groupedEventsList, value); }
        }

        #endregion

        #region Helper Methods

        private void GroupEvents()
        {
            GroupedEvents = CustomKeyGroup<Event>.GetEventGroups(Events);
        }

        #endregion
    }
}
