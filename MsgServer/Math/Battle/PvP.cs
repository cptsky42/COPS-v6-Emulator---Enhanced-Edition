// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * COPS v6 Emulator

using System;
using COServer.Entities;
using COServer.Network;

namespace COServer
{
    public partial class MyMath
    {
        public static Int32 GetDamagePlayer2Player(Player Attacker, Player Target)
        {
            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            Double Dodge = 1.00 - (Math.Min(Target.Dodge, 100.00) / 100.00);

            switch (Attacker.AtkType)
            {
                case 2:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);

                        if (Attacker.HasStatus(Status.SuperAtk))
                            Damage *= 0.2; //PvP Reduction!

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

                        if (Attacker.HasStatus(Status.SuperAtk))
                            Damage *= 0.2; //PvP Reduction!

                        Damage *= Dodge;
                        Damage *= 0.12;
                        break;
                    }
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, new MsgName(Attacker.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, new MsgName(Target.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        public static Int32 GetDamagePlayer2Player(Player Attacker, Player Target, UInt16 MagicType, Byte MagicLevel)
        {
            Magic.Info Info = new Magic.Info();
            Database.AllMagics.TryGetValue((MagicType * 10) + MagicLevel, out Info);

            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            Double Dodge = 1.00 - (Math.Min(Target.Dodge, 100.00) / 100.00);

            if (Info.Type == 1115 || (Info.WeaponSubtype != 0 && Info.WeaponSubtype != 500))
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Attacker.HasStatus(Status.SuperAtk))
                    Damage *= 0.2; //PvP Reduction!

                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage -= Target.Defence;
            }
            else if (Info.WeaponSubtype == 500)
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Attacker.HasStatus(Status.SuperAtk))
                    Damage *= 0.2; //PvP Reduction!

                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage *= Dodge;
                Damage *= 0.12;
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
                Damage *= 0.75;
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, new MsgName(Attacker.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, new MsgName(Target.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }
    }
}
