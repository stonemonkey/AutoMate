using Common;

namespace Ui.Wp8.Infrastructure
{
    public class JsonEncoder : IClientStatisticsEncoder
    {
        public string ContentType
        {
            get { return "application/json"; }
        }

        public string Encode(ClientStatistics clientStatistics)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(clientStatistics);
        }
    }
}