using RemoteCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DirectRemoteUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CancellationToken _cancelScan;

        private const int _WIFI_INTERFACE_TYPE = 71;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var ip = GetDeviceIp();
            var source = new CancellationTokenSource();
            _cancelScan = source.Token;
            var ips = await PortScanner.ScanForDirecTVBoxes(ip, _cancelScan);
        }

        private string GetDeviceIp()
        {
            var hostnames = NetworkInformation.GetHostNames();
            var ipAddress = string.Empty;

            foreach (var hn in hostnames)
            {
                if (hn.IPInformation != null)
                {
                    if (hn.IPInformation.NetworkAdapter.IanaInterfaceType == _WIFI_INTERFACE_TYPE)
                    {
                        ipAddress = hn.DisplayName;
                    }
                }
            }

            return ipAddress;
        }
    }
}
