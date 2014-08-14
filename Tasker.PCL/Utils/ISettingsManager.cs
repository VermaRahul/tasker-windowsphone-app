using System;
using System.Collections.Generic;
using System.Linq;
using Tasker.PCL.Model;

namespace Tasker.PCL.Utils
{
    public interface ISettingsManager
    {
        AppSettings RetrieveSettings();
        bool RemoveSettings();
        bool SaveSetting(string value, string name, bool isProtected);
        bool SaveCredentials(string username, string token);
    }
}
