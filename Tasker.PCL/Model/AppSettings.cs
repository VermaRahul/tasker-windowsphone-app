using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.PCL.Utils;

namespace Tasker.PCL.Model
{
    public class AppSettings
    {
        public const string AccessTokenKey = "AccessToken";
        public const string UsernameKey = "Username";

        public ISettingsManager SettingsManager { get; set; }


        public string AccessToken { get; set; }
        public string Username { get; set; }

        public bool LoadSettings()
        {
            try
            {
                AccessToken = SettingsManager.RetrieveSetting(AccessTokenKey, true);
                Username = SettingsManager.RetrieveSetting(UsernameKey, true);

                //TODO Add other settings

                return IsValid();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading settings {0}", e);
            }

            return false;
        }

        public bool SaveCredentials(string username, string token)
        {
            var result = SettingsManager.SaveSetting(UsernameKey, username, true) && SettingsManager.SaveSetting(AccessTokenKey, token, true);

            if (!result)
            {
                SettingsManager.RemoveSettings();
            }

            return result;
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(AccessToken) && !String.IsNullOrEmpty(Username);
        }
    }
}
