using System.IO;
using System.Xml.Serialization;
using Dto;
using System.Threading.Tasks;

namespace Ui.Wp8.Infrastructure
{
    public class XmlEncoder : IClientStatisticsEncoder
    {
        public string ContentType
        {
            get { return "application/xml"; }
        }

        public async Task Encode(ClientStatistics clientStatistics, Stream stream)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                var serializer = new XmlSerializer(typeof(ClientStatistics));
                serializer.Serialize(streamWriter, clientStatistics);
                await stream.FlushAsync();
            }
        }
    }
}