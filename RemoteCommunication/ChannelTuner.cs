using System.Net.Http;

namespace RemoteCommunication
{
    public class ChannelTuner : CommunicationManager
    {
        public ChannelTuner(string direcTvIp) : base(direcTvIp)
        {

        }

        public async void SetChannel(string majorChannel)
        {
            HttpResponseMessage response = await SendGetRequest("http://" + _direcTvIp + ":8080/tv/tune" + "?major=" + majorChannel);
        }

        public async void SetChannel(string majorChannel, string minorChannel)
        {
            HttpResponseMessage response = await SendGetRequest("http://" + _direcTvIp + ":8080/tv/tune" + "?major=" + majorChannel + "&minor=" + minorChannel);
        }

        /// <summary>
        /// Windows phone caches responses. There's no way to turn this off. Therefore, we add a dummy parameter
        /// that changes on each request.
        /// </summary>
        /// <param name="button">The button that was pressed</param>
        public async void SimulateRemoteButton(RemoteButton button)
        {
            string buttonText = StringValueAttribute.GetStringValue(button);

            string url = "http://" + _direcTvIp + ":8080/remote/processKey" + "?key=" + buttonText +
                         "&hold=keyPress&callback=dummy" + _random.Next();

            HttpResponseMessage response = await SendGetRequest(url);
        }
    }
}
