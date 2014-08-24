using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Enumerations;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.PCL.ViewModel
{
    public class AddNewItemViewModel : AppViewModel
    {
        private ISettingsManager _settingsManager;

        public AddNewItemViewModel(AppContext context, INavigationService navigationService, ISettingsManager settingsManager) : base(context, navigationService)
        {
            _settingsManager = settingsManager;
        }

        public override void SetData(object content)
        {
            if (!(content is PageNavigationData))
                return;

            var data = (PageNavigationData)content;

            if (data.Mode == ObjectType.Event)
            {
                PageTitle = "New Event";
            }
            else if (data.Mode == ObjectType.Category)
            {
                PageTitle = "New Category";
            }
            PageItemType = data.Mode;

            if (data.Categories == null)
                return;

            Categories = data.Categories;
        }

        #region Properties

        /// <summary>
        /// The <see cref="PageTitle" /> property's name.
        /// </summary>
        public const string PageTitlePropertyName = "PageTitle";

        private string _pageTitle = null;

        /// <summary>
        /// Sets and gets the PageTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PageTitle
        {
            get { return _pageTitle; }
            set { Set(PageTitlePropertyName, ref _pageTitle, value); }
        }

        /// <summary>
        /// The <see cref="PageItemType" /> property's name.
        /// </summary>
        public const string PageItemTypePropertyName = "PageItemType";

        private ObjectType _pageItemType = ObjectType.Empty;

        /// <summary>
        /// Sets and gets the PageItemType property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObjectType PageItemType
        {
            get { return _pageItemType; }
            set { Set(PageItemTypePropertyName, ref _pageItemType, value); }
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
