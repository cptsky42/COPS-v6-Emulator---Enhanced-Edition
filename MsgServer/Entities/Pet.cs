// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer.Entities
{
    public partial class Pet : Monster
    {
        public Player Owner = null;
        public new Pet.AI Brain = null;

        public Pet(Int32 UniqId, MonsterInfo Info, Player Owner)
            : base(UniqId, Info, -1)
        {
            this.Owner = Owner;
            this.Brain = new Pet.AI(this, 500, MoveSpeed, AtkSpeed, ViewRange, 5, AtkRange);
        }


    }
}
