using System.IO;
using System.Xml.Serialization;
using Common;
using System.Threading.Tasks;

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
            var writer = new StringWriter();
            var serializer = new XmlSerializer(typeof(ClientStatistics));

            serializer.Serialize(writer, clientStatistics);

            return writer.ToString();
        }
    }
}