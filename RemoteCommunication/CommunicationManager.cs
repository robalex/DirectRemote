using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteCommunication
{
    public class CommunicationManager
    {
        protected string _direcTvIp;
        protected Random _random;
        private HttpClient _client;

        protected const long _MAX_RESPONSE_BUFFER_SIZE = 256000;
        protected const string _USER_AGENT_FIELD = "user-agent";
        protected const string _USER_AGENT_VALUE = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
        protected const int _MAX_RESPONSE_TIME_MS = 200;

        public CommunicationManager(string direcTvIp)
        {
            _direcTvIp = direcTvIp;
            _random = new Random();
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = _MAX_RESPONSE_BUFFER_SIZE;
            _client.DefaultRequestHeaders.Add(_USER_AGENT_FIELD, _USER_AGENT_VALUE);
        }

        public CommunicationManager()
        {
            _direcTvIp = string.Empty;
            _random = new Random();
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = _MAX_RESPONSE_BUFFER_SIZE;
            _client.DefaultRequestHeaders.Add(_USER_AGENT_FIELD, _USER_AGENT_VALUE);
        }  

        protected async Task<HttpResponseMessage> SendGetRequest(string url)
        {
            
            var source = new CancellationTokenSource(_MAX_RESPONSE_TIME_MS);

            HttpResponseMessage response = await _client.GetAsync(url, source.Token);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}