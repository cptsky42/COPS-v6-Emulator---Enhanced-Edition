// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;
using System.Timers;
using COServer.Network;

namespace COServer.Entities
{
    public class Player : AdvancedEntity, IDisposable
    {
        public const UInt32 _MAX_MONEYLIMIT = 1000000000;

        private String mMate = "None";
        private UInt16 mHair = 0;

        private bool mAutoAllot = true;
        private Byte mMetempsychosis = 0;

        public Client Client;
        public Screen Screen;

        /// <summary>
        /// The mate of the player.
        /// </summary>
        public String Mate
        {
            get { return mMate; }
            set
            {
                mMate = value;
                Database.UpdateField(this, "mate", mMate);

                var msg = new MsgName(UniqId, mMate, MsgName.NameAct.Spouse);
                Send(msg);
            }
        }

        private const UInt32 MASK_CHANGELOOK = 10000000U;

        /// <summary>
        /// The look of the player.
        /// </summary>
        public override UInt32 Look
        {
            get
            {
                if (IsGhost())
                {
                    if (Sex == Sex.Man)
                        return mLook + (MASK_CHANGELOOK * 98U);
                    else
                        return mLook + (MASK_CHANGELOOK * 99U);
                }
                else if (QueryTransformation().HasValue)
                {
                    return mLook + (MASK_CHANGELOOK * QueryTransformation().Value.Look);
                }
                else
                    return mLook;
            }
            set
            {
                mLook = value;
                Database.UpdateField(this, "lookface", mLook);

                var msg = new MsgUserAttrib(this, mLook, MsgUserAttrib.AttributeType.Look);
                World.BroadcastRoomMsg(this, msg, true);
            }
        }

        /// <summary>
        /// Whether or not the player is a ghost.
        /// </summary>
        private bool mIsGhost = false;

        /// <summary>
        /// Whether or not the player is a ghost.
        /// </summary>
        public bool IsGhost() { return mIsGhost; }

        /// <summary>
        /// Transform the player to a ghost.
        /// </summary>
        public void TransformGhost()
        {
            mIsGhost = true;

            var msg = new MsgUserAttrib(this, Look, MsgUserAttrib.AttributeType.Look);
            World.BroadcastRoomMsg(this, msg, true);
        }

        private MonsterInfo? mTransformation = null;

        public MonsterInfo? QueryTransformation()
        {
            return mTransformation;
        }

        /// <summary>
        /// The hair of the player.
        /// </summary>
        public UInt16 Hair
        {
            get { return mHair; }
            set
            {
                mHair = value;
                Database.UpdateField(this, "hair", mHair);

                var msg = new MsgUserAttrib(this, mHair, MsgUserAttrib.AttributeType.Hair);
                World.BroadcastRoomMsg(this, msg, true);
            }
        }

        /// <summary>
        /// The sex of the player.
        /// </summary>
        public Sex Sex
        {
            get { return (mLook / 1000U) % 2 == 0 ? Sex.Woman : Sex.Man; }
        }

        public Byte Profession;
        public UInt32 Exp;
        public UInt16 Strength;
        public UInt16 Agility;
        public UInt16 Vitality;
        public UInt16 Spirit;
        public UInt16 AddPoints;
        public UInt32 Money;
        public UInt16 CurMP;
        public UInt16 MaxMP;

        /// <summary>
        /// The metempsychosis of the player.
        /// </summary>
        public Byte Metempsychosis
        {
            get { return mMetempsychosis; }
            set
            {
                mMetempsychosis = value;
                Database.UpdateField(this, "metempsychosis", mMetempsychosis);

                if (mMetempsychosis > 0)
                    mAutoAllot = false;

                var msg = new MsgUserAttrib(this, mMetempsychosis, MsgUserAttrib.AttributeType.Metempsychosis);
                World.BroadcastRoomMsg(this, msg, true);
            }
        }

        public Int32 VPs;
        public Int16 PkPoints;
        public PkMode PkMode;
        public Byte Energy;
        public Byte XP;

        public UInt32 WHMoney;
        /// <summary>
        /// The PIN used to lock the account / depot.
        /// </summary>
        internal UInt32 mLockPin = 0;

        public Int32 KO;
        public Int32 CurKO;
        public Int32 TimeAdd;

        public GameMap PrevMap;
        public UInt16 PrevX;
        public UInt16 PrevY;

        public Byte FirstLevel;
        public Byte SecondLevel;
        public Byte FirstProfession;
        public Byte SecondProfession;

        /// <summary>
        /// Determine whether or not the stats points of the player
        /// are automatically allocated.
        /// </summary>
        public bool AutoAllot { get { return mAutoAllot; } }

        public Double ExpBonus;
        public Double MagicBonus;
        public Double WeaponSkillBonus;

        public Int32 LastXPAdd;
        public Int32 LastPkPointRemove;
        public Int32 LastCoolShow;
        public Int32 LastSave;
        public Int32 LastJumpTick;
        public Int32 LastPickSwingTick;
        public Int32 LastLeaderPosTick;
        public Int32 LastAttackTick;
        public Int32 LastClientTick;
        public Int32 LastRcvClientTick;
        public Int32 LastReqClientTick;
        public Int32 LastDieTick;

        public Int32 DblExpEndTime;
        public Int32 TransformEndTime;

        public Dictionary<Int32, Item> Items;

        /// <summary>
        /// Weapon skills of the player.
        /// </summary>
        public List<WeaponSkill> WeaponSkills
        {
            get
            {
                lock (mWeaponSkills)
                    return new List<WeaponSkill>(mWeaponSkills.Values);
            }
        }

        /// <summary>
        /// Magic skills of the player.
        /// </summary>
        public List<Magic> Magics
        {
            get
            {
                lock (mMagics)
                    return new List<Magic>(mMagics.Values);
            }
        }

        public Dictionary<Int32, String> Friends;
        public Dictionary<Int32, String> Enemies;
		public Team Team;
        public Syndicate Syndicate;

        public Pet Pet = null;

        public Deal Deal;
        public Booth Booth;

        public Int32 TradeRequest;
        public Int32 FriendRequest;
        public Int32 GameRequest;

        public Boolean Mining;

        public Int32 PrevSpeedHackReset = Environment.TickCount;
        public Int32 SpeedHack = 0;

        public Int32 JailC;

        public const Int32 MAX_LUCKY_TIME = 2 * 60 * 60 * 1000;
        public Int32 LuckyTime = 0;
        public Boolean Praying;
        public Boolean CastingPray;
        public UInt32 PrayMap;
        public UInt16 PrayX;
        public UInt16 PrayY;

        public readonly List<Byte> UserTasks = new List<Byte>();
        public readonly Dictionary<Tuple<int, int>, int> UserStats = new Dictionary<Tuple<int, int>, int>();

        /// <summary>
        /// Determine whether or not the player is a game master.
        /// </summary>
        public bool IsGM { get { return Name.EndsWith("[GM]"); } }
        /// <summary>
        /// Determine whether or not the player is a project master.
        /// </summary>
        public bool IsPM { get { return Name.EndsWith("[PM]"); } }

        /// <summary>
        /// Weapon skills of the player.
        /// </summary>
        private Dictionary<UInt32, WeaponSkill> mWeaponSkills = new Dictionary<UInt32, WeaponSkill>();
        /// <summary>
        /// Magic skills of the player.
        /// </summary>
        private Dictionary<UInt32, Magic> mMagics = new Dictionary<UInt32, Magic>();

        /// <summary>
        /// Registers used by Lua tasks.
        /// 
        /// First eight registers are integers, last
        /// eight registers are strings.
        /// </summary>
        private object[] mRegisters = new object[16];


        /// <summary>
        /// Last messages count.
        /// </summary>
        private UInt32 mMsgCount = 0;
        /// <summary>
        /// Tick of first client response.
        /// </summary>
        private Int32 mFirstClientTick = 0;
        /// <summary>
        /// Tick of latest client response.
        /// </summary>
        private Int32 mLastClientTick = 0;
        /// <summary>
        /// The latest receive of the client tick.
        /// </summary>
        private DateTime mLastRcvClientTick = DateTime.MinValue;
        /// <summary>
        /// The tick of first server request.
        /// </summary>
        private DateTime mFirstServerTick = DateTime.MinValue;
        /// <summary>
        /// Tick of the latest server request.
        /// </summary>
        private DateTime mLastServerTick = DateTime.MinValue;
        /// <summary>
        /// The server ticks.
        /// </summary>
        private LinkedList<DateTime> mServerTicks = new LinkedList<DateTime>();

        /// <summary>
        /// Periodic timer of the player.
        /// </summary>
        private Timer mTimer;

        /// <summary>
        /// Indicate whether or not the object is disposed.
        /// </summary>
        private bool mIsDisposed = false;

