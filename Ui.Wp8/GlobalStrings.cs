namespace Ui.Wp8
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class GlobalStrings
    {
        private static readonly GlobalResources _localizedResources = new GlobalResources();

        public GlobalResources LocalizedResources { get { return _localizedResources; } }
    }
}