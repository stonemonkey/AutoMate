using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Dto;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public XmlActionResult GetRoKmlData()
        {
            return new XmlActionResult(XDocument.Load(Server.MapPath("~/Data/ro.kml")));
        }

        public ActionResult GetLocationAgresivtyRates()
        {
            var agresivityRatesByDistrict = GetDistrictAgresivityRates();

            return Json(agresivityRatesByDistrict, JsonRequestBehavior.AllowGet);
        }

        private List<ClientStatistics> GetDistrictAgresivityRates()
        {
            //TODO - This info will be taken from DB 
            var agresivityRates = new List<ClientStatistics>
                {
                    new ClientStatistics {Location = "Sibiu", AgresivityRate = 70},
                    new ClientStatistics {Location = "Fagaras", AgresivityRate = 70},
                    new ClientStatistics {Location = "Arad", AgresivityRate = 70},
                    new ClientStatistics {Location = "Satu Mare", AgresivityRate = 6},
                    new ClientStatistics {Location = "Timisoara", AgresivityRate = 90},
                    new ClientStatistics {Location = "Cluj - Napoca", AgresivityRate = 26},
                    new ClientStatistics {Location = "Constanta", AgresivityRate = 100}
                };

            var agresivityRatesByDistrict = (from clientStatisticse in agresivityRates
                                             let districtName =
                                                 Location2County.GetLocationAdminDistrictName(clientStatisticse.Location)
                                             where !string.IsNullOrEmpty(districtName)
                                             select new ClientStatistics
                                                 {
                                                     AgresivityRate = clientStatisticse.AgresivityRate,
                                                     Location = districtName
                                                 }).ToList();
            return agresivityRatesByDistrict;
        }


        public string BingMapsAuthenticationKey()
        {
            return Configuration.BingMapsAuthenticationKey;
        }
    }
}
