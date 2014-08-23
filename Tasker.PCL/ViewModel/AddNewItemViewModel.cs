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
            if(!(content is ObjectType))
                return;

            var type = (ObjectType) content;

            if (type == ObjectType.Task)
            {
                PageTitle = "New Task";
            }
            else if (type == ObjectType.Category)
            {
                PageTitle = "New Category";
            }
            PageItemType = type;

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

        #endregion
    }
}