        public Player(Int32 UniqId, Client aClient)
            : base(UniqId)
        {
            this.LastXPAdd = Environment.TickCount;
            this.LastPkPointRemove = Environment.TickCount;
            this.LastCoolShow = Environment.TickCount;
            this.LastSave = Environment.TickCount + 30000;
            this.LastClientTick = 0;
            this.LastRcvClientTick = Environment.TickCount;
            this.LastReqClientTick = Environment.TickCount;
            this.LastJumpTick = Environment.TickCount;
            this.LastPickSwingTick = Environment.TickCount;
            this.LastAttackTick = Environment.TickCount;
            this.LastDieTick = Environment.TickCount;

            this.DblExpEndTime = 0;
            this.TransformEndTime = 0;

            this.Client = aClient;
            this.Screen = new Screen(this);

            this.Items = new Dictionary<Int32, Item>();
            this.Friends = new Dictionary<Int32, String>();
            this.Enemies = new Dictionary<Int32, String>();

            mTimer = new Timer();
            mTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            mTimer.Interval = 500; // 500 ms
            mTimer.Start();
        }

        ~Player()
        {
            Client = null;
            Screen = null;

            Items = null;
            mWeaponSkills = null;
            mMagics = null;
            Friends = null;
            Enemies = null;
			Team = null;
            Syndicate = null;
            Pet = null;

            Deal = null;
            Booth = null;

            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        protected virtual void Dispose(bool aDisposing)
        {
            if (!mIsDisposed)
            {
                if (aDisposing)
                {
                    if (mTimer != null)
                        mTimer.Dispose();
                }

                mTimer = null;

                mIsDisposed = true;
            }
        }

        /// <summary>
        /// Check if the specified PIN match.
        /// </summary>
        /// <param name="aPin">The PIN to validate.</param>
        /// <returns>True if the specified PIN match, false otherwise.</returns>
        public bool CheckLockPin(UInt32 aPin)
        {
            if (mLockPin == 0)
                return true;

            return aPin == mLockPin;
        }

        /// <summary>
        /// Set the PIN used to lock the account / depot.
        /// </summary>
        /// <param name="aPin">The new PIN of the account.</param>
        /// <returns>True on success, false otherwise.</returns>
        public bool SetLockPin(UInt32 aPin)
        {
            mLockPin = aPin;
            return Database.UpdateField(this, "depot_pin", mLockPin);
        }

        /// <summary>
        /// Get the data of the specified register.
        /// </summary>
        /// <param name="aId">The ID of the register.</param>
        /// <returns>The data of the specified register, or null.</returns>
        public object GetRegData(byte aId)
        {
            if (aId > mRegisters.Length)
                return null;

            return mRegisters[aId];
        }

        /// <summary>
        /// Set the data of the specified register.
        /// </summary>
        /// <param name="aId">The ID of the register.</param>
        /// <param name="aData">The data to assign to the register.</param>
        /// <returns>True if the assignation succeeded, false otherwise.</returns>
        public bool SetRegData(byte aId, object aData)
        {
            if (aId > mRegisters.Length)
                return false;

            mRegisters[aId] = aData;
            return true;
        }

        /// <summary>
        /// Recalculate the maximum HP of the player.
        /// </summary>
        /// <returns>The maximum HP of the player.</returns>
        public UInt16 CalcMaxHP()
        {
            if (TransformEndTime != 0)
                return (UInt16)MaxHP;

            int life = ((Strength * 3) + (Agility * 3) + (Vitality * 24) + (Spirit * 3));

            switch (Profession)
            {
                case 11:
                    life = (int)(life * 1.05);
                    break;
                case 12:
                    life = (int)(life * 1.08);
                    break;
                case 13:
                    life = (int)(life * 1.10);
                    break;
                case 14:
                    life = (int)(life * 1.12);
                    break;
                case 15:
                    life = (int)(life * 1.15);
                    break;
            }

            lock (Items)
            {
                foreach (Item item in Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                    {
                        life += item.Enchant;
                        life += item.Life;

                        ItemAddition bonus;
                        if (Database.AllBonus.TryGetValue(ItemHandler.GetBonusId(item.Type, item.Craft), out bonus))
                            life += (UInt16)bonus.Life;
                    }
                }
            }

            life = Math.Max(0, life);

            MaxHP = (UInt16)life;
            return (UInt16)life;
        }

        /// <summary>
        /// Recalculate the maximum MP of the player.
        /// </summary>
        /// <returns>The maximum MP of the player.</returns>
        public UInt16 CalcMaxMP()
        {
            int mana = (Spirit * 5);

            if (Profession > 100 && Profession < 200)
                mana += (int)(mana * (Profession % 10));

            lock (Items)
            {
                foreach (Item item in Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                        mana += item.Mana;
                }
            }

            mana = Math.Max(0, mana);

            MaxMP = (UInt16)mana;
            return (UInt16)mana;
        }

        /// <summary>
        /// Get the effect of all gems on the attack of the player.
        /// </summary>
        /// <returns>The effect of all gems on the attack of the player.</returns>
        public double GetGemAtkEffect()
        {
            double effect = 1.0;

            lock (Items)
            {
                foreach (Item item in Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                        effect += item.GetGemDmgEffect();
                }
            }

            return effect;
        }

        /// <summary>
        /// Get the effect of all gems on the magic attack of the player.
        /// </summary>
        /// <returns>The effect of all gems on the magic attack of the player.</returns>
        public double GetGemMAtkEffect()
        {
            double effect = 1.0;

            lock (Items)
            {
                foreach (Item item in Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                        effect += item.GetGemMgcAtkEffect();
                }
            }

            return effect;
        }

        public void Die(AdvancedEntity aKiller)
        {
            ////////////////////////////////////////////////////////////
            // TODO cleanup
            CurHP = 0;
            Send(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
            if (Team != null)
                Team.BroadcastMsg(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));

            if (TransformEndTime != 0)
            {
                CalcMaxHP();
                CalcMaxMP();
            }

            TransformEndTime = 0;
            Mining = false;
            ////////////////////////////////////////////////////////////


            bool dropItem = Profession % 10 > 0;
            bool dropExp = Profession % 10 > 0;

            //    ClrAttackTarget();
            //    if (QueryMagic())
            //        QueryMagic()->AbortMagic(true);
            //    CallBackAllEudemon();
            //    DetachEudemon();

            ResetStatuses();
            AttachStatus(Status.Die);
            AttachStatus(Status.Freeze);

            mIsGhost = false;
            TransformGhost();

            //Block - Revive Hack
            LastDieTick = Environment.TickCount;

            //    OBJID	idRebornMap = ID_NONE;
            //    POINT	pos;
            //    if (this->GetLev() <= MAX_RETURN_BORN_POS_LEV)
            //    {
            //        CGameMap* pMap = MapManager()->GetGameMap(DEFAULT_LOGIN_MAPID);
            //        IF_OK (pMap)
            //            this->SetRecordPos(pMap->GetID(), DEFAULT_LOGIN_POSX, DEFAULT_LOGIN_POSY);
            //    }
            //    else
            //    {
            //        IF_OK(GetMap()->GetRebornMap(&idRebornMap, &pos))
            //            this->SetRecordPos(idRebornMap, pos.x, pos.y);
            //    }
            //    KillCallPet();

            if (Map.IsPkField() || Map.IsSynMap())
            {
                //            // drop item
                //            {
                //                int nChance = 30;
                //                if(!pMap->IsDeadIsland() && bDropItem) // ËÀÍöµº²»µôÎïÆ·
                //                    m_pPackage->RandDropItem(_MAP_PKFIELD, nChance);

                //            }

                //            // fly out
                //            /*if (pMap->IsPkField())
                //            {
                //                if (idRebornMap != ID_NONE)
                //                    this->FlyMap(idRebornMap, pos.x, pos.y);
                //            }
                //            */

                //            // set reborn pos to DoubleDragon city if not in syn war but in syn map
                //            if (pMap->IsSynMap() && !pMap->IsWarTime())
                //            {
                //                CONST OBJID ID_DDCITY	= 1002;
                //                CONST POINT POS_REBORN	= { 438, 398 };
                //                this->SetRecordPos(ID_DDCITY, POS_REBORN.x, POS_REBORN.y);
                //            }

                return;
            }
            else if (Map.IsPrisonMap())
            {
                //            // drop item
                //            {
                //                if(!pMap->IsDeadIsland() && bDropItem) // ËÀÍöµº²»µôÎïÆ·
                //                {
                //                    int nChance = __min(90, 20+this->GetPk()/2);
                //                    m_pPackage->RandDropItem(_MAP_PRISON, nChance);
                //                }

                //            }

                return;
            }

            if (aKiller == null)
                return;

            if (aKiller.IsPlayer() && true)// && !Map.IsDeadIsland())
            {
                Player killer = aKiller as Player;

                var syn = Syndicate;
                var synKiller = killer.Syndicate;

                if (syn != null && synKiller != null && (syn.IsHostile(synKiller.Id) || synKiller.IsHostile(syn.Id)))
                {
                    //       this->QuerySynAttr()->DecProffer(PROFFER_BEKILL);
                }

                if (!IsCriminal() && !IsRedName())
                {
                    if (killer.Enemies.ContainsKey(UniqId))
                        killer.PkPoints += 5;
                    else if (killer.Syndicate != null && Syndicate != null
                        && killer.Syndicate.IsHostile(Syndicate.Id))
                        killer.PkPoints += 3;
                    else
                        killer.PkPoints += 10;

                    if (killer.PkPoints > 30000)
                        killer.PkPoints = 30000;

                    killer.Send(new MsgUserAttrib(killer, killer.PkPoints, MsgUserAttrib.AttributeType.PkPoints));

                    if (killer.PkPoints >= 30 && killer.PkPoints < 100)
                    {
                        if (!killer.IsRedName())
                            killer.AttachStatus(Status.RedName);
                    }
                    else if (killer.PkPoints >= 100)
                    {
                        if (!killer.IsBlackName())
                        {
                            killer.DetachStatus(Status.RedName);
                            killer.AttachStatus(Status.BlackName);
                        }
                    }
                }

                if (!IsBlueName())
                {
                    if (!Enemies.ContainsKey(killer.UniqId))
                    {
                        Enemies.Add(killer.UniqId, killer.Name);
                        Send(new MsgFriend(killer.UniqId, killer.Name, MsgFriend.Status.Online, MsgFriend.Action.EnemyAdd));
                    }
                }

                {
                    UInt32 droppedMoney = 0;
                    if (PkPoints < 30) // TODO constant
                        droppedMoney = (UInt32)((Money * (MyMath.Generate(0, 40) + 10)) / 100);
                    else if (PkPoints < 100) // TODO constant
                        droppedMoney = (UInt32)((Money * (MyMath.Generate(0, 50) + 50)) / 100);
                    else
                        droppedMoney = Money;

                    if (droppedMoney > 0)
                    {
                        Item money = null;
                        if (droppedMoney <= 10) //Silver
                            money = Item.CreateMoney(1090000);
                        else if (droppedMoney <= 100) //Sycee
                            money = Item.CreateMoney(1090010);
                        else if (droppedMoney <= 1000) //Gold
                            money = Item.CreateMoney(1090020);
                        else if (droppedMoney <= 2000) //GoldBullion
                            money = Item.CreateMoney(1091000);
                        else if (droppedMoney <= 5000) //GoldBar
                            money = Item.CreateMoney(1091010);
                        else if (droppedMoney > 5000) //GoldBars
                            money = Item.CreateMoney(1091020);
                        else //Error
                            money = Item.CreateMoney(1090000);

                            UInt16 posX = X, posY = Y;
                            if (Map.FindFloorItemCell(ref posX, ref posY, MyMath.USERDROP_RANGE))
                            {
                                Money -= droppedMoney;
                                Send(new MsgUserAttrib(this, Money, MsgUserAttrib.AttributeType.Money));

                                FloorItem floorItem = new FloorItem(money, droppedMoney, 0, Map, posX, posY);
                                World.FloorThread.AddToQueue(floorItem);
                            }
                    }
                }

                if (dropItem)
                {
                    int chance = 0;
                    if (PkPoints < 30) // TODO constant
                        chance = 10 + MyMath.Generate(0, 40);
                    else if (PkPoints < 100) // TODO constant
                        chance = 50 + MyMath.Generate(0, 50);
                    else
                        chance = 100;

                    int count = (ItemInInventory() * chance) / 100;
                    for (int i = 0; i < count; ++i) // TODO refact this dangerous code
                    {
                        Item[] items = new Item[Items.Count];
                        Items.Values.CopyTo(items, 0);

                        do
                        {
                            int pos = MyMath.Generate(0, items.Length - 1);
                            Item item = items[pos];

                            if (item.Position != 0)
                                continue;

                            UInt16 posX = X, posY = Y;
                            if (Map.FindFloorItemCell(ref posX, ref posY, MyMath.USERDROP_RANGE))
                            {
                                DelItem(item, true);

                                FloorItem floorItem = new FloorItem(item, 0, 0, Map, posX, posY);
                                World.FloorThread.AddToQueue(floorItem);
                            }
                            break;
                        }
                        while (true);
                    }
                }

                if (dropItem)
                {
                    int count = 0;
                    if (PkPoints >= 30) // TODO constant
                        count = 1;

                    List<Byte> positions = new List<Byte>();

                    for (Byte i = 0; i < 10; ++i)
                    {
                        Item equip = GetItemByPos(i);
                        if (equip != null /* TODO && !equip.IsNeverDropWhenDead()*/)
                            positions.Add(i);
                    }

                    if (positions.Count > 0)
                    {
                        count = Math.Min(count, positions.Count);
                        for (int i = 0; i < count; ++i)
                        {
                            int index = MyMath.Generate(0, positions.Count - 1);
                            byte pos = positions[index];

                            Item equip = GetItemByPos(pos);
                            if (equip != null)
                            {
                                if (pos == 4) // TODO constant weapon right
                                {
                                    Item leftWeapon = GetItemByPos(5); // TODO constant weapon left
                                    if (leftWeapon != null)
                                        equip = leftWeapon;
                                }

                                UInt16 posX = X, posY = Y;
                                if (Map.FindFloorItemCell(ref posX, ref posY, MyMath.USERDROP_RANGE))
                                {
                                    Send(new MsgItem(equip.Id, equip.Position, MsgItem.Action.Unequip));
                                    DelItem(equip, true);

                                    FloorItem floorItem = new FloorItem(equip, 0, 0, Map, posX, posY);
                                    World.FloorThread.AddToQueue(floorItem);
                                }
                            }
                        }
                    }
                }

                if (dropExp)
                {
                    UInt32 lostExp = (UInt32)(Exp * 0.02);
                    if (IsRedName())
                        lostExp = (UInt32)(Exp * 0.10);
                    else if (IsBlackName())
                        lostExp = (UInt32)(Exp * 0.20);

                    if (Syndicate != null)
                    {
                        Syndicate.Member member = Syndicate.GetMemberInfo(UniqId);
                        if (member != null)
                        {
                            UInt32 compensation = 0;
                            if (member.Proffer > 0)
                            {
                                switch (member.Rank)
                                {
                                    case Syndicate.Rank.Leader:
                                        compensation = (UInt32)(lostExp * 0.80);
                                        break;
                                    case Syndicate.Rank.SubLeader:
                                        compensation = (UInt32)(lostExp * 0.60);
                                        break;
                                    case Syndicate.Rank.BranchMgr:
                                        compensation = (UInt32)(lostExp * 0.40);
                                        break;
                                    case Syndicate.Rank.DeputyMgr:
                                        compensation = (UInt32)(lostExp * 0.35);
                                        break;
                                    case Syndicate.Rank.InternMgr:
                                        compensation = (UInt32)(lostExp * 0.30);
                                        break;
                                    default:
                                        compensation = (UInt32)(lostExp * 0.25);
                                        break;
                                }
                            }
                            else
                                compensation = (UInt32)(lostExp * 0.20);

                            if (compensation > UInt32.MaxValue)
                                compensation = UInt32.MaxValue;

                            Syndicate.Money -= (UInt32)compensation;
                            member.Proffer -= (UInt32)compensation;
                            lostExp -= compensation;
                        }
                    }

                    if (lostExp > 0)
                    {
                        if (Exp < lostExp)
                            Exp = 0;
                        else
                            Exp -= lostExp;

                        Send(new MsgUserAttrib(this, Exp, MsgUserAttrib.AttributeType.Exp));
                        killer.AddExp(lostExp * 0.10, false);
                    }
                }

                if (IsBlackName())
                {
                    World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", String.Format(StrRes.STR_SENT_TO_JAIL, Name), Channel.GM, Color.Red));
                    Move(6000, 29, 72); // GotoJail();
                    // PoliceWanted().DelWanted(this->GetID());
                }
            }
            else if (aKiller.IsPlayer() && false) // Map.IsDeadIsland()
            {
                Player killer = aKiller as Player;

                if (!IsBlueName())
                {
                    if (!Enemies.ContainsKey(killer.UniqId))
                    {
                        Enemies.Add(killer.UniqId, killer.Name);
                        Send(new MsgFriend(killer.UniqId, killer.Name, MsgFriend.Status.Online, MsgFriend.Action.EnemyAdd));
                    }
                }
            }
            else if (aKiller.IsMonster())
            {
                Monster killer = aKiller as Monster;

                //        if (pRole->QueryObj(OBJ_MONSTER, IPP_OF(pMonster)))
                //        {
                //            CUser* pUser = pMonster->QueryOwnerUser();
                //            // ×Ô¼º²»ÊÇ´¦ÓÚÉÁÀ¶×´Ì¬²Å¼Ó³ðÈË
                //            if (pUser && !QueryStatus(STATUS_CRIME))
                //                QueryEnemy()->Add(pUser->GetID(), pUser->GetName(), SYNCHRO_TRUE, UPDATE_TRUE);
                //        }

                //        if (!pMap->IsDeadIsland())
                //        {
                //            // money
                //            DWORD dwMoneyLost = ::RandGet(this->GetMoney()/3);
                //            if (dwMoneyLost > 0)
                //                this->DropMoney(dwMoneyLost, this->GetPosX(), this->GetPosY());

                //            // item
                //            if (bDropItem)
                //            {
                //                int nChance = 33;
                //                if (this->GetLev() < 10)
                //                    nChance = 5;

                //                m_pPackage->RandDropItem(_MAP_NONE, nChance);
                //            }

                //            // guard
                //            if (pMonster && (pMonster->IsGuard() || pMonster->IsPkKiller()))
                //            {
                //                if (this->GetPk() >= PKVALUE_BLACKNAME)
                //                    this->GotoJail();
                //            }
                //        }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="aChangePos"></param>
        public void Reborn(Boolean aChangePos)
        {
            if (IsAlive())
                return;
            
            MyMath.GetEquipStats(this); // TODO To remove

            ResetStatuses();
            mIsGhost = false;

            XP = 0;
            Send(new MsgUserAttrib(this, XP, MsgUserAttrib.AttributeType.XP));
            LastXPAdd = Environment.TickCount;

//    m_tAutoHealMaxLife.Clear();

            CurHP = MaxHP;
            Send(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));

            //CurMP = MaxMP;
            //Send(new MsgUserAttrib(this, CurMP, MsgUserAttrib.AttributeType.Mana));

            World.BroadcastRoomMsg(this, new MsgUserAttrib(this, Look, MsgUserAttrib.AttributeType.Look), true);

            Energy = 100;
            Send(new MsgUserAttrib(this, Energy, MsgUserAttrib.AttributeType.Energy));

            Action = Emotion.StandBy;

            if (Team != null)
            {
                Team.BroadcastMsg(new MsgUserAttrib(this,
                    new Dictionary<MsgUserAttrib.AttributeType, UInt64>
                    { 
                        { MsgUserAttrib.AttributeType.Life, (UInt64)CurHP },
                        { MsgUserAttrib.AttributeType.MaxLife, (UInt64)MaxHP }
                    }));
            }

            if (aChangePos)
            {
                GameMap rebornMap = null;
                if (MapManager.TryGetMap(Map.RebornMap, out rebornMap))
                    Move(rebornMap.Id, rebornMap.PortalX, rebornMap.PortalY);
            }

//    // lock
//    CRole::AttachStatus(this->QueryRole(), STATUS_PK_PROTECT, 0, CHGMAP_LOCK_SECS);
////	m_tLock.Startup(CHGMAP_LOCK_SECS);

            World.BroadcastRoomMsg(this, new MsgAction(this, 0, MsgAction.Action.Reborn), true);
        }

        public UInt32 AddExp(Double Value, Boolean UseBonus)
        {
            if (!Database.AllLevExp.ContainsKey((Byte)Level))
                return 0;

            if (UseBonus)
            {
                if (DblExpEndTime > 0)
                    Value *= 2.00;

                if (LuckyTime > 0)
                    Value *= 1.10;

                if (Metempsychosis > 1)
                    Value /= Metempsychosis * 1.50;

                Value *= ExpBonus;
            }
            Exp += (UInt32)Value;

            Boolean Leveled = false;
            if (Exp > Database.AllLevExp[(Byte)Level])
                Leveled = true;

            while (Exp > Database.AllLevExp[(Byte)Level])
            {
                if (Level < 70 && Team != null)
                {
                    if (Team.Leader.UniqId != UniqId && Team.Leader.Level >= 70)
                    {
                        if (Team.Leader.Map == Map)
                        {
                            Int32 vps = (Int32)((Level * 17f / 13f * 12f / 2f) + Level * 3f);

                            Team.Leader.VPs += vps;
                            Team.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Team.Leader.Name + " a obtenu " + vps + " points de vertu.", Channel.Team, Color.White));
                        }
                    }
                }

                Exp -= Database.AllLevExp[(Byte)Level];
                Level++;

                if (Metempsychosis == 0 && Level <= 120)
                    MyMath.GetLevelStats(this);
                else
                    AddPoints += 3;

                if (!Database.AllLevExp.ContainsKey((Byte)Level))
                    break;
            }

            if (Leveled)
            {
                CalcMaxHP();
                CalcMaxMP();

                CurHP = MaxHP;
                if (Team != null)
                {
                    Team.BroadcastMsg(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
                    Team.BroadcastMsg(new MsgUserAttrib(this, MaxHP, (MsgUserAttrib.AttributeType.Life + 1)));
                }

                Send(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
                Send(new MsgUserAttrib(this, Strength, MsgUserAttrib.AttributeType.Strength));
                Send(new MsgUserAttrib(this, Agility, MsgUserAttrib.AttributeType.Agility));
                Send(new MsgUserAttrib(this, Vitality, MsgUserAttrib.AttributeType.Vitality));
                Send(new MsgUserAttrib(this, Spirit, MsgUserAttrib.AttributeType.Spirit));
                Send(new MsgUserAttrib(this, AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                World.BroadcastRoomMsg(this, new MsgUserAttrib(this, Level, MsgUserAttrib.AttributeType.Level), true);
                World.BroadcastRoomMsg(this, new MsgAction(this, 0, MsgAction.Action.UpLev), true);

                if (Syndicate != null)
                {
                    Syndicate.Member Member = Syndicate.GetMemberInfo(UniqId);
                    if (Member != null)
                        Member.Level = (Byte)Level;
                }
            }
            Send(new MsgUserAttrib(this, (Int64)Exp, MsgUserAttrib.AttributeType.Exp));
            return (UInt32)Value;
        }

        public void AddMagicExp(UInt16 Type, UInt32 Exp)
        {
            Magic Magic = GetMagicByType(Type);
            if (Magic == null)
                return;

            if (Magic.Unlearn)
            {
                Magic.Unlearn = false;
                Magic.Level = 0;
                Magic.Exp = 0;
                Send(new MsgMagicInfo(Magic));
            }

            Magic.Info Info;
            if (!Database.AllMagics.TryGetValue((Magic.Type * 10) + Magic.Level, out Info))
                return;

            if (!Database.AllMagics.ContainsKey((Magic.Type * 10) + Magic.Level + 1))
                return;

            if (Level < Info.ReqLevel)
                return;

            Exp = (UInt32)(Exp * MagicBonus); 

            if ((Magic.Exp + Exp >= Info.ReqExp) || 
                (Magic.Level >= (Byte)(Magic.OldLevel / 2) && Magic.Level < Magic.OldLevel))
            {
                Magic.Level++;
                Magic.Exp = 0;
                Send(new MsgMagicInfo(Magic));
                return;
            }

            Magic.Exp += Exp;
            Send(new MsgFlushExp(Magic));
        }

        public void AddWeaponSkillExp(UInt16 Type, UInt32 Exp)
        {
            WeaponSkill WeaponSkill = GetWeaponSkillByType(Type);
            if (WeaponSkill == null)
            {
                WeaponSkill = WeaponSkill.Create(this, Type, 0);
                AwardSkill(WeaponSkill, true);
            }

            if (WeaponSkill.Unlearn)
            {
                WeaponSkill.Unlearn = false;
                WeaponSkill.Level = 0;
                WeaponSkill.Exp = 0;
                Send(new MsgWeaponSkill(WeaponSkill));
            }

            if (WeaponSkill.Level >= Database.AllWeaponSkillExp.Count)
                return;

            Exp = (UInt32)(Exp * WeaponSkillBonus);

            if ((WeaponSkill.Exp + Exp >= Database.AllWeaponSkillExp[WeaponSkill.Level]) ||
                (WeaponSkill.Level >= (Byte)(WeaponSkill.OldLevel / 2) && WeaponSkill.Level < WeaponSkill.OldLevel))
            {
                WeaponSkill.Level++;
                WeaponSkill.Exp = 0;
                Send(new MsgWeaponSkill(WeaponSkill));
                return;
            }

            WeaponSkill.Exp += Exp;
            Send(new MsgFlushExp(WeaponSkill));
        }

        public void Mine()
        {
            if (Mining && Environment.TickCount - LastPickSwingTick < 3000)
                return;

            Mining = true;
            LastPickSwingTick = Environment.TickCount;

            if (!IsAlive())
            {
                Mining = false;
                SendSysMsg(StrRes.STR_DIE);
                return;
            }

            Item Item = GetItemByPos(4);
            if (Item == null)
            {
                Mining = false;
                SendSysMsg(StrRes.STR_MINE_WITH_PECKER);
                return;
            }

            if (Item.Type / 1000 != 562)
            {
                Mining = false;
                SendSysMsg(StrRes.STR_MINE_WITH_PECKER);
                return;
            }

            World.BroadcastRoomMsg(this, new MsgAction(this, 0, MsgAction.Action.Mine), true);

            Double Bonus = 1.00;
            if (mWeaponSkills.ContainsKey(562))
                Bonus += (Double)mWeaponSkills[562].Level / 100.00;

            Int32 ItemId = 0;
            if (MyMath.Success(30 * Bonus))
            {
                //Iron
                ItemId = 1072010 + MyMath.Generate(0, 3);
                if (MyMath.Success(25 * Bonus))
                {
                    ItemId = 1072010 + MyMath.Generate(4, 7);
                    if (MyMath.Success(25 * Bonus))
                        ItemId++;
                    if (MyMath.Success(15 * Bonus))
                        ItemId++;
                }

                //Copper
                if (MyMath.Success(75.0 * Bonus) && Map.Id != 1028)
                {
                    ItemId = 1072020 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072020 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                //Silver
                if (MyMath.Success(35.0 * Bonus) && (Map.Id == 1026 || Map.Id == 1027))
                {
                    ItemId = 1072040 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072040 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                //Gold
                if (MyMath.Success(5.0 * Bonus) && Map.Id != 1027 || MyMath.Success(25.0 * Bonus))
                {
                    ItemId = 1072050 + MyMath.Generate(0, 3);
                    if (MyMath.Success(25 * Bonus))
                    {
                        ItemId = 1072050 + MyMath.Generate(4, 7);
                        if (MyMath.Success(25 * Bonus))
                            ItemId++;
                        if (MyMath.Success(15 * Bonus))
                            ItemId++;
                    }
                }

                if (MyMath.Success(7.5 * Bonus) && Map.Id == 1028)
                    ItemId = 1072031; //Euxite

                if (MyMath.Success(1.5 * Bonus))
                {
                    ItemId = 700001;
                    while (true)
                    {
                        Byte GemType = (Byte)MyMath.Generate(0, 7);
                        if (Map.Id == 1028 && !(GemType == 0 || GemType == 1 || GemType == 2 || GemType == 7))
                            continue;
                        else if (Map.Id == 1025 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        else if (Map.Id == 1026 && !(GemType == 5 || GemType == 6))
                            continue;
                        else if (Map.Id == 1027 && !(GemType == 5 || GemType == 6))
                            continue;
                        else if (Map.Id == 1218 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        else if (Map.Id == 6000 && !(GemType == 0 || GemType == 1 || GemType == 3 || GemType == 4))
                            continue;
                        ItemId += (GemType * 10);
                        break;
                    }
                    if (MyMath.Success(7.5 * Bonus))
                        ItemId++;
                    if (MyMath.Success(5.0 * Bonus))
                        ItemId++;
                }
            }

            if (ItemId != 0 && ItemInInventory() < 40)
                AddItem(Item.Create(0, 0, ItemId, 0, 0, 0, 0, 0, 2, 0, ItemHandler.GetMaxDura(ItemId), ItemHandler.GetMaxDura(ItemId)), true);
        }

        public Boolean Reflect()
        {
            if (!(FirstProfession == 25 && Profession > 19 && Profession < 26) && !(SecondProfession == 25))
                return false;

            if (!MyMath.Success(15))
                return false;
            return true;
        }

        public Boolean IsAllNonsuchEquip()
        {
            Boolean TwoHandsWeapon = false;
            for (Byte i = 1; i < 10; i++)
            {
                if (i == 5 && TwoHandsWeapon)
                    continue;

                if (i == 7 || i == 9)
                    continue;

                Item Item = GetItemByPos(i);
                if (Item == null)
                    return false;

                if (Item.Type % 10 != 9)
                    return false;

                if (i == 4 && ((Byte)(Item.Type / 100000) == 5 || (Int16)(Item.Type / 1000) == 421))
                    TwoHandsWeapon = true;
            }
            return true;
        }

        public Int32 GetArmorTypeID()
        {
            Item Item = GetItemByPos(3);
            if (Item == null)
                return 0;
            return Item.Type;
        }

        public Int32 GetHeadTypeID()
        {
            Item Item = GetItemByPos(1);
            if (Item == null)
                return 0;
            return Item.Type;
        }

        public Int32 GetRightHandTypeID()
        {
            Item Item = GetItemByPos(4);
            if (Item == null)
                return 0;
            return Item.Type;
        }

        public Int32 GetLeftHandTypeID()
        {
            Item Item = GetItemByPos(5);
            if (Item == null)
                return 0;
            return Item.Type;
        }

        public Item GetItemByPos(Byte Position)
        {
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == Position)
                        return Item;
                }
            }
            return null;
        }

        public Item GetItemById(Int32 Id)
        {
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == 0 && Item.Type == Id)
                        return Item;
                }
            }
            return null;
        }

        public Item GetItemByUID(Int32 UniqId)
        {
            if (Items.ContainsKey(UniqId))
                return Items[UniqId];
            return null;
        }

        public Boolean InventoryContains(Int32 Id, Int32 Count)
        {
            Int32 ICount = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Type == Id && Item.Position == 0)
                        ICount++;
                }
            }
            return Count <= ICount;
        }

