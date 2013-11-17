using Dto;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Ui.Wp8.Infrastructure
{
    public class JsonEncoder : IClientStatisticsEncoder
    {
        public string ContentType
        {
            get { return "application/json"; }
        }

        public async Task Encode(ClientStatistics clientStatistics, Stream stream)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, clientStatistics);
                await stream.FlushAsync();
            }
        }
    }
}