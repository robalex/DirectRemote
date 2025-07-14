using RemoteCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DirectRemoteUniversal
{
    public sealed partial class Settings : Page
    {
        private CancellationTokenSource _cancelScan;

        private const int _WIFI_INTERFACE_TYPE = 71;

        public Settings()
        {
            this.InitializeComponent();
            this.Loaded += Settings_Loaded;
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            
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

        private async void scanButton_Click(object sender, RoutedEventArgs e)
        {
            scanButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            scanProgress.Visibility = Visibility.Visible;
            scanProgress.IsEnabled = true;
            await ScanForBoxes();
        }

        private async Task ScanForBoxes()
        {
            var deviceIp = GetDeviceIp();
            _cancelScan = new CancellationTokenSource();
            var boxes = await PortScanner.ScanForDirecTVBoxes(deviceIp, _cancelScan.Token);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if(_cancelScan != null)
            {
                _cancelScan.Cancel();
            }

            scanButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            scanProgress.Visibility = Visibility.Collapsed;
        }
    }
}
