// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Concurrent;
using System.Linq;
using COServer.Entities;
using COServer.Network;

namespace COServer
{
    public class Screen
    {
        internal readonly ConcurrentDictionary<Int32, Entity> mEntities = new ConcurrentDictionary<Int32, Entity>();
        internal readonly ConcurrentDictionary<Int32, FloorItem> mFloorItems = new ConcurrentDictionary<Int32, FloorItem>();
        private Player mPlayer;

        public Screen(Player aPlayer)
        {
            mPlayer = aPlayer;
        }

        public Boolean Contains(Int32 UniqId) { return mEntities.ContainsKey(UniqId); }

        public void Add(Entity aEntity, Boolean aSend)
        {
            if (aEntity.UniqId == mPlayer.UniqId)
                return;

            if (!mEntities.ContainsKey(aEntity.UniqId))
            {
                mEntities.TryAdd(aEntity.UniqId, aEntity);

                if (aSend)
                {
                    if (aEntity.IsPlayer())
                    {
                        Player player = (Player)aEntity;

                        mPlayer.Send(new MsgPlayer(player));

                        if (player.HasStatus(Status.SuperAtk) || player.HasStatus(Status.SuperSpeed))
                            mPlayer.Send(new MsgInteract(player, null, 0xFFFF * (player.CurKO + 1), MsgInteract.Action.Kill));
                    }
                    else if (aEntity.IsMonster() && (aEntity as Monster).IsAlive())
                        mPlayer.Send(new MsgPlayer((aEntity as Monster)));
                    else if (aEntity.IsNPC())
                    {
                        if (aEntity.IsTerrainNPC())
                            mPlayer.Send(new MsgNpcInfoEx((aEntity as TerrainNPC), (aEntity as TerrainNPC).Name != null));
                        else
                        {
                            if ((aEntity as NPC).Type == (Byte)NPC.NpcType.Booth)
                                (aEntity as Booth).SendShow(mPlayer);
                            else
                                mPlayer.Send(new MsgNpcInfo((aEntity as NPC), false));
                        }
                    }
                }
            }
        }

        public void Add(FloorItem aItem, Boolean aSend)
        {
            if (!mFloorItems.ContainsKey(aItem.Id))
            {
                mFloorItems.TryAdd(aItem.Id, aItem);

                if (aSend)
                    mPlayer.Send(new MsgMapItem(aItem, MsgMapItem.Action.Create));
            }
        }

        public void Remove(Entity aEntity, Boolean aSend)
        {
            if (mEntities.ContainsKey(aEntity.UniqId))
            {
                Entity entity = null;
                mEntities.TryRemove(aEntity.UniqId, out entity);
            }

            if (aSend)
                mPlayer.Send(new MsgAction(aEntity, 0, MsgAction.Action.LeaveMap));
        }

        public void Remove(FloorItem aItem, Boolean aSend)
        {
            if (mFloorItems.ContainsKey(aItem.Id))
            {
                FloorItem item = null;
                mFloorItems.TryRemove(aItem.Id, out item);
            }

            if (aSend)
                mPlayer.Send(new MsgMapItem(aItem, MsgMapItem.Action.Delete));
        }

        public void Clear(Boolean aSend)
        {
            if (aSend)
            {
                foreach (Entity entity in mEntities.Values)
                {
                    if (entity.IsPlayer())
                        (entity as Player).Screen.Remove(mPlayer, true);

                    Remove(entity, true);
                }
            }
            mEntities.Clear();


            if (aSend)
            {
                foreach (FloorItem item in mFloorItems.Values)
                    Remove(item, true);
            }
            mFloorItems.Clear();
        }

        public void ChangeMap()
        {
            Clear(true);
            Check();
        }

        public void Move(Msg aMsg)
        {
            Check();

            foreach (Entity entity in mEntities.Values)
            {
                if (entity.IsPlayer())
                    (entity as Player).Send(aMsg);

                if (!MyMath.CanSee(mPlayer.X, mPlayer.Y, entity.X, entity.Y, 17))
                {
                    Remove(entity, false);
                    if (entity.IsPlayer())
                        (entity as Player).Screen.Remove(mPlayer, false);
                }
            }

            foreach (FloorItem item in mFloorItems.Values)
            {
                if (!MyMath.CanSee(mPlayer.X, mPlayer.Y, item.X, item.Y, 17))
                    Remove(item, false);
            }
        }

        private void Check()
        {
            foreach (Entity entity in mPlayer.Map.Entities.Values)
            {
                if (entity.UniqId == mPlayer.UniqId)
                    continue;

                if (entity.IsMonster())
                    if (MyMath.CanSee(mPlayer.X, mPlayer.Y, entity.X, entity.Y, (entity as Monster).ViewRange))
                        (entity as Monster).Brain.Awake();

                if (Contains(entity.UniqId))
                    continue;

                if (MyMath.CanSee(mPlayer.X, mPlayer.Y, entity.X, entity.Y, 17))
                {
                    Add(entity, true);
                    if (entity.IsPlayer())
                        (entity as Player).Screen.Add(mPlayer, true);
                }
            }

            foreach (FloorItem Item in mPlayer.Map.FloorItems.Values)
            {
                if (Contains(Item.Id))
                    continue;

                if (MyMath.CanSee(mPlayer.X, mPlayer.Y, Item.X, Item.Y, 17))
                    Add(Item, true);
            }
        }
    }
}
