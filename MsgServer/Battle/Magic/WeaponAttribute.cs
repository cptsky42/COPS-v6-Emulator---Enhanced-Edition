// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using System.Collections.Generic;
using COServer.Entities;

namespace COServer
{
    public partial class Battle
    {
        public static Boolean WeaponAttribute(Player Attacker, AdvancedEntity Target)
        {
            try
            {
                if (Attacker.TransformEndTime != 0)
                    return false;

                Item RWeapon = Attacker.GetItemByPos(4);
                Item LWeapon = Attacker.GetItemByPos(5);

                List<UInt16> Magics = new List<UInt16>();

                if (RWeapon != null && RWeapon.Attribute >= 200)
                {
                    if (Database.AllMagics.ContainsKey(((RWeapon.Attribute + 10000) * 10)))
                        Magics.Add((UInt16)(RWeapon.Attribute + 10000));
                }

                if (LWeapon != null && LWeapon.Attribute >= 200)
                {
                    if (Database.AllMagics.ContainsKey(((LWeapon.Attribute + 10000) * 10)))
                        Magics.Add((UInt16)(LWeapon.Attribute + 10000));
                }

                if (Magics.Count <= 0)
                    return false;

                Byte RandNumber = (Byte)MyMath.Generate(0, (Magics.Count - 1));
                UInt16 Magic = Magics.ToArray()[RandNumber];

                Magic.Info Info = new Magic.Info();
                if (Database.AllMagics.TryGetValue((Magic * 10), out Info))
                {
                    if (MyMath.Success(Info.Success))
                    {
                        Attacker.MagicType = Magic;
                        Attacker.MagicLevel = 0;
                        if (Info.Sort == MagicSort.AtkStatus && Target != null)
                        {
                            Battle.UseMagic(Attacker, Target, Attacker.X, Attacker.Y);
                            return true;
                        }
                        else
                        {
                            Battle.UseMagic(Attacker, Attacker, Attacker.X, Attacker.Y);
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception exc) { sLogger.Error(exc); return false; }
        }
    }
}