using Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteCommunication
{
    public class PortScanner
    {
        public static async Task<List<DirecTvBox>> ScanForDirecTVBoxes(string deviceIp, CancellationToken cancelScan)
        {
            var direcTvBoxes = new List<DirecTvBox>();
            var ipAddresses = GenerateIpAddressList(deviceIp);
            var statusChecker = new StatusChecker();

            foreach (var ipAddress in ipAddresses)
            {
                if(cancelScan.IsCancellationRequested)
                {
                    break;
                }

                try {
                    var boxes = await statusChecker.BoxExists(ipAddress);
                    direcTvBoxes.AddRange(boxes);
                }
                catch
                {
                    // Communication Exception. Continue scanning.
                }
               
            }

            return direcTvBoxes;
        }

        private static List<string> GenerateIpAddressList(string deviceIpAddress)
        {
            var ipAddresses = new List<string>();

            var ipParts = deviceIpAddress.Split('.');
            var lastPart = int.Parse(ipParts[3]);

            for (var i = 1; i <= 255; i++)
            {
                if (i == lastPart)
                {
                    continue;
                }

                var ipAddress = string.Format("{0}.{1}.{2}.{3}", ipParts[0], ipParts[1], ipParts[2], i);
                ipAddresses.Add(ipAddress);
            }

            return ipAddresses;
        }
    }
}