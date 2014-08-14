using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Model;
using Tasker.PCL.Utils;

namespace Tasker.Utils
{
    public class SettingsManager : ISettingsManager
    {
        private Dictionary<string, string> _settingsDictionary;

        private const string SettingsKey = "Settings";

        public const string AccessTokenKey = "AccessToken";
        public const string UsernameKey = "Username";


        private IsolatedStorageSettings _settings;

        public SettingsManager()
        {
            _settings = IsolatedStorageSettings.ApplicationSettings;
            object creds = null;

            if (_settings.TryGetValue(SettingsKey, out creds))
            {
                _settingsDictionary = creds as Dictionary<string, string>;
            }
            else
            {
                _settingsDictionary = new Dictionary<string, string>();
                _settings[SettingsKey] = _settingsDictionary;
            }
        }

        public AppSettings RetrieveSettings()
        {
            try
            {
                if (_settingsDictionary == null || !_settingsDictionary.Any())
                    return null;

                var appSettings = new AppSettings();

                if (_settingsDictionary.ContainsKey(AccessTokenKey))
                    appSettings.AccessToken = GetUnprotectedDataString(_settingsDictionary[AccessTokenKey]);

                //TODO Add other settings

                return appSettings.IsValid() ? appSettings : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading settings {0}", e);
            }

            return null;
        }

        public bool SaveCredentials(string username, string token)
        {
            return SaveSetting(username, UsernameKey, true) && SaveSetting(token, AccessTokenKey, true);
        }

        public bool SaveSetting(string value, string name, bool isProtected)
        {
            try
            {
                _settingsDictionary[name] = isProtected ? GetProtectedDataString(value) : value;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to save {0} to vault: {1}", name, e);
            }

            return false;
        }

        public bool RemoveSettings()
        {
            var result = _settings.Remove(SettingsKey);
            _settings.Save();
            return result;
        }

        private string GetProtectedDataString(string unprotectedData)
        {
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedData);
            var protectedBytes = ProtectedData.Protect(unprotectedBytes, null);

            return Convert.ToBase64String(protectedBytes);
        }

        private string GetUnprotectedDataString(string protectedData)
        {
            var protectedBytes = Convert.FromBase64String(protectedData);
            var unprotectedBytes = ProtectedData.Unprotect(protectedBytes, null);

            return Encoding.UTF8.GetString(unprotectedBytes, 0, unprotectedBytes.Length);
        }

    }
}