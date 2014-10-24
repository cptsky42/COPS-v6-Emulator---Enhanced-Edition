// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;

namespace COServer.Entities
{
    public class AdvancedEntity : Entity
    {
        public enum Flag : long
        {
            None = 0x00,
            Flashing = 0x01,
            Poisoned = 0x02,
            //Invisible = 0x04,
            //Unknow = 0x08
            XPList = 0x10,
            Frozen = 0x20,
            TeamLeader = 0x40,
            Accuracy = 0x80,
            Shield = 0x100,
            Stigma = 0x200,
            Die = 0x400,
            //FadeOut = 0x800,
            AzureShield = 0x1000,
            //0x2000 -> None
            RedName = 0x4000,
            BlackName = 0x8000,
            //0x10000 -> None
            //0x20000 -> None
            SuperMan = 0x40000,
            ThirdMetempsychosis = 0x80000,
            FourthMetempsychosis = 0x100000,
            FifthMetempsychosis = 0x200000,
            Invisibility = 0x400000,
            Cyclone = 0x800000,
            SixthMetempsychosis = 0x1000000,
            SeventhMetempsychosis = 0x2000000,
            EighthMetempsychosis = 0x4000000,
            Flying = 0x8000000,
            NinthMetempsychosis = 0x10000000,
            TenthMetempsychosis = 0x20000000,
            CastingPray = 0x40000000,
            Praying = 0x80000000,
        };

        public String Name;
        public Int64 Flags;
        public Int32 CurHP;
        public Int32 MaxHP;
        public Int16 Level;
        public Int16 Action;
        public Int16 PrevMap;
        public UInt16 PrevX;
        public UInt16 PrevY;

        public Int32 MinAtk;
        public Int32 MaxAtk;
        public Int32 Defence;
        public Int32 MagicAtk;
        public Int32 MagicDef;
        public Int32 MagicBlock;
        public Int32 Dexterity;
        public Int32 Dodge;
        public Int32 Weight;
        public Int32 AtkRange;
        public Int32 AtkSpeed;
        public Double Bless;
        public Double GemBonus;

        public Boolean IsInBattle;
        public Int32 TargetUID;
        public Int32 AtkType;
        public Int16 MagicType;
        public Byte MagicLevel;
        public Boolean MagicIntone;
        public Int32 AtkSpeedT;

        public Int32 AccuracyEndTime;
        public Int32 StarOfAccuracyEndTime;
        public Int32 ShieldEndTime;
        public Int32 MagicShieldEndTime;
        public Int32 AzureShieldEndTime;
        public Int32 StigmaEndTime;
        public Int32 InvisibilityEndTime;
        public Int32 SupermanEndTime;
        public Int32 CycloneEndTime;
        public Int32 FlyEndTime;
        public Int32 DodgeEndTime;

        public Double DexterityBonus;
        public Double DefenceBonus;
        public Double AttackBonus;
        public Double DodgeBonus;
        public Double SpeedBonus;
        public Int32 DefenceAddBonus;

        public AdvancedEntity(Int32 UniqId)
            : base(UniqId)
        {
            this.Action = 100;

            this.AccuracyEndTime = 0;
            this.StarOfAccuracyEndTime = 0;
            this.ShieldEndTime = 0;
            this.MagicShieldEndTime = 0;
            this.StigmaEndTime = 0;
            this.InvisibilityEndTime = 0;
            this.SupermanEndTime = 0;
            this.CycloneEndTime = 0;
            this.FlyEndTime = 0;
            this.DodgeEndTime = 0;

            this.DexterityBonus = 1;
            this.DefenceBonus = 1;
            this.AttackBonus = 1;
            this.DodgeBonus = 1;
            this.SpeedBonus = 1;
        }

        public Boolean IsAlive() { return !ContainsFlag(Player.Flag.Die); }

        public Boolean ContainsFlag(Int64 Flag) { return (Flags & Flag) != 0; }
        public void AddFlag(Int64 Flag) { Flags |= Flag; }
        public void DelFlag(Int64 Flag) { Flags &= ~Flag; }

        public Boolean ContainsFlag(Flag Flag) { return (Flags & (Int64)Flag) != 0; }
        public void AddFlag(Flag Flag) { Flags |= (Int64)Flag; }
        public void DelFlag(Flag Flag) { Flags &= ~(Int64)Flag; }
    }
}
