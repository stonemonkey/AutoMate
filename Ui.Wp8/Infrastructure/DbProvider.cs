namespace Ui.Wp8.Infrastructure
{
    public class DbProvider
    {
        public string BuildDbName(string email)
        {
            return GlobalResources.ApplicationTitle + "_" + email;
        }
    }
}