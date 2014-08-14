using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Tasker.Extensions;
using Tasker.PCL.ViewModel;

namespace Tasker.ViewModel
{
    public class NavigationService : NavigationService<NavigationService.PageId>
    {
        public NavigationService()
        {
            Register(typeof(MainViewModel), PageId.Main);
            Register(typeof(TasksViewModel), PageId.Tasks);
        }

        public enum PageId
        {
            Main,
            Tasks
        }

        public override void GoBack(object data)
        {
            ((PhoneApplicationFrame)Application.Current.RootVisual).GoBack(data);
        }

        private static string _version;

        private static readonly SemaphoreSlim _versionSem = new SemaphoreSlim(1);

        public override string GetAppVersion()
        {
            if (_version == null)
            {
                _versionSem.Wait();
                try
                {
                    if (_version == null)
                    {
                        _version = "Unknown";
                        var xElement = XDocument.Load("WMAppManifest.xml").Root;
                        if (xElement != null)
                        {
                            var element = xElement.Element("App");
                            if (element != null)
                            {
                                var xAttribute = element.Attribute("Version");
                                if (xAttribute != null)
                                {
                                    _version = xAttribute.Value;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    _versionSem.Release();
                }
            }
            return _version;
        }

        public override void RemoveBackEntry()
        {
            ((PhoneApplicationFrame)Application.Current.RootVisual).RemoveBackEntry();
        }

        public override void ClearBackStack()
        {
            while (((PhoneApplicationFrame)Application.Current.RootVisual).BackStack.Any())
                ((PhoneApplicationFrame)Application.Current.RootVisual).RemoveBackEntry();
        }

        protected override void NavigateTo(PageId id, object data)
        {
            string pageUri;
            switch (id)
            {
                case PageId.Main:
                    pageUri = "/View/MainPage.xaml";
                    break;
                case PageId.Tasks:
                    pageUri = "/View/TasksPage.xaml";
                    break;
                default:
                    pageUri = "/View/MainPage.xaml";
                    break;
            }
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(pageUri, UriKind.Relative), data);
        }
    }
}
