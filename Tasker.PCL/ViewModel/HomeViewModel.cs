using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Tasker.PCL.Enumerations;
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
            if (AppData == null)
                AppData = await _settingsManager.ReadJsonDataFileAsync<JsonData>("Json.dat");

            if(AppData == null)
                AppData = new JsonData();
        }

        private async void InitializeJsonData()
        {
            AppData = await _settingsManager.ReadJsonDataFileAsync<JsonData>("Json.dat");
        }

        public override void SetData(object content)
        {
            if(AppData == null)
                return;

            if (content is Category)
            {
                var item = content as Category;

                if (AppData.Categories.FirstOrDefault(t => t.Name.Equals(item.Name)) == null)
                {
                    var copy = AppData.DeepCopy();
                    copy.Categories.Add(content as Category);
                    _settingsManager.WriteJsonDataToFileAsync("Json.dat", copy);
                    AppData = copy;
                }
                else
                {
                    InvokeError("Item already exists", "error");
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
            switch (type)
            {
                case ObjectType.Task:
                    NavigationService.NavigateTo<AddNewItemViewModel>(type);
                    break;
                case ObjectType.Category:
                    NavigationService.NavigateTo<AddNewItemViewModel>(type);
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

        #endregion
    }
}
