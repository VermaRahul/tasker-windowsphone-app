using Tasker.PCL.Resources;

namespace Tasker.Resources
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static readonly AppResources _localizedResources = Tasker.PCL.Resources.LocalizedStrings.GetInstance().AppResources;

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}