        public Int32 ItemInInventory()
        {
            Int32 Count = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == 0)
                        Count++;
                }
            }
            return Count;
        }

        public Item[] GetWHItems(Int16 WarehouseUID)
        {
            List<Item> WHItems = new List<Item>();
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == WarehouseUID)
                        WHItems.Add(Item);
                }
            }
            return WHItems.ToArray();
        }

        public Int32 ItemInWarehouse(Int16 WarehouseUID)
        {
            Int32 Count = 0;
            lock (Items)
            {
                foreach (Item Item in Items.Values)
                {
                    if (Item.Position == WarehouseUID)
                        Count++;
                }
            }
            return Count;
        }

        public void AddItem(Item Item, Boolean Send)
        {
            if (Items.ContainsKey(Item.Id))
                return;

            Item.OwnerUID = UniqId;
            Item.Position = 0;

            lock (Items) { Items.Add(Item.Id, Item); }
            if (Send)
                this.Send(new MsgItemInfo(Item, MsgItemInfo.Action.AddItem));
        }

        public void UpdateItem(Item Item)
        {
            if (!Items.ContainsKey(Item.Id))
                return;

            Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));
            World.BroadcastRoomMsg(this, new MsgItemInfoEx(UniqId, Item, 0, MsgItemInfoEx.Action.Equipment), false);
        }

        public void DelItem(Item Item, Boolean Send)
        {
            if (!Items.ContainsKey(Item.Id))
                return;

            Item.OwnerUID = 0;

            if (Send)
                this.Send(new MsgItem(Item.Id, Item.Position, MsgItem.Action.Drop)); //?

            lock (Items) { Items.Remove(Item.Id); }
        }

        public void DelItem(Int32 UniqId, Boolean Send)
        {
            if (!Items.ContainsKey(UniqId))
                return;

            Item Item = Items[UniqId];
            Item.OwnerUID = 0;

            if (Send)
                this.Send(new MsgItem(UniqId, Item.Position, MsgItem.Action.Drop)); //?

            lock (Items) { Items.Remove(UniqId); }
        }

        public void DelItem(Int32 Id, Int32 Count, Boolean Send)
        {
            lock (Items)
            {
                Item[] Array = new Item[Items.Count];
                Items.Values.CopyTo(Array, 0);

                Int32 ICount = 0;
                for (Int32 i = 0; i < Array.Length; i++)
                {
                    if (Count == ICount)
                        break;

                    if (Array[i].Type == Id && Array[i].Position == 0)
                    {
                        Array[i].OwnerUID = 0;

                        if (Send)
                            this.Send(new MsgItem(Array[i].Id, 0, MsgItem.Action.Drop));
                        Items.Remove(Array[i].Id);
                        ICount++;
                    }
                }

                Array = null;
            }
        }

        public WeaponSkill GetWeaponSkillByType(UInt16 aType)
        {
            lock (mWeaponSkills)
            {
                foreach (WeaponSkill skill in mWeaponSkills.Values)
                {
                    if (skill.Type == aType)
                        return skill;
                }
            }

            return null;
        }

        /// <summary>
        /// Award a weapon skill to the player.
        /// </summary>
        /// <param name="aSkill">The awarded skill.</param>
        /// <param name="aSend">Whether or not the client must be updated.</param>
        public void AwardSkill(WeaponSkill aSkill, Boolean aSend)
        {
            if (aSkill.Player.UniqId != UniqId)
                return;

            if (mWeaponSkills.ContainsKey(aSkill.Id))
                return;

            lock (mWeaponSkills) { mWeaponSkills.Add(aSkill.Id, aSkill); }

            if (aSend)
                Send(new MsgWeaponSkill(aSkill));
        }

        /// <summary>
        /// Drop a weapon skill.
        /// </summary>
        /// <param name="aSkill">The skill to drop.</param>
        /// <param name="aSend">Whether or not the client must be updated.</param>
        public void DropSkill(WeaponSkill aSkill, Boolean aSend)
        {
            if (!mWeaponSkills.ContainsKey(aSkill.Id))
                return;

            if (Database.Delete(aSkill))
            {
                lock (mWeaponSkills) { mWeaponSkills.Remove(aSkill.Id); }

                if (aSend)
                    Send(new MsgAction(this, aSkill.Type, MsgAction.Action.DropSkill));
            }
        }

        /// <summary>
        /// Send the weapon skills information.
        /// </summary>
        public void SendWeaponSkillSet()
        {
            lock (mWeaponSkills)
            {
                foreach (var skill in mWeaponSkills.Values)
                {
                    if (!skill.Unlearn)
                        Send(new MsgWeaponSkill(skill));
                }
            }
        }

        public Magic GetMagicByType(UInt16 aType)
        {
            lock (mMagics)
            {
                foreach (Magic magic in mMagics.Values)
                {
                    if (magic.Type == aType)
                        return magic;
                }
            }

            return null;
        }

        /// <summary>
        /// Award a magic skill to the player.
        /// </summary>
        /// <param name="aMagic">The awarded magic skill.</param>
        /// <param name="aSend">Whether or not the client must be updated.</param>
        public void AwardMagic(Magic aMagic, Boolean aSend)
        {
            if (aMagic.Player.UniqId != UniqId)
                return;

            if (mMagics.ContainsKey(aMagic.Id))
                return;

            lock (mMagics) { mMagics.Add(aMagic.Id, aMagic); }

            if (aSend)
                Send(new MsgMagicInfo(aMagic));
        }

        /// <summary>
        /// Drop a magic skill.
        /// </summary>
        /// <param name="aMagic">The magic skill to drop.</param>
        /// <param name="aSend">Whether or not the client must be updated.</param>
        public void DropMagic(Magic aMagic, Boolean aSend)
        {
            if (!mMagics.ContainsKey(aMagic.Id))
                return;

            if (Database.Delete(aMagic))
            {
                lock (mMagics) { mMagics.Remove(aMagic.Id); }

                if (aSend)
                    Send(new MsgAction(this, aMagic.Type, MsgAction.Action.DropMagic));
            }
        }

        /// <summary>
        /// Send the magic skills information.
        /// </summary>
        public void SendMagicSkillSet()
        {
            lock (mMagics)
            {
                foreach (var magic in mMagics.Values)
                {
                    if (!magic.Unlearn)
                        Send(new MsgMagicInfo(magic));
                }
            }
        }

        /// <summary>
        /// Disconnect the client.
        /// </summary>
        public void Disconnect() { Client.Disconnect(); }

        /// <summary>
        /// Send the message to the client.
        /// </summary>
        /// <param name="aMsg">The message to send to the client.</param>
        public override void Send(Msg aMsg) { Client.Send(aMsg); }

        /// <summary>
        /// Process the MsgTick data.
        /// </summary>
        /// <param name="aClientTime">The client time.</param>
        /// <param name="aMsgCount">The client's message count.</param>
        public void ProcessTick(Int32 aClientTime, UInt32 aMsgCount)
        {
            if (mMsgCount == 0)
            {
                mMsgCount = aMsgCount;
            }

            if (mMsgCount > aMsgCount || mMsgCount + 16 < aMsgCount) // cheater found !
            {
                Program.Log("[CHEAT] Msg counter of {0} (uid={1}) is too off. (Should be around {2}, got {3}).",
                    Name, UniqId, mMsgCount, aMsgCount);

                Disconnect(); // disconnect the client...
                return;
            }

            if (mFirstClientTick == 0)
            {
                mFirstClientTick = aClientTime;
            }

            if (aClientTime < mLastClientTick) // ridiculous timestamp
            {
                SendSysMsg(StrRes.STR_CONNECTION_OFF);

                Disconnect(); // disconnect the client...
                return;
            }

            const int CRITICAL_TICK = 500; // 500 ms

            int nServerTicks = mServerTicks.Count;
            if (mLastClientTick != 0 && nServerTicks >= 2 &&
                aClientTime > mLastClientTick + 10000 + CRITICAL_TICK)
            {
                // suspicious timestamp
                var serverTime = DateTime.Now;
                var serverTickInterval = (mServerTicks.First.Next.Value - mServerTicks.First.Value).TotalMilliseconds;

                var echoTime = (serverTime - mServerTicks.First.Next.Value).TotalMilliseconds;
                if (echoTime < aClientTime - mLastClientTick - serverTickInterval)
                {
                    Program.Log("[CHEAT] {0} (uid={1}) has a suspicious timestamp.",
                        Name, UniqId);
                    SendSysMsg(StrRes.STR_CONNECTION_OFF);

                    Disconnect(); // disconnect the client...
                    return;
                }
            }

            if (mServerTicks.Count >= 2)
                mServerTicks.RemoveLast();

            mMsgCount = aMsgCount;
            mLastClientTick = aClientTime;
            mLastRcvClientTick = DateTime.Now;
        }


        /// <summary>
        /// Called when the timer elapse.
        /// </summary>
        public override void TimerElapsed(Object aSender, ElapsedEventArgs Args)
        {
            DateTime now = DateTime.Now;

            //Reset speedhack check each 3 mins
            if (Environment.TickCount - PrevSpeedHackReset > 180000)
            {
                PrevSpeedHackReset = Environment.TickCount;
                SpeedHack = 0;
            }

            if (SpeedHack >= 15)
            {
                SpeedHack = 0;
                if (Database.SendPlayerToJail(Name))
                    Program.Log("[CRIME] {0} has been sent to jail for using a speed hack!", Name);
            }

            if (Environment.TickCount - LastSave > 20000)
            {
                Database.Save(this, true);
                LastSave = Environment.TickCount;
            }

            //////////////////////////////////////////////////////////////
            ///  MsgTick : Ping / Pong
            //////////////////////////////////////////////////////////////
            if (mFirstServerTick == DateTime.MinValue) // run only once per user
            {
                mFirstServerTick = now;
                mLastRcvClientTick = now;

                mLastServerTick = now;
                mServerTicks.AddLast(now);

                Send(new MsgTick(this));
            }
            else
            {
                if (now >= mLastServerTick.AddSeconds(10)) // each 10s
                {
                    mLastServerTick = now;
                    mServerTicks.AddLast(now);

                    Send(new MsgTick(this));
                }
            }

            if (mLastRcvClientTick != DateTime.MinValue)
            {
                if ((now - mLastRcvClientTick).TotalSeconds >= 25) // no feedback after 25s...
                {
                    // reject
                    Disconnect();
                }
            }

            //////////////////////////////////////////////////////////////
            ///  Clear statuses
            //////////////////////////////////////////////////////////////
            List<Status> statuses;
            lock (mStatuses)
                statuses = new List<Status>(mStatuses.Keys);

            StatusEffect effect;
            foreach (Status status in statuses)
            {
                if (GetStatus(status, out effect))
                {
                    if (effect.IsElapsed())
                    {
                        DetachStatus(status);

                        if (status == Status.SuperAtk || status == Status.SuperSpeed)
                        {
                            if (CurKO > KO)
                            {
                                KO = CurKO;
                                if (CurKO > 15000)
                                    TimeAdd = 3;
                                else if (CurKO > 10000)
                                    TimeAdd = 2;
                                else if (CurKO > 5000)
                                    TimeAdd = 1;
                                Send(new MsgUserAttrib(this, TimeAdd, MsgUserAttrib.AttributeType.TimeAdd));
                            }
                            if (CurKO > 1000)
                                World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Name + " a tué " + CurKO + " monstres en KO!", Channel.Talk, Color.White));
                            CurKO = 0;
                        }
                    }
                }
            }

            MyMath.GetEquipStats(this); // TODO remove this


            if (IsInBattle && Environment.TickCount - LastAttackTick >= AtkSpeed)
            {
                if (Entity.IsPlayer(TargetUID))
                {
                    Player Target = null;
                    if (World.AllPlayers.TryGetValue(TargetUID, out Target))
                    {
                        if (MagicType != 0)
                            Battle.UseMagic(this, Target, Target.X, Target.Y);
                        else
                            Battle.PvP(this, Target);
                    }
                    else
                        IsInBattle = false;
                }
                else if (Entity.IsMonster(TargetUID))
                {
                    Monster Target = null;
                    if (World.AllMonsters.TryGetValue(TargetUID, out Target))
                    {
                        if (MagicType != 0)
                            Battle.UseMagic(this, Target, Target.X, Target.Y);
                        else
                            Battle.PvM(this, Target);
                    }
                    else
                        IsInBattle = false;
                }
                else if (Entity.IsTerrainNPC(TargetUID))
                {
                    TerrainNPC Target = null;
                    if (World.AllTerrainNPCs.TryGetValue(TargetUID, out Target))
                    {
                        if (MagicType != 0)
                            Battle.UseMagic(this, Target, Target.X, Target.Y);
                        else
                            Battle.PvE(this, Target);
                    }
                    else
                        IsInBattle = false;
                }
                else if (TargetUID == 0)
                {
                    if (MagicType != 0)
                        Battle.UseMagic(this, null, X, Y);
                }
                else
                    IsInBattle = false;
            }

            if (PkPoints > 0 && Environment.TickCount - LastPkPointRemove > 120000)
            {
                LastPkPointRemove = Environment.TickCount;
                if (PkPoints == 100)
                {
                    DetachStatus(Status.BlackName);
                    AttachStatus(Status.RedName);
                }
                else if (PkPoints == 30)
                {
                    DetachStatus(Status.RedName);
                }
                PkPoints--;
                Send(new MsgUserAttrib(this, PkPoints, MsgUserAttrib.AttributeType.PkPoints));
            }

            if (DblExpEndTime != 0)
            {
                if (Environment.TickCount >= DblExpEndTime)
                {
                    DblExpEndTime = 0;
                    Send(new MsgUserAttrib(this, DblExpEndTime, MsgUserAttrib.AttributeType.DblExpTime));
                }
            }

            #region LuckyTime
            if (LuckyTime != 0 && !(Praying || CastingPray))
            {
                LuckyTime -= 500;
                if (LuckyTime <= 0)
                    LuckyTime = 0;
                Send(new MsgUserAttrib(this, LuckyTime, MsgUserAttrib.AttributeType.LuckyTime));
            }

            if (CastingPray)
            {
                if (X != PrayX || Y != PrayY || Map.Id != PrayMap || IsInBattle)
                {
                    CastingPray = false;
                    PrayX = 0;
                    PrayY = 0;

                    DetachStatus(Status.CastingPray);

                    List<Player> Casters = new List<Player>();
                    List<Player> Prayers = new List<Player>();
                    foreach (Entity Entity in Map.Entities.Values)
                    {
                        if (!Entity.IsPlayer())
                            continue;

                        Player Player2 = Entity as Player;
                        if (Player2.CastingPray)
                            Casters.Add(Player2);

                        if (Player2.Praying)
                            Prayers.Add(Player2);
                    }

                    foreach (Player Prayer in Prayers)
                    {
                        Boolean Stop = true;
                        foreach (Player Caster in Casters)
                        {
                            if (MyMath.CanSee(Prayer.X, Prayer.Y, Caster.PrayX, Caster.PrayY, 4))
                            {
                                Stop = false;
                                break;
                            }
                        }

                        if (Stop)
                        {
                            Prayer.Praying = false;
                            Prayer.DetachStatus(Status.Praying);
                        }
                    }
                }
                else
                {

                    LuckyTime += 1500;
                    if (LuckyTime > Player.MAX_LUCKY_TIME)
                        LuckyTime = Player.MAX_LUCKY_TIME;
                }
            }

            if (Praying)
            {
                if (X != PrayX || Y != PrayY || Map.Id != PrayMap || IsInBattle)
                {
                    Praying = false;
                    PrayMap = 0;
                    PrayX = 0;
                    PrayY = 0;

                    DetachStatus(Status.Praying);
                }

                LuckyTime += 500;
                if (LuckyTime > Player.MAX_LUCKY_TIME)
                    LuckyTime = Player.MAX_LUCKY_TIME;
            }
            else
            {
                List<Player> Casters = new List<Player>();
                foreach (Entity Entity in Screen.mEntities.Values)
                {
                    if (!Entity.IsPlayer())
                        continue;

                    Player Caster = Entity as Player;
                    if (Caster.CastingPray)
                        Casters.Add(Caster);
                }

                foreach (Player Caster in Casters)
                {
                    if (MyMath.CanSee(X, Y, Caster.PrayX, Caster.PrayY, 4))
                    {
                        Praying = true;
                        PrayMap = Map.Id;
                        PrayX = X;
                        PrayY = Y;

                        AttachStatus(Status.Praying);
                        break;
                    }
                    break;
                }
            }
            #endregion

            if (Team != null && Team.Leader.UniqId == UniqId)
            {
                if (Environment.TickCount - LastLeaderPosTick > 5000)
                {
                    Team.LeaderPosition();
                    LastLeaderPosTick = Environment.TickCount;
                }
            }

            if (IsAlive())
            {
                const Byte maxEnergy = 100;

                if (CurHP > UInt16.MaxValue)
                    Die(null);

                if (Action == Emotion.SitDown && !IsFlying())
                {
                    if (Energy < maxEnergy)
                    {
                        Energy += 8;
                        if (Energy > maxEnergy)
                            Energy = maxEnergy;
                        Send(new MsgUserAttrib(this, Energy, MsgUserAttrib.AttributeType.Energy));
                    }
                }
                if (Action == Emotion.StandBy && !IsFlying())
                {
                    if (Energy < maxEnergy)
                    {
                        Energy++;
                        if (Energy > maxEnergy)
                            Energy = maxEnergy;
                        Send(new MsgUserAttrib(this, Energy, MsgUserAttrib.AttributeType.Energy));
                    }
                }

                if ((HasStatus(Status.XpFull) && Environment.TickCount - LastXPAdd > 20000)
                    || (!HasStatus(Status.XpFull) && Environment.TickCount - LastXPAdd > 3000))
                {
                    ++XP;
                    if (XP == 100)
                    {
                        AttachStatus(Status.XpFull);
                    }
                    if (XP > 100)
                    {
                        XP = 0;
                        DetachStatus(Status.XpFull);
                    }
                    if (XP % 20 == 0)
                        Send(new MsgUserAttrib(this, XP, MsgUserAttrib.AttributeType.XP));
                    LastXPAdd = Environment.TickCount;
                }

                if (TransformEndTime != 0)
                {
                    if (Environment.TickCount >= TransformEndTime)
                    {
                        TransformEndTime = 0;
                        // TODO re-enable transformation skills
                        //DelTransform();

                        if (CurHP >= MaxHP)
                            CurHP = MaxHP;
                        Double Multiplier = (Double)CurHP / (Double)MaxHP;
                        CalcMaxHP();
                        CurHP = (Int32)(MaxHP * Multiplier);

                        CalcMaxMP();
                        MyMath.GetEquipStats(this);

                        Send(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
                        Send(new MsgUserAttrib(this, MaxHP, (MsgUserAttrib.AttributeType.Life + 1)));
                        if (Team != null)
                        {
                            Team.BroadcastMsg(new MsgUserAttrib(this, CurHP, MsgUserAttrib.AttributeType.Life));
                            Team.BroadcastMsg(new MsgUserAttrib(this, MaxHP, (MsgUserAttrib.AttributeType.Life + 1)));
                        }
                    }
                }

                if (Mining && Environment.TickCount - LastPickSwingTick > 3000)
                    Mine();
            }
        }

        public void SendSysMsg(String Message) { Send(new MsgTalk("SYSTEM", "ALLUSERS", Message, Channel.System, Color.Red)); }
        public void SendSysMsg(String aFmt, params object[] aArgs) { Send(new MsgTalk("SYSTEM", "ALLUSERS", String.Format(aFmt, aArgs), Channel.System, Color.Red)); }
        public void KickBack() { Send(new MsgAction(this, Map.DocId, MsgAction.Action.EnterMap)); }

        public void Move(UInt32 aMapId, UInt16 aX, UInt16 aY)
        {
            GameMap newMap;
            if (!MapManager.TryGetMap(aMapId, out newMap))
                return;

            Action = Emotion.StandBy;

            IsInBattle = false;
            MagicIntone = false;
            Mining = false;

            // Pet
            if (Pet != null)
            {
                Pet.Brain.Sleep();
                Pet.Disappear();
                Pet = null;
            }

            Map.DelEntity(this);

            // TODO re-enable prev map in Move() ?
            //PrevMap = Map;
            //PrevX = X;
            //PrevY = Y;

            Map = newMap;
            X = aX;
            Y = aY;

            Map.AddEntity(this);

            Send(new MsgAction(this, Map.DocId, MsgAction.Action.EnterMap));
            Send(new MsgAction(this, (Int32)Map.Light, MsgAction.Action.MapARGB));
            Send(new MsgMapInfo(Map));
            if (Map.Weather != 0)
                Send(new MsgWeather(Map));
        }

        public void RemoveAtkDura()
        {
            SendGemEffect();

            for (Byte Pos = 4; Pos < 7; Pos++)
            {
                Item Item = GetItemByPos(Pos);
                if (Item == null)
                    continue;

                if (Item.CurDura == 0)
                    continue;

                if (!MyMath.Success(25) && Item.Type / 10000 != 105) //!Arrow
                    continue;

                Item.CurDura--;
                if (Item.CurDura == 0)
                    MyMath.GetEquipStats(this);
                Send(new MsgItem(Item.Id, Item.CurDura, MsgItem.Action.SynchroAmount));

                if (Item.CurDura == 0)
                    Send(new MsgItemInfo(Item, MsgItemInfo.Action.Update));

                if (Item.Type / 10000 == 105 && Item.CurDura == 0)
                {
                    IsInBattle = false;

                    Item Bow = GetItemByPos(4);
                    Send(new MsgItem(Item.Id, Item.Position, MsgItem.Action.Unequip));
                    Send(new MsgItem(Bow.Id, Bow.Position, MsgItem.Action.Unequip));
                    Send(new MsgItem(Bow.Id, Bow.Position, MsgItem.Action.Equip));
                    DelItem(Item, true);
                }
            }
        }

        public void RemoveDefDura()
        {
            BlessEffect();
            for (Byte Pos = 1; Pos < 9; Pos++)
            {
                if (Pos >= 4 && Pos <= 7)
                    continue;

                Item Item = GetItemByPos(Pos);
                if (Item == null)
                    continue;

                if (Item.CurDura == 0)
                    continue;

                if (!MyMath.Success(25))
                    continue;

                Item.CurDura--;
                if (Item.CurDura == 0)
                    MyMath.GetEquipStats(this);
                Send(new MsgItem(Item.Id, Item.CurDura, MsgItem.Action.SynchroAmount));
            }
        }

