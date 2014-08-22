using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}
