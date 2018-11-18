# SurperSocket.Core
基于SurperSocket自定义命令方式（头部格式固定并且包含内容长度的协议）实现一个Socket服务端、客户端，同时对SurperSocket相关常用方法进行了二次封装，完全自定义协议实现

当前项目采用的协议如下：
```C#
/// +-------+---+-------------------------------+
/// |request| l |                               |
/// | name  | e |    request body               |
/// |  (2)  | n |                               |
/// |       |(2)|                               |
/// +-------+---+-------------------------------+
```
头部包含 4 个字节, 前 2 个字节用于存储请求的名字（命令值）, 后 2 个字节用于代表请求体的长度，其余部分为数据

SurperSocket协议地址：http://docs.supersocket.net/v1-6/zh-CN/The-Built-in-Common-Format-Protocol-Implementation-Templates

服务端启动方式：
```C#
SocketServiceEasyClient easyClient = new SocketServiceEasyClient();
easyClient.InitEasyClient();
```

客户端启动方式：
```C#
SocketClientEasyClient client = new SocketClientEasyClient(new IPEndPoint(IPAddress.Parse("192.168.31.38"), 9005));
EasyClient<CustomPackageInfo> res = client.InitEasyClient();
while (Console.ReadLine() != "")
 {
    string data = Console.ReadLine();
    string json = data.GetTransmitPackets(CustomCommand.Test);
    res.Send(CustomCommand.Test, json);
 }
```
