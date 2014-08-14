using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Utils;

namespace Tasker.PCL.Model
{
    public class AppContext
    {
        public AppSettings Settings { get; set; }

        private ISettingsManager _manager ;

        public AppContext(ISettingsManager settingsManager)
        {
            _manager = settingsManager;

            Settings = new AppSettings { SettingsManager = settingsManager };
            if (!Settings.LoadSettings())
            {
                settingsManager.RemoveSettings();
                Settings = new AppSettings();
            }
        }
    }
}
