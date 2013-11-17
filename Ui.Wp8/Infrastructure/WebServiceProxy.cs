using System;
using System.Net;
using System.Threading.Tasks;
using Dto;
using System.IO;

namespace Ui.Wp8.Infrastructure
{
    public class WebServiceProxy
    {
        private readonly string _uri;
        private IClientStatisticsEncoder _encoder;

        public IClientStatisticsEncoder Encoder { set { _encoder = value; } }

        public WebServiceProxy(string uri)
        {
            _uri = uri;
        }

        public async Task<string> Post(ClientStatistics clientStatistics)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    var data = _encoder.Encode(clientStatistics, stream);
                    var streamReader = new StreamReader(stream);

                    var client = new WebClient();
                    client.Headers[HttpRequestHeader.ContentType] = _encoder.ContentType;
                    return await client.UploadStringTaskAsync(new Uri(_uri), "POST", streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}