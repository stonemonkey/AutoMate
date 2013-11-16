using System.Web.Configuration;

namespace WebUI
{
    public class Configuration
    {
        private const string BingMapsAuthKeySettings = "bingMapsAuthenticationKey";

        public static string BingMapsAuthenticationKey
        {
            get { return WebConfigurationManager.AppSettings[BingMapsAuthKeySettings]; }
        }

        public static string UrlLocationBase
        {
            get
            {
                return "http://dev.virtualearth.net/REST/v1/Locations?output=xml&countryRegion=&adminDistrict=&locality={0}&postalCode=&addressLine=&userLocation=&userIp=&usermapView=&includeNeighborhood=&maxResults=&key={1}";
            }
        }
    }
}