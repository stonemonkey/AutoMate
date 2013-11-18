using System;
using System.Net;
using System.Threading.Tasks;
using Common;
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

        //POST /api/automate HTTP/1.1
        //Host: automatewebui.azurewebsites.net
        //Cache-Control: no-cache
        //Content-Type: application/x-www-form-urlencoded
        //
        //EmailAddress=jm_aba%40ahoo.com&Location=Lehliu&AgresivityRate=99
        public async Task<string> Post(ClientStatistics clientStatistics)
        {
            try
            {
                var data = _encoder.Encode(clientStatistics);

                var client = new WebClient();
                client.Headers[HttpRequestHeader.ContentType] = _encoder.ContentType;
                return await client.UploadStringTaskAsync(new Uri(_uri), "POST", data);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}