using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.ClientEngine;
using SuperSocketClient.AppBase;
using SurperSocket.Core.Client.AppBase;
using SurperSocket.Core.Client.Models;

namespace SurperSocket.Core.Client.Tools
{
    /// <summary>
    /// 客户端向服务器发送消息
    /// </summary>
    public static class SendMsgToServiceExt
    {
        /// <summary>
        /// 客户端向服务器发送消息
        /// </summary>
        /// <param name="client">客户端对象</param>
        /// <param name="command">命令</param>
        /// <param name="msg">数据包</param>
        public static void Send(this EasyClient<CustomPackageInfo> client, CustomCommand command, string msg)
        {
            if (client != null && client.IsConnected && !string.IsNullOrEmpty(msg))
            {
                byte[] data = GetMsgBytes(msg, command);
                client.Send(data);
            }
        }
        
        /// <summary>
        /// 组装自定义数据包
        /// </summary>
        /// <typeparam name="T">数据包类型</typeparam>
        /// <param name="msg">数据</param>
        /// <param name="command">命令</param>
        /// <param name="platform">目标平台</param>
        /// <param name="shopId">店铺ID</param>
        /// <param name="info">普通消息</param>
        /// <returns></returns>
        public static string GetTransmitPackets<T>(this T msg, CustomCommand command, string platform = "", string shopId = "", string info = "") where T : class
        {
            TransmitPackets<T> transmit = new TransmitPackets<T>
            {
                Command = (int)command,
                Data = msg,
                Platform = platform,
                ShopId = shopId,
                Message = info
            };
            return transmit.TryToJson(true);
        }

        /// <summary>
        /// 组装消息协议
        /// </summary>
        /// <param name="msg">数据包</param>
        /// <param name="command">命令</param>
        /// <returns></returns>
        private static byte[] GetMsgBytes(string msg, CustomCommand command)
        {
            //协议: 头部包含 4 个字节, 前 2 个字节用于存储请求的名字（命令值）, 后 2 个字节用于代表请求体的长度
            List<byte> response = new List<byte>();
            if (!string.IsNullOrEmpty(msg))
            {
                //命令值
                response = BitConverter.GetBytes((ushort)command).Reverse().ToList();
                //数据包
                byte[] data = Encoding.UTF8.GetBytes(msg);
                //包长度
                response.AddRange(BitConverter.GetBytes((ushort)data.Length).Reverse().ToArray());

                response.AddRange(data);
            }
            return response.ToArray();
        }
    }
}