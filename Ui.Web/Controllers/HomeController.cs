using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using DataAccess.Ef;
using Common;
using Ui.Web.Models;

namespace Ui.Web.Controllers
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

        public string GetBingMapsAuthKey()
        {
            return Configuration.BingMapsAuthenticationKey;
        }

        public JsonResult GetStartEndDate()
        {
            var endDate = repository.GetUsersStatistisc().Max(x => x.DateTime);
            var startDate = repository.GetUsersStatistisc().Min(x => x.DateTime);

            return Json(new
                {
                    StartDate = startDate.ToString("yyyy-MM-dd"),
                    EndDate = endDate.ToString("yyyy-MM-dd")
                }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoundaries()
        {
            var xml = new XmlDocument();
            xml.Load(Server.MapPath("~/Data/ro.kml"));
            var counties = new List<County>();
            foreach (XmlNode xNode in xml.SelectNodes("descendant::Placemark"))
            {
                var coordinates = xNode.SelectNodes("descendant::coordinates")[0].InnerText.Split(new char[] { '\n', '\r' }).Where(s => s.IndexOf(',') > 0).ToList();
                var county = new County
                {
                    Name = xNode.SelectNodes("name")[0].InnerText,
                    Locations = coordinates.Select(f => new Location
                    {
                        Latitude = float.Parse(f.Trim().Split(',')[1]),
                        Longitude = float.Parse(f.Trim().Split(',')[0])
                    }).ToList()
                };
                counties.Add(county);
            }

            return Json(counties, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatistics(DateTime startDate, DateTime endDate)
        {
            var statistics = new Statistics
            {
                BingMapAuthKey = Configuration.BingMapsAuthenticationKey,
                ClientStatisticses = GetDistrictAgresivityRates(startDate, endDate),
                Counties = null
            };

            return Json(statistics, JsonRequestBehavior.AllowGet);
        }

        private List<ClientStatistics> GetDistrictAgresivityRates(DateTime startDate, DateTime endDate)
        {
            try
            {
                var userStatistics =
                        (from userStatistic in repository.GetUsersStatistisc()
                         where userStatistic.DateTime >= startDate && userStatistic.DateTime <= endDate
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

    }
}
