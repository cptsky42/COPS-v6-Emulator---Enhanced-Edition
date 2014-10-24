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
        public static Int32 GetDamagePlayer2Player(Player Attacker, Player Target)
        {
            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            Double Dodge = 1.00;
            Dodge -= (Double)Target.Dodge / 100;
            Dodge += (Double)Target.Weight / 100;

            switch (Attacker.AtkType)
            {
                case 2:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);

                        if (Attacker.ContainsFlag(Player.Flag.SuperMan))
                            Damage *= 0.2; //PvP Reduction!

                        Damage -= Target.Defence;
                        break;
                    }
                case 21:
                    {
                        Damage = Attacker.MagicAtk;

                        //Damage *= ((Double)(100 - Target.MagicDef) / 100);
                        //Damage -= Target.MagicBlock;
                        //Damage *= 0.75;

                        Damage *= ((Double)(100 - Math.Min(Target.MagicDef, 95)) / 100);
                        Damage -= Target.MagicBlock;
                        Damage *= 0.65;
                        break;
                    }
                case 25:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);

                        if (Attacker.ContainsFlag(Player.Flag.SuperMan))
                            Damage *= 0.2; //PvP Reduction!

                        Damage *= Dodge;
                        Damage *= 0.10;
                        //Damage *= 0.12;
                        break;
                    }
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            Double BattlePower = Attacker.Potency - Target.Potency;
            BattlePower = Math.Pow(2.0, BattlePower / 100.00);
            Damage *= BattlePower;

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, Network.MsgName.Create(Attacker.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }
            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, Network.MsgName.Create(Target.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        public static Int32 GetDamagePlayer2Player(Player Attacker, Player Target, Int16 MagicType, Byte MagicLevel)
        {
            MagicType.Entry Info = new MagicType.Entry();
            Database2.AllMagics.TryGetValue((MagicType * 10) + MagicLevel, out Info);

            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            Double Dodge = 1.00;
            Dodge -= (Double)Target.Dodge / 100;
            Dodge += (Double)Target.Weight / 100;

            if (Info.MagicType == 1115 || (Info.WeaponSubType != 0 && Info.WeaponSubType != 500))
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Attacker.ContainsFlag(Player.Flag.SuperMan))
                    Damage *= 0.2; //PvP Reduction!

                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage -= Target.Defence;
            }
            else if (Info.WeaponSubType == 500)
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Attacker.ContainsFlag(Player.Flag.SuperMan))
                    Damage *= 0.2; //PvP Reduction!

                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                Damage *= Dodge;
                Damage *= 0.10;
                //Damage *= 0.12;
            }
            else
            {
                Damage = Attacker.MagicAtk;
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;

                //Damage *= ((Double)(100 - Target.MagicDef) / 100);
                //Damage -= Target.MagicBlock;
                //Damage *= 0.75;

                Damage *= ((Double)(100 - Math.Min(Target.MagicDef, 95)) / 100);
                Damage -= Target.MagicBlock;
                Damage *= 0.65;
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            Double BattlePower = Attacker.Potency - Target.Potency;
            BattlePower = Math.Pow(2.0, BattlePower / 100.00);
            Damage *= BattlePower;

            if (Damage < 1)
                Damage = 1;

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, Network.MsgName.Create(Attacker.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }
            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, Network.MsgName.Create(Target.UniqId, "LuckyGuy", Network.MsgName.Action.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }
    }
}
