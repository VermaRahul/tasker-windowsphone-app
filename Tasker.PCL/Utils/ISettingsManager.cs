using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasker.PCL.Model;

namespace Tasker.PCL.Utils
{
    public interface ISettingsManager
    {
        bool RemoveSettings();
        bool SaveSetting(string key, string value, bool isProtected = false);
        string RetrieveSetting(string key, bool isProtected = false);

        Task WriteDataToFileAsync(string fileName, string value);
        Task<string> ReadFileContentsAsync(string fileName);

        Task WriteJsonDataToFileAsync(string fileName, JsonData data);
        Task<T> ReadJsonDataFileAsync<T>(string fileName);
    }
}
