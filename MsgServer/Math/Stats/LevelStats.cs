// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;

namespace COServer
{
    public partial class MyMath
    {
        public static void GetLevelStats(Player Player)
        {
            Byte Lvl = (Byte)Player.Level;
            Byte Job = Player.Profession;

            if (Player.Level > 120)
                Lvl = 120;

            if (Player.Profession > 129 && Player.Profession < 136)
                Job = (Byte)(Player.Profession + 60);
            else if (Player.Profession > 139 && Player.Profession < 146)
                Job = (Byte)(Player.Profession + 50);
            else if (Player.Profession > 99 && Player.Profession < 116)
                Job = (Byte)(Player.Profession + 90);

            Job = (Byte)(Job - (Job % 10));

            if (Player.AutoAllot)
            {
                Player.Strength = (UInt16)Database.AllPointAllot[Job, Lvl][0];
                Player.Agility = (UInt16)Database.AllPointAllot[Job, Lvl][1];
                Player.Vitality = (UInt16)Database.AllPointAllot[Job, Lvl][2];
                Player.Spirit = (UInt16)Database.AllPointAllot[Job, Lvl][3];

                if (Player.Level > 120)
                    Player.AddPoints = (UInt16)((Player.Level - 120) * 3);
            }
            else
            {
                Player.Strength = (UInt16)Database.AllPointAllot[Job, 1][0];
                Player.Agility = (UInt16)Database.AllPointAllot[Job, 1][1];
                Player.Vitality = (UInt16)Database.AllPointAllot[Job, 1][2];
                Player.Spirit = (UInt16)Database.AllPointAllot[Job, 1][3];

                Player.AddPoints = (UInt16)((Player.Level - 1) * 3);
            }

            if (Player.Metempsychosis > 0)
                Player.AddPoints += 30;

            if (Player.FirstLevel == 121 || (Player.FirstLevel == 112 && Player.FirstProfession == 135))
                Player.AddPoints += 1;
            else if (Player.FirstLevel == 122 || (Player.FirstLevel == 114 && Player.FirstProfession == 135))
                Player.AddPoints += 3;
            else if (Player.FirstLevel == 123 || (Player.FirstLevel == 116 && Player.FirstProfession == 135))
                Player.AddPoints += 6;
            else if (Player.FirstLevel == 124 || (Player.FirstLevel == 118 && Player.FirstProfession == 135))
                Player.AddPoints += 10;
            else if (Player.FirstLevel == 125 || (Player.FirstLevel == 120 && Player.FirstProfession == 135))
                Player.AddPoints += 15;
            else if (Player.FirstLevel == 126 || (Player.FirstLevel == 122 && Player.FirstProfession == 135))
                Player.AddPoints += 21;
            else if (Player.FirstLevel == 127 || (Player.FirstLevel == 124 && Player.FirstProfession == 135))
                Player.AddPoints += 28;
            else if (Player.FirstLevel == 128 || (Player.FirstLevel == 126 && Player.FirstProfession == 135))
                Player.AddPoints += 36;
            else if (Player.FirstLevel == 129 || (Player.FirstLevel == 128 && Player.FirstProfession == 135))
                Player.AddPoints += 45;
            else if (Player.FirstLevel == 130 || (Player.FirstLevel == 130 && Player.FirstProfession == 135))
                Player.AddPoints += 55;
            else if (Player.FirstLevel == 131)
                Player.AddPoints += 60;
            else if (Player.FirstLevel == 132)
                Player.AddPoints += 65;
            else if (Player.FirstLevel == 133)
                Player.AddPoints += 70;
            else if (Player.FirstLevel == 134)
                Player.AddPoints += 75;
            else if (Player.FirstLevel == 135)
                Player.AddPoints += 80;
            else if (Player.FirstLevel == 136)
                Player.AddPoints += 85;
            else if (Player.FirstLevel == 137)
                Player.AddPoints += 90;
            else if (Player.FirstLevel == 138)
                Player.AddPoints += 95;
            else if (Player.FirstLevel == 139)
                Player.AddPoints += 100;
            else if (Player.FirstLevel == 140)
                Player.AddPoints += 105;

            if (Player.SecondLevel == 121)
                Player.AddPoints += 1;
            else if (Player.SecondLevel == 122)
                Player.AddPoints += 3;
            else if (Player.SecondLevel == 123)
                Player.AddPoints += 6;
            else if (Player.SecondLevel == 124)
                Player.AddPoints += 10;
            else if (Player.SecondLevel == 125)
                Player.AddPoints += 15;
            else if (Player.SecondLevel == 126)
                Player.AddPoints += 21;
            else if (Player.SecondLevel == 127)
                Player.AddPoints += 28;
            else if (Player.SecondLevel == 128)
                Player.AddPoints += 36;
            else if (Player.SecondLevel == 129)
                Player.AddPoints += 45;
            else if (Player.SecondLevel == 130)
                Player.AddPoints += 55;
            else if (Player.SecondLevel == 131)
                Player.AddPoints += 60;
            else if (Player.SecondLevel == 132)
                Player.AddPoints += 65;
            else if (Player.SecondLevel == 133)
                Player.AddPoints += 70;
            else if (Player.SecondLevel == 134)
                Player.AddPoints += 75;
            else if (Player.SecondLevel == 135)
                Player.AddPoints += 80;
            else if (Player.SecondLevel == 136)
                Player.AddPoints += 85;
            else if (Player.SecondLevel == 137)
                Player.AddPoints += 90;
            else if (Player.SecondLevel == 138)
                Player.AddPoints += 95;
            else if (Player.SecondLevel == 139)
                Player.AddPoints += 100;
            else if (Player.SecondLevel == 140)
                Player.AddPoints += 105;

            MyMath.GetHitPoints(Player, true);
            MyMath.GetMagicPoints(Player, true);
            MyMath.GetEquipStats(Player);
        }

        public static Int16[] GetLevelStats(Byte Level, Byte Profession)
        {
            Byte Lvl = Level;
            Byte Job = Profession;

            if (Level > 120)
                Lvl = 120;

            if (Profession > 129 && Profession < 136)
                Job = (Byte)(Profession + 60);
            else if (Profession > 139 && Profession < 146)
                Job = (Byte)(Profession + 50);
            else if (Profession > 99 && Profession < 116)
                Job = (Byte)(Profession + 90);

            Job = (Byte)(Job - (Job % 10));

            try { return Database.AllPointAllot[Job, Lvl]; }
            catch { return null; }
        }
    }
}
