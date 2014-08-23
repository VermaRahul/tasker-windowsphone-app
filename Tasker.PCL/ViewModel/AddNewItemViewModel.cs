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
                var a = 1;
            }
            else if (type == ObjectType.Category)
            {
                var a = 2;
            }
        }
    }
}
