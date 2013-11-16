using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.XPath;

using System.Xml;
using System.Xml.Linq;
using DataAccess;
using Dto;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController()
        {
            repository = new Repository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStatistics()
        {
            var xml = XDocument.Load(Server.MapPath("~/Data/ro.kml"));
            
            var r = from c in xml.Descendants("Document/Placemark")
                    select new County
                        {
                            Name = c.Descendants("name").First().Value,
                            Locations = c.Descendants("coordinates").First().Value.Split('\n').Select(f => new Location
                                {
                                    Latitude = float.Parse(f.Split(',')[1]),
                                    Longitude = float.Parse(f.Split(',')[0])
                                }).ToList()
                        };

                var statistics = new Statistics
                {
                    BingMapAuthKey = Configuration.BingMapsAuthenticationKey,
                    ClientStatisticses = GetDistrictAgresivityRates(),
                    Counties = r.ToList()
                };

            return Json(statistics, JsonRequestBehavior.AllowGet);
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
            try
            {
                var userStatistics =
        (from userStatistic in repository.GetUsersStatistisc()
         group userStatistic by userStatistic.Location into grouping
         select new ClientStatistics
         {
             Location = grouping.Key,
             AgresivityRate = (int)grouping.Average(c => c.AgresivityRate)
         }).ToList();

                var agresivityRatesByDistrict = (from clientStatisticse in userStatistics
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
            catch (Exception)
            {
                
            }
            return new List<ClientStatistics>();
        }


        public string BingMapsAuthenticationKey()
        {
            return Configuration.BingMapsAuthenticationKey;
        }
    }
}
