namespace ChessServerTCP.Sockets;

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketSetup
    {

        public async Task<IPEndPoint> getIpEndpoint(string? host = null)
        {
            string hostname = host ?? Dns.GetHostName();
            IPHostEntry localhost = await Dns.GetHostEntryAsync(hostname);
            IPEndPoint ipEndPoint = new(localhost.AddressList[0], 11_000);
            return ipEndPoint;
        }

        public async void intializeSocket()
        {
            IPEndPoint iPEndPoint = await getIpEndpoint();
            using Socket listener = new(
            iPEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);



        }
    }
