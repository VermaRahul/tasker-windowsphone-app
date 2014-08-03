using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tasker.Extensions
{
    public static class FrameExtension
    {
        static Object _navigationData = null;

        public static bool Navigate(this Frame frame, Uri pageUri, Object obj)
        {
            _navigationData = obj;
            return frame.Navigate(pageUri);
        }

        public static void GoBack(this Frame frame, Object obj)
        {
            _navigationData = obj;

            if (frame.CanGoBack)
                frame.GoBack();
        }

        public static Object GetNavigationData(this Frame frame)
        {
            object obj = _navigationData;
            _navigationData = null;
            return obj;
        }
    }
}
