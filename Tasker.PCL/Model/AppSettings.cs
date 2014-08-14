using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.PCL.Model
{
    public class AppSettings
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(AccessToken) && !String.IsNullOrEmpty(Username);
        }
    }
}
