using System.IO;
using System.Xml.Serialization;
using Dto;

namespace Ui.Wp8.Infrastructure
{
    public class XmlEncoder : IClientStatisticsEncoder
    {
        public string ContentType
        {
            get { return "application/xml"; }
        }

        public string Encode(ClientStatistics clientStatistics)
        {
            var stream = new StringWriter();
            var serializer = new XmlSerializer(typeof(ClientStatistics));

            serializer.Serialize(stream, clientStatistics);

            return stream.ToString();
        }
    }
}