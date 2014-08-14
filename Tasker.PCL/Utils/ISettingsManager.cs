using System;
using System.Collections.Generic;
using System.Linq;
using Tasker.PCL.Model;

namespace Tasker.PCL.Utils
{
    public interface ISettingsManager
    {
        bool RemoveSettings();
        bool SaveSetting(string key, string value, bool isProtected = false);
        string RetrieveSetting(string key, bool isProtected = false);
    }
}
