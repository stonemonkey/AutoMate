using System.Collections.Generic;
using Dto;

namespace WebUI.Models
{
    public class Statistics
    {
        public string BingMapAuthKey { get; set; }
        public List<County> Counties { get; set; }
        public List<ClientStatistics> ClientStatisticses { get; set; }
    }
}