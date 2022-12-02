using PortScanner;
using System.Net;
using System.Net.NetworkInformation;
List<PortInfo> list = GetOpenPort();
foreach (var item in list)
{
    Console.WriteLine(item.PortNumber+" "+item.Local+" "
        +item.Remote+" "+item.State);
}

List<PortInfo> GetOpenPort()
{
    IPGlobalProperties properties=IPGlobalProperties.GetIPGlobalProperties();
    IPEndPoint[] tcpEndPoints=properties.GetActiveTcpListeners();
    TcpConnectionInformation[] tcpConnectionInformation=
        properties.GetActiveTcpConnections();
    return tcpConnectionInformation.Select(p =>
        {
        return new PortInfo(p.LocalEndPoint.Port,
            String.Format("{0}:{1}", p.LocalEndPoint.Address, p.LocalEndPoint.Port),
            String.Format("{0}:{1}", p.RemoteEndPoint.Address, p.RemoteEndPoint.Port),
            p.State.ToString()
            );
        }).ToList();
}