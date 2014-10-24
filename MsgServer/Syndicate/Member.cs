// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using COServer.Entities;

namespace COServer
{
    public partial class Syndicate
    {
        public class Member
        {
            public Int32 UniqId;
            public String Name;
            public Byte Level;
            public Byte Rank;
            public Int32 Donation;

            public Member(Int32 UniqId, String Name, Byte Level, Byte Rank, Int32 Donation)
            {
                this.UniqId = UniqId;
                this.Name = Name;
                this.Level = Level;
                this.Rank = Rank;
                this.Donation = Donation;
            }
        }
    }
}
