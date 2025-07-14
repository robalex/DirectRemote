using Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RemoteCommunication
{
    public class StatusChecker : CommunicationManager
    {
        private const string _GET_LOCATIONS_URL = "http://{0}:8080/info/getLocations";
        private const string _GET_TUNED_URL = "http://{0}:8080/tv/getTuned{1}";

        public async Task<IList<DirecTvBox>> BoxExists(string ipAddress)
        {
            var boxes = new List<DirecTvBox>();
            var url = string.Format(_GET_LOCATIONS_URL, ipAddress);
            var result = await SendGetRequest(url);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var locations = Newtonsoft.Json.JsonConvert.DeserializeObject<GetLocationsResult>(response);
                foreach (var location in locations.Locations)
                {
                    boxes.Add(new DirecTvBox() { ClientAddress = location.ClientAddr, IpAddress = ipAddress, Name = location.LocationName });
                }
            }

            return boxes;
        }

        public async Task<ChannelStatus> GetTuned()
        {
            var url = string.Format(_GET_TUNED_URL, _direcTvIp, "?callback=dummy" + _random.Next());
            HttpResponseMessage response = await SendGetRequest(url);

            string result = response.Content.ReadAsStringAsync().Result;
            int index = result.IndexOf('(');
            result = result.Substring(index + 1).TrimEnd(';').TrimEnd(')');

            ChannelStatus status = Newtonsoft.Json.JsonConvert.DeserializeObject<ChannelStatus>(result);
            return status;
        }
    }
}