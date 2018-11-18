using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ClientEngine;
using SuperSocketClient.AppBase;
using SurperSocket.Core.Client.AppBase;
using SurperSocket.Core.Client.Tools;

namespace SurperSocket.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClientEasyClient client = new SocketClientEasyClient(new IPEndPoint(IPAddress.Parse("192.168.31.38"), 9005));
            EasyClient<CustomPackageInfo> res = client.InitEasyClient();

            while (Console.ReadLine() != "")
            {
                string json = "你好！".GetTransmitPackets(CustomCommand.Test);
                res.Send(CustomCommand.Test, json);
            }

        }
    }
}
