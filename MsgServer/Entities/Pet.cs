// * Created by Jean-Philippe Boivin
// * Copyright © 2010, 2014
// * COPS v6 Emulator

using System;

namespace COServer.Entities
{
    public partial class Pet : Monster
    {
        public Player Owner = null;

        public Pet(Int32 UniqId, MonsterInfo Info, Player Owner)
            : base(UniqId, Info, null)
        {
            this.Owner = Owner;
            this.Brain = new PetAI(this, 500, MoveSpeed, AtkSpeed, ViewRange, 5, AtkRange);
        }
    }
}