//        public bool DecEquipmentDurability(bool aIsAttacked, bool aMagic, int bDurValue/*=1*/)
//{
//    int nInc = -1 * bDurValue;
//    for(int i = ITEMPOSITION_EQUIPBEGIN; i < ITEMPOSITION_EQUIPEND; i++)
//    {
//        if (!bMagic)
//        {
//            if (i == ITEMPOSITION_RINGR || 
//                    i == ITEMPOSITION_RINGL ||
//                        i == ITEMPOSITION_SHOES ||
//                            i == ITEMPOSITION_WEAPONR ||
//                                i == ITEMPOSITION_WEAPONL)
//            {
//                if(!bBeAttack)
//                    AddEquipmentDurability(i, nInc);
//            }
//            else
//            {
//                if(bBeAttack)
//                    AddEquipmentDurability(i, nInc);
//            }
//        }
//        else
//        {
//            if (i == ITEMPOSITION_RINGR || 
//                    i == ITEMPOSITION_RINGL ||
//                        i == ITEMPOSITION_SHOES ||
//                            i == ITEMPOSITION_WEAPONR ||
//                                i == ITEMPOSITION_WEAPONL)
//            {
//                if(!bBeAttack)
//                    AddEquipmentDurability(i, -5);
//            }
//            else
//            {
//                if(bBeAttack)
//                    AddEquipmentDurability(i, nInc);
//            }
//        }
//    }
//    return true;
//}

        public double Attack(AdvancedEntity aTarget)
        {
    //CHECKF(pTarget);

    //CNpc* pNpc;
    //pTarget->QueryObj(OBJ_NPC, IPP_OF(pNpc));

    //int nAtk=0;
    //int nDamage = CBattleSystem::CalcPower(IsSimpleMagicAtk(), QueryRole(), pTarget, &nAtk, true);

    //// Ä¿±ê»¤¶Ü×´Ì¬×ªÒÆÉËº¦
    //if (nDamage>0 && pTarget->TransferShield(IsSimpleMagicAtk(), QueryRole(), nDamage))
    //{
    //}
    //else
    //{
    //    int nLoseLife = ::CutOverflow(nDamage, (int)pTarget->GetLife());
    //    if (nLoseLife > 0)
    //        pTarget->AddAttrib(_USERATTRIB_LIFE, -1*nLoseLife, SYNCHRO_TRUE);

    //    // adjust synflag damage
    //    if(pNpc && pNpc->IsSynFlag() && pNpc->IsSynMoneyEmpty())
    //    {
    //        nDamage	*= SYNWAR_NOMONEY_DAMAGETIMES;
    //    } 

    //    if (nDamage > 0)
    //    {
    //        if (::RandGet(10) == 7)
    //            this->SendGemEffect();
    //    }
		
    //    pTarget->BeAttack(IsSimpleMagicAtk(), QueryRole(), nDamage);
    //}		

    //CGameMap* pMap = this->GetMap();
    //if (pMap)
    //{
    //    // crime
    //    if (!pTarget->IsEvil()				// Ö»ÓÐpk°×Ãû²ÅÉÁÀ¶
    //            && !pMap->IsDeadIsland())	// ËÀÍöµºÉ±ÈË²»ÉÁÀ¶
    //    {
    //        if (!QueryStatus(STATUS_CRIME))
    //            this->SetCrimeStatus();
    //    }

    //    // equipment durability cost
    //    if (!pMap->IsTrainMap())
    //        this->DecEquipmentDurability(ATTACK_TIME, IsSimpleMagicAtk(), (nDamage < nAtk/10) ? 10 : 1);
    //}

    //IStatus* pStatus = QueryStatus(STATUS_DMG2LIFE);
    //if (pStatus)
    //{
    //    int nLifeGot = ::CutTrail(0, MulDiv(nDamage, pStatus->GetPower(), 100));
    //    if (nLifeGot > 0)
    //        this->AddAttrib(_USERATTRIB_LIFE, nLifeGot, SYNCHRO_TRUE);
    //}

    //return nDamage;
            return 0;
        }


//        bool CUser::BeAttack(bool bMagic, IRole* pTarget, int nPower, bool bReflectEnable/*=true*/)
//{
//    CHECKF(pTarget);

//    QueryStatusSet()->DelObj(STATUS_LURKER);
//    CRole::DetachStatus(this->QueryRole(), STATUS_FREEZE);
//    StopMine();

//    // reflect, only use for weapon attack!!!
//    IStatus* pStatus = QueryStatus(bMagic?STATUS_REFLECTMAGIC:STATUS_REFLECT);
//    if(nPower>0 && pStatus && bReflectEnable)
//    {
//        int nPower2 = AdjustData(nPower, pStatus->GetPower());
//        if(nPower2 > 0)
//        {
//            int nLoseLife = ::CutOverflow((int)pTarget->GetLife(), nPower2);
//            pTarget->AddAttrib(_USERATTRIB_LIFE, -1*nLoseLife, SYNCHRO_TRUE);
//            pTarget->BeAttack(bMagic, this, nPower2, false);

//            // ´«ËÍ½á¹ûÏûÏ¢
//            CMsgInteract msg;
//            if (msg.Create(bMagic?INTERACT_REFLECTMAGIC:INTERACT_REFLECTWEAPON, GetID(), pTarget->GetID(), GetPosX(), GetPosY(), nPower2))
//                BroadcastRoomMsg(&msg, INCLUDE_SELF);

