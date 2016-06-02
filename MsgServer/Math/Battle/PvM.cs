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
        public static Int32 GetDamagePlayer2Monster(Player Attacker, Monster Target)
        {
            Double Damage = 0;

            switch (Attacker.AtkType)
            {
                case 2:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                        Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);

                        Damage -= Target.Defence;
                        break;
                    }
                case 21:
                    {
                        Damage = Attacker.MagicAtk;
                        Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);

                        Damage *= ((Double)(100 - Target.MagicDef) / 100);
                        Damage -= Target.MagicBlock;
                        Damage *= 0.75;
                        break;
                    }
                case 25:
                    {
                        Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                        Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);
                        break;
                    }
            }

            if (Damage < 1)
                Damage = 1;

            Damage = AdjustMinDamagePlayer2Monster(Damage, Attacker, Target);

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, new MsgName(Attacker.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        public static Int32 GetDamagePlayer2Monster(Player Attacker, Monster Target, UInt16 MagicType, Byte MagicLevel)
        {
            Magic.Info Info = new Magic.Info();
            Database.AllMagics.TryGetValue((MagicType * 10) + MagicLevel, out Info);

            Double Damage = 0;

            if (Info.Type == 1115 || (Info.WeaponSubtype != 0 && Info.WeaponSubtype != 500))
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);

                Damage -= Target.Defence;
            }
            else if (Info.WeaponSubtype == 500)
            {
                Damage = MyMath.Generate(Attacker.MinAtk, Attacker.MaxAtk);
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);
            }
            else
            {
                Damage = Attacker.MagicAtk;
                if (Info.Power > 30000)
                    Damage *= (Double)(Info.Power - 30000) / 100;
                else
                    Damage += Info.Power;
                Damage = AdjustDamagePlayer2Monster(Damage, Attacker, Target);

                Damage *= ((Double)(100 - Target.MagicDef) / 100);
                Damage -= Target.MagicBlock;
                Damage *= 0.75;
            }

            if (Damage < 1)
                Damage = 1;

            Damage = AdjustMinDamagePlayer2Monster(Damage, Attacker, Target);

            if (Attacker.LuckyTime > 0 && MyMath.Success(10))
            {
                Damage *= 2;
                World.BroadcastRoomMsg(Attacker, new MsgName(Attacker.UniqId, "LuckyGuy", Network.MsgName.NameAct.RoleEffect), true);
            }
            return (Int32)Math.Round(Damage, 0);
        }

        private static Int32 AdjustMinDamagePlayer2Monster(Double Damage, Player Attacker, Monster Target)
        {
            Int32 MinDmg = 1;
            MinDmg += (Int32)(Attacker.Level / 10);

            Item Item = Attacker.GetItemByPos(4);
            if (Item != null)
                MinDmg += Item.Type % 10;

            return Math.Max(MinDmg, (Int32)Damage);
        }

        private static Int32 AdjustDamagePlayer2Monster(Double Damage, Player Attacker, Monster Target)
        {
            if (!Target.IsGreen(Attacker))
                return Math.Max(0, (Int32)Damage);

            Int32 DeltaLvl = Attacker.Level - Target.Level;
            if (DeltaLvl >= 3 && DeltaLvl <= 5)
                Damage *= 1.5;
            else if (DeltaLvl > 5 && DeltaLvl <= 10)
                Damage *= 2;
            else if (DeltaLvl > 10 && DeltaLvl <= 20)
                Damage *= 2.5;
            else if (DeltaLvl > 20)
                Damage *= 3;
            else
                Damage *= 1;

            return Math.Max(0, (Int32)Damage);
        }
    }
}
