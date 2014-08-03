namespace Tasker.PCL.Resources
{
    public class LocalizedStrings
    {
        private static readonly AppResources _appResources = new AppResources();

        public AppResources AppResources { get { return _appResources; } }

        private static LocalizedStrings _localizedStrings;
        private LocalizedStrings() { }

        public static LocalizedStrings GetInstance()
        {
            if (_localizedStrings == null)
            {
                _localizedStrings = new LocalizedStrings();
            }

            return _localizedStrings;
        }
    }
}
