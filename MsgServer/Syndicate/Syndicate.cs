// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.IO;

namespace COServer
{
    public partial class Syndicate
    {
        enum Rank
        {
            Member = 50,

            Dir_en_Stage = 60,
            Sous_Directeur = 70,
            Dir_de_Filliade = 80,

            Sous_Chef = 90,
            GuildLeader = 100,
        }

        public static void Delete(Int16 UniqId)
        {
            if (File.Exists(Program.RootPath + "\\Syndicates\\" + UniqId.ToString() + ".syn"))
                File.Delete(Program.RootPath + "\\Syndicates\\" + UniqId.ToString() + ".syn");
        }
    }
}
