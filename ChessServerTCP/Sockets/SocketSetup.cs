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

            listener.Bind(iPEndPoint);
            listener.Listen(100);

            var handler = await listener.AcceptAsync();
            
            while(handler.Connected)
            {
                var buffer = new byte[1024];
                var recieved = await handler.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, recieved);

                
               if(response != null)
                {
                    
                    
                    
                }
                   



        }

    }
    }
