// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Collections.Generic;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class Battle
    {
        public partial class Magic
        {
            public static Boolean WeaponAttribute(Player Attacker, AdvancedEntity Target)
            {
                try
                {
                    if (Attacker.TransformEndTime != 0)
                        return false;

                    Item RWeapon = Attacker.GetItemByPos(4);
                    Item LWeapon = Attacker.GetItemByPos(5);

                    List<Int16> Magics = new List<Int16>();

                    if (RWeapon != null && RWeapon.Attr >= 200)
                    {
                        if (Database2.AllMagics.ContainsKey(((RWeapon.Attr + 10000) * 10)))
                            Magics.Add((Int16)(RWeapon.Attr + 10000));
                    }

                    if (LWeapon != null && LWeapon.Attr >= 200)
                    {
                        if (Database2.AllMagics.ContainsKey(((LWeapon.Attr + 10000) * 10)))
                            Magics.Add((Int16)(LWeapon.Attr + 10000));
                    }

                    if (Magics.Count <= 0)
                        return false;

                    Byte RandNumber = (Byte)MyMath.Generate(0, (Magics.Count - 1));
                    Int16 Magic = Magics.ToArray()[RandNumber];

                    MagicType.Entry Info = new MagicType.Entry();
                    if (Database2.AllMagics.TryGetValue((Magic * 10), out Info))
                    {
                        if (MyMath.Success(Info.Success))
                        {
                            Attacker.MagicType = Magic;
                            Attacker.MagicLevel = 0;
                            if (Info.ActionSort == 16 && Target != null)
                            {
                                Battle.Magic.Use(Attacker, Target, Attacker.X, Attacker.Y);
                                return true;
                            }
                            else
                            {
                                Battle.Magic.Use(Attacker, Attacker, Attacker.X, Attacker.Y);
                                return true;
                            }
                        }
                    }
                    return false;
                }
                catch (Exception Exc) { Program.WriteLine(Exc); return false; }
            }
        }
    }
}