//            // kill?
//            if (!pTarget->IsAlive())
//                pTarget->BeKill();
//        }
//    }

//    // equipment durability cost
//    if (!this->GetMap()->IsTrainMap())
//    {
//        this->DecEquipmentDurability(BEATTACK_TIME, bMagic, (nPower > this->GetMaxLife()/4) ? 10 : 1);
//    }

//    // self_defence     // add huang 2004.1.14
////	if(QueryStatus(STATUS_SELFDEFENCE))
////		this->SetSelfDefStatus();
	
//    if(nPower > 0)
//        BroadcastTeamLife();

//    // abort magic
//    if (QueryMagic() && QueryMagic()->IsIntone())
//        QueryMagic()->AbortMagic(true);

//    /*/ stamina lost
//    if (_ACTION_SITDOWN ==  this->GetPose())
//    {
//        this->SetPose(_ACTION_STANDBY);
//        this->SetAttrib(_USERATTRIB_ENERGY, this->GetEnergy()/2, SYNCHRO_TRUE);
//    }*/

//    return true;
//}

        private static readonly Dictionary<GemType, String> GEM_EFFECTS = new Dictionary<GemType, String>()
        {
            { GemType.MAtkHgt, "phoenix" },
            { GemType.DmgHgt, "goldendragon" },
            { GemType.HitHgt, "fastflash" },
            { GemType.ExpHgt, "rainbow" },
            { GemType.DurHgt, "goldenkylin" },
            { GemType.WpnExpHgt, "purpleray" },
            { GemType.MgcExpHgt, "moon" },
            { (GemType)73, "recovery" } // TODO constant
        };

        /// <summary>
        /// Send the effect of the gem to all players in screen.
        /// </summary>
        private void SendGemEffect()
        {
            List<GemType> gems = new List<GemType>();

            lock (Items)
            {
                foreach (Item item in Items.Values)
                {
                    if (item.Position > 0 && item.Position < 10)
                    {
                        if ((GemType)item.FirstGem != GemType.None && (GemType)item.FirstGem != GemType.Hole)
                            gems.Add((GemType)item.FirstGem);

                        if ((GemType)item.SecondGem != GemType.None && (GemType)item.SecondGem != GemType.Hole)
                            gems.Add((GemType)item.SecondGem);
                    }
                }
            }

            if (gems.Count <= 0)
                return;

            GemType type = gems[MyMath.Generate(0, gems.Count - 1)];
            if (!GEM_EFFECTS.ContainsKey(type))
                return;

            String effect = GEM_EFFECTS[type];

            if (effect != "")
                World.BroadcastRoomMsg(this, new MsgName(UniqId, effect, MsgName.NameAct.RoleEffect), true);
        }

        private static readonly Dictionary<Byte, String> BLESS_EFFECTS = new Dictionary<Byte, String>()
        {
            { 1, "Aegis1" },
            { 3, "Aegis2" },
            { 5, "Aegis5" },
            { 7, "Aegis7" }
        };

        private void BlessEffect()
        {
            if (MyMath.Success(10))
            {
                List<String> effects = new List<String>();
                for (Byte pos = 0; pos < 9; pos++)
                {
                    Item item = GetItemByPos(pos);
                    if (item == null)
                        continue;

                    if (item.Bless == 0)
                        continue;

                    String itemEffect = null;

                    BLESS_EFFECTS.TryGetValue(item.Bless, out itemEffect);

                    if (itemEffect != null && !effects.Contains(itemEffect))
                        effects.Add(itemEffect);
                }

                String effect = "";
                if (effects.Count > 0)
                    effect = effects[MyMath.Generate(0, effects.Count - 1)];

                if (effect != "")
                    World.BroadcastRoomMsg(this, new MsgName(UniqId, effect, MsgName.NameAct.RoleEffect), true);
            }
        }
    }
}
