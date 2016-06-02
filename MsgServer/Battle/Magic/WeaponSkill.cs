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
                    RType = (Int16)(RWeapon.Type / 1000);

                Int16 LType = 0;
                if (LWeapon != null)
                    LType = (Int16)(LWeapon.Type / 1000);

                if (RType == 421)
                    RType = 420;

                if (LType == 421)
                    LType = 421;

                List<UInt16> Magics = new List<UInt16>();
                foreach (var magic in Attacker.Magics)
                {
                    if (!magic.TargetType.HasFlag(MagicTarget.Passive))
                        continue;

                    if (magic.WeaponSubtype != 0 && magic.WeaponSubtype != RType && magic.WeaponSubtype != LType)
                        continue;

                    Magics.Add(magic.Type);
                }

                if (Magics.Count <= 0)
                    return false;

                Byte RandNumber = (Byte)MyMath.Generate(0, (Magics.Count - 1));
                COServer.Magic WMagic = Attacker.GetMagicByType(Magics.ToArray()[RandNumber]);
                if (WMagic != null)
                {
                    Magic.Info Info = new Magic.Info();
                    if (Database.AllMagics.TryGetValue((WMagic.Type * 10) + WMagic.Level, out Info))
                    {
                        if (MyMath.Success(Info.Success))
                        {
                            Attacker.MagicType = WMagic.Type;
                            Attacker.MagicLevel = WMagic.Level;
                            if (Info.Sort == MagicSort.AtkStatus || Info.Sort == MagicSort.Line)
                            {
                                Battle.UseMagic(Attacker, Target, Attacker.X, Attacker.Y);
                                Attacker.MagicType = 0;
                                Attacker.MagicLevel = 0;
                                return true;
                            }
                            else
                            {
                                Battle.UseMagic(Attacker, null, Attacker.X, Attacker.Y);
                                Attacker.MagicType = 0;
                                Attacker.MagicLevel = 0;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception exc) { sLogger.Error(exc); return false; }
        }
    }
}