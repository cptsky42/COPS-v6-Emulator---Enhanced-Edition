// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer
{
    public partial class MyMath
    {
        public static Int32 GetDamagePlayer2Environment(Player Attacker, TerrainNPC Target)
        {
            Double Damage = 0;

            switch (Attacker.AtkType)
            {
                case 2:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);

                        Damage -= Target.Defence;
                        break;
                    }
                case 21:
                    {
                        Damage = Attacker.MagicAtk;
  
                        Damage *= ((Double)(100 - Target.MagicDef) / 100);
                        Damage -= Target.MagicBlock;
                        Damage *= 0.75;
                        break;
                    }
                case 25:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                        break;
                    }
            }

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, Network.MsgName.Create(Attacker.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }

            return (Int32)Math.Round(Damage, 0);
        }

        public static Int32 GetDamagePlayer2Environment(Player Attacker, TerrainNPC Target, Int16 MagicType, Byte MagicLevel)
        {
            MagicType.Entry Info = new MagicType.Entry();
            Database2.AllMagics.TryGetValue((MagicType * 10) + MagicLevel, out Info);

            Double Damage = 0;

            if (Info.MagicType == 1115 || (Info.WeaponSubType != 0 && Info.WeaponSubType != 500))
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage -= Target.Defence;
            }
            else if (Info.WeaponSubType == 500)
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
            }
            else
            {
                Damage = Attacker.MagicAtk;
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage *= ((Double)(100 - Target.MagicDef) / 100);
                Damage -= Target.MagicBlock;
            }

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, Network.MsgName.Create(Attacker.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }

            return (Int32)Math.Round(Damage, 0);
        }
    }
}
