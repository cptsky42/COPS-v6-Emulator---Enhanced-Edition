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
        public static Int32 GetDamageMonster2Player(Monster Attacker, Player Target)
        {
            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            switch (Attacker.AtkType)
            {
                case 2:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                        Damage = AdjustDamageMonster2Player(Damage, Attacker, Target);

                        Damage -= Target.Defence;
                        break;
                    }
                case 21:
                    {
                        Damage = Attacker.MagicAtk;
                        Damage = AdjustDamageMonster2Player(Damage, Attacker, Target);

                        Damage *= ((Double)(100 - Target.MagicDef) / 100);
                        Damage -= Target.MagicBlock;
                        Damage *= 0.75;
                        break;
                    }
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            if (Damage < 1)
                Damage = 1;

            Damage = AdjustMinDamageMonster2Player(Damage, Attacker, Target);

            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, new MsgName(Target.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        public static Int32 GetDamageMonster2Player(Monster Attacker, Player Target, UInt16 MagicType, Byte MagicLevel)
        {
            Magic.Info Info = new Magic.Info();
            Database.AllMagics.TryGetValue((MagicType * 10) + MagicLevel, out Info);

            Double Damage = 0;

            Double Reborn = 1.00;
            if (Target.Metempsychosis == 1)
                Reborn -= 0.30; //30%
            else if (Target.Metempsychosis >= 2)
                Reborn -= 0.50; //50%

            if (Info.Type == 1115 || (Info.WeaponSubtype != 0 && Info.WeaponSubtype != 500))
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamageMonster2Player(Damage, Attacker, Target);

                Damage -= Target.Defence;
            }
            else if (Info.WeaponSubtype == 500)
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamageMonster2Player(Damage, Attacker, Target);
            }
            else
            {
                Damage = Attacker.MagicAtk;
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamageMonster2Player(Damage, Attacker, Target);

                Damage *= ((Double)(100 - Target.MagicDef) / 100);
                Damage -= Target.MagicBlock;
                Damage *= 0.75;
            }

            Damage *= Reborn;
            Damage *= Target.Bless;
            Damage *= Target.GemBonus;

            if (Damage < 1)
                Damage = 1;

            Damage = AdjustMinDamageMonster2Player(Damage, Attacker, Target);

            if (Target.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage = 1;
                World.BroadcastRoomMsg(Target, new MsgName(Target.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        private static Int32 AdjustMinDamageMonster2Player(Double Damage, Monster Attacker, Player Target)
        {
            Int32 MinDmg = 7;
            if (Damage >= MinDmg || Target.Level <= 15)
                return (Int32)Damage;

            MinDmg += (Int32)(Attacker.Level / 10);

            Item Item = Target.GetItemByPos(3);
            if (Item != null)
                MinDmg -= (Item.Type % 10);

            if (Item != null && (Item.Type % 10) == 0)
                MinDmg = 1;

            MinDmg = Math.Max(1, MinDmg);

            return Math.Max(MinDmg, (Int32)Damage);
        }

        private static Int32 AdjustDamageMonster2Player(Double Damage, Monster Attacker, Player Target)
        {
            Byte Level = 120;
            if (Attacker.Level < 120)
                Level = (Byte)Attacker.Level;

            if (Attacker.IsRed(Target))
                Damage *= 1.5;
            else if (Attacker.IsBlack(Target))
            {
                Int32 DeltaLvl = Target.Level - Level;
                if (DeltaLvl >= -10 && DeltaLvl <= -5)
                    Damage *= 2.0;
                else if (DeltaLvl >= -20 && DeltaLvl < -10)
                    Damage *= 3.5;
                else if (DeltaLvl < -20)
                    Damage *= 5.0;
            }

            return Math.Max(0, (Int32)Damage);
        }
    }
}
