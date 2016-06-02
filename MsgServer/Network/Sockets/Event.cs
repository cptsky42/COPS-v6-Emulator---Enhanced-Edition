// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011, 2014
// * COPS v6 Emulator

using System;

namespace COServer.Network.Sockets
{
    public delegate void NetworkClientConnection(TcpSocket aSocket);
    public delegate void NetworkClientReceive(TcpSocket aSocket, ref Byte[] aData);
}