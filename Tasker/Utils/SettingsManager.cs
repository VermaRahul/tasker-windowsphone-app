﻿using System;
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
                _settings.Save();
            }
        }

        public bool SaveSetting(string key, string value, bool isProtected = false)
        {
            try
            {
                _settingsDictionary[key] = isProtected ? GetProtectedDataString(value) : value;
                _settings[SettingsKey] = _settingsDictionary;
                _settings.Save();
                return true;
            }
            catch (Exception e)
            {
                if (_settingsDictionary.ContainsKey(key))
                    _settingsDictionary.Remove(key);
                Debug.WriteLine("Failed to save {0} to vault: {1}", key, e);
            }

            return false;
        }

        public bool RemoveSettings()
        {
            var result = _settings.Remove(SettingsKey);
            _settings.Save();
            return result;
        }

        public string RetrieveSetting(string key, bool isProtected = false)
        {
            string value;
            if (_settingsDictionary.TryGetValue(key, out value))
                return isProtected ? GetUnprotectedDataString(value) : value;

            if (_settings.TryGetValue(key, out value))
            {
                _settingsDictionary[key] = value;
                return isProtected ? GetUnprotectedDataString(value) : value;
            }

            return null;
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