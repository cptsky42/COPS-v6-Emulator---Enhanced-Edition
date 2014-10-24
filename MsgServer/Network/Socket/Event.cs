using System;

namespace ConquerServer_v1Acc.Network
{
    public delegate void NetworkClientConnection(Object Client);
    public delegate void NetworkClientReceive(Object Client, Byte[] Buffer);
    public delegate void NetworkClientDisconnection(Object Client);
}
