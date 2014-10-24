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
            public static Boolean WeaponSkill(Player Attacker, AdvancedEntity Target)
            {
                try
                {
                    if (Attacker.TransformEndTime != 0)
                        return false;

                    Item RWeapon = Attacker.GetItemByPos(4);
                    Item LWeapon = Attacker.GetItemByPos(5);

                    Int16 RType = 0;
                    if (RWeapon != null)
                        RType = (Int16)(RWeapon.Id / 1000);

                    Int16 LType = 0;
                    if (LWeapon != null)
                        LType = (Int16)(LWeapon.Id / 1000);

                    if (RType == 421)
                        RType = 420;

                    if (LType == 421)
                        LType = 421;

                    List<Int16> Magics = new List<Int16>();
                    foreach (COServer.Magic Magic in Attacker.Magics.Values)
                    {
                        MagicType.Entry Info;
                        if (!Database2.AllMagics.TryGetValue((Magic.Type * 10) + Magic.Level, out Info))
                            continue;

                        if (Info.Target != 8) //Passive Skill
                            continue;

                        if (Info.WeaponSubType != 0 && Info.WeaponSubType != RType && Info.WeaponSubType != LType)
                            continue;

                        Magics.Add(Magic.Type);
                    }

                    if (Magics.Count <= 0)
                        return false;

                    Byte RandNumber = (Byte)MyMath.Generate(0, (Magics.Count - 1));
                    COServer.Magic WMagic = Attacker.GetMagicByType(Magics.ToArray()[RandNumber]);
                    if (WMagic != null)
                    {
                        MagicType.Entry Info = new MagicType.Entry();
                        if (Database2.AllMagics.TryGetValue((WMagic.Type * 10) + WMagic.Level, out Info))
                        {
                            if (MyMath.Success(Info.Success))
                            {
                                Attacker.MagicType = WMagic.Type;
                                Attacker.MagicLevel = WMagic.Level;
                                if (Info.ActionSort == 16 || Info.ActionSort == 14)
                                {
                                    Battle.Magic.Use(Attacker, Target, Attacker.X, Attacker.Y);
                                    Attacker.MagicType = -1;
                                    Attacker.MagicLevel = 0;
                                    return true;
                                }
                                else
                                {
                                    Battle.Magic.Use(Attacker, null, Attacker.X, Attacker.Y);
                                    Attacker.MagicType = -1;
                                    Attacker.MagicLevel = 0;
                                    return true;
                                }
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