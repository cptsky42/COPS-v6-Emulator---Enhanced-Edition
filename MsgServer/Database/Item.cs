// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using COServer.Threads;

namespace COServer
{
    public partial class Database
    {
        public unsafe static void GetAllItems()
        {
            try
            {
                Console.Write("Loading all items in memory...  ");

                FileStream Stream = new FileStream(Program.RootPath + "\\Items\\Item.pkg", FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

                World.AllItems = new Dictionary<Int32, Item>(200000);
                World.ItemThread = new ItemThread(Stream);

                Byte[] Buffer = new Byte[256];
                Int32 Read = 0;

                //using (FileStream Stream = new FileStream(Program.RootPath + "\\Items\\Item.pkg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Item.Header* pHeader = stackalloc Item.Header[1];
                    Read = Stream.Read(Buffer, 0, sizeof(Item.Header));
                    if (Read != sizeof(Item.Header))
                    {
                        Marshal.FreeHGlobal((IntPtr)pHeader);

                        Console.WriteLine("\bOk!");
                        return;
                    }

                    Marshal.Copy(Buffer, 0, (IntPtr)pHeader, sizeof(Item.Header));

                    if (pHeader->Identifier != 0x4244494B)
                    {
                        Console.WriteLine("\bOk!");
                        return;
                    }

                    Item.Info* pInfo = stackalloc Item.Info[1];
                    for (Int32 i = 0; i < pHeader->MaxAmount; i++)
                    {
                        Item Item = null;

                        Read = Stream.Read(Buffer, 0, sizeof(Item.Info));
                        if (Read != sizeof(Item.Info))
                            continue;

                        Marshal.Copy(Buffer, 0, (IntPtr)pInfo, sizeof(Item.Info));

                        if (pInfo->OwnerUID == 0)
                            continue;

                        Item = new Item(pInfo);
                        if (!World.AllItems.ContainsKey(Item.UniqId))
                            World.AllItems.Add(Item.UniqId, Item);
                    }
                }
                Buffer = null;
                Console.WriteLine("\bOk!");
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
