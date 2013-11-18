using System.Net;
using System.Xml;

namespace Ui.Web.Models
{
    public class Location2County
    {
        public static string GetLocationAdminDistrictName(string locationName)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var result = client.DownloadString(GetUrl(locationName));

                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(result);
                    var districtName = xmlDocument.GetElementsByTagName("AdminDistrict");
                    if (districtName.Count != 0)
                    {
                        if (!string.IsNullOrEmpty(districtName[0].InnerText))
                            return districtName[0].InnerText;
                    }
                }
            }
            catch
            {

            }
            return string.Empty;
        }

        private static string GetUrl(string locationName)
        {
            return
                string.Format(
                    Configuration.UrlLocationBase,
                    locationName,
                    Configuration.BingMapsAuthenticationKey);
        }
    }
}