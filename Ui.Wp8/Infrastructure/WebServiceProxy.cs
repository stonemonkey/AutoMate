using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dto;

namespace Ui.Wp8.Infrastructure
{
    public class WebServiceProxy
    {
        private readonly string _uri;

        public WebServiceProxy(string uri)
        {
            _uri = uri;
        }

        public async Task<string> Post(ClientStatistics clientStatistics)
        {
            await Task.Delay(1500);
            return "HTTP/1.1 200 OK ...";
            //return "HTTP/1.1 300 ...";

            //var xml = Xml(clientStatistics);

            //var client = new WebClient();
            //client.Headers[HttpRequestHeader.ContentType] = "application/xml";
            //return await client.UploadStringTaskAsync(new Uri(_uri), "POST", xml);
        }

        private static string Xml(ClientStatistics clientStatistics)
        {
            var stream = new StringWriter();
            var serializer = new XmlSerializer(typeof (ClientStatistics));
            
            serializer.Serialize(stream, clientStatistics);
            
            return stream.ToString();
        }
    }
}