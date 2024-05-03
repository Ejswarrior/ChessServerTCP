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
        Console.WriteLine(hostname);
            IPHostEntry localhost = await Dns.GetHostEntryAsync("127.0.0.1");
        Console.WriteLine("Local address");
            Console.WriteLine(localhost.AddressList[0].ToString());
            IPEndPoint ipEndPoint = new(localhost.AddressList[0], 8910);
            return ipEndPoint;
        }

        public async void intializeSocket()
        {
            IPEndPoint iPEndPoint = await getIpEndpoint();

            using Socket listener = new(
            iPEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);


        Console.WriteLine("Listening on:");
         Console.WriteLine(iPEndPoint.Address.ToString());
            listener.Bind(iPEndPoint);
            listener.Listen();

        Console.WriteLine("before listener");
         var handler = await listener.AcceptAsync();
        Console.WriteLine("Before connection");

            while(true)
            {
                Console.WriteLine("connection created");

                var buffer = new byte[100];

                Console.WriteLine(buffer);
                var recieved = await handler.ReceiveAsync(buffer, SocketFlags.None);
            Console.WriteLine(recieved.ToString());
                var response = Encoding.UTF8.GetString(buffer, 0, recieved);
                
                Console.WriteLine(response);
            break; 
        /*        
               if(response != null)
                {

                    var responseMessage = Encoding.UTF8.GetBytes("Send message");
                    await handler.SendAsync(responseMessage);
                }*/
        }
    }
    }
