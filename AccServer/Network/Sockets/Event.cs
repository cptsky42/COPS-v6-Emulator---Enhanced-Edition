// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;

namespace COServer.Network.Sockets
{
    public delegate void NetworkClientConnection(TcpSocket aSocket);
    public delegate void NetworkClientReceive(TcpSocket aSocket, ref Byte[] aData);
    public delegate void NetworkClientDisconnection(TcpSocket aSocket);
}