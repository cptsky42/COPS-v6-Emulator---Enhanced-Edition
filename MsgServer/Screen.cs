using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using COServer.Network;
using COServer.Entities;

namespace COServer
{
    public class Screen
    {
        public ConcurrentDictionary<Int32, Entity> Entities;
        public ConcurrentDictionary<Int32, FloorItem> FloorItems;
        private Player m_Owner;

        public Player Owner { get { return m_Owner; } }

        public Screen(Player Owner)
        {
            Entities = new ConcurrentDictionary<Int32, Entity>();
            FloorItems = new ConcurrentDictionary<Int32, FloorItem>();
            m_Owner = Owner;
        }

        ~Screen()
        {
            Entities.Clear();
            Entities = null;
            FloorItems.Clear();
            FloorItems = null;
            m_Owner = null;
        }

        public Boolean Contains(Int32 UniqId) { return Entities.ContainsKey(UniqId); }

        public void Add(Object Object, Boolean Send)
        {
            Entity Entity = (Object as Entity);
            if (Entity != null)
            {
                if (!Entities.ContainsKey(Entity.UniqId) && Entity.UniqId != Owner.UniqId)
                {
                    Entities.TryAdd(Entity.UniqId, Entity);

                    if (Send)
                    {
                        if (Entity.IsPlayer())
                        {
                            Owner.Send(MsgPlayer.Create((Object as Player)));

                            Byte Param = 0;
                            if ((Object as Player).CurseEndTime != 0)
                                Param += 1;
                            if ((Object as Player).BlessEndTime != 0)
                                Param += 2;
                            Owner.Send(MsgUserAttrib.Create((Object as Player), Param, MsgUserAttrib.Type.SizeAdd));

                            if ((Object as Player).SupermanEndTime != 0 || (Object as Player).SupermanEndTime != 0)
                                Owner.Send(MsgInteract.Create((Object as Player), null, 0xFFFF * (Object as Player).CurKO, MsgInteract.Action.Kill));
                        }
                        else if (Entity.IsMonster() && (Object as AdvancedEntity).IsAlive())
                            Owner.Send(MsgPlayer.Create((Object as Monster)));
                        else if (Entity.IsNPC())
                        {
                            if (Entity.IsTerrainNPC())
                                Owner.Send(MsgNpcInfoEx.Create((Object as TerrainNPC), (Object as TerrainNPC).Name != null));
                            else
                            {
                                if ((Object as NPC).Type == (Byte)NPC.NpcType.Booth)
                                    (Object as Booth).SendShow(Owner);
                                else
                                    Owner.Send(MsgNpcInfo.Create((Object as NPC), false));
                            }
                        }
                    }
                }
            }

            FloorItem Item = (Object as FloorItem);
            if (Item != null)
            {
                if (!FloorItems.ContainsKey(Item.UniqId))
                {
                    FloorItems.TryAdd(Item.UniqId, Item);

                    if (Send)
                        Owner.Send(MsgMapItem.Create(Item, MsgMapItem.Action.Create));
                }
            }
        }

        public void Remove(Object Object, Boolean Send)
        {
            Entity Entity = (Object as Entity);
            if (Entity != null)
            {
                if (Entities.ContainsKey(Entity.UniqId))
                {
                    Entity Tmp = null;
                    Entities.TryRemove(Entity.UniqId, out Tmp);
                }

                if (Send)
                    Owner.Send(MsgAction.Create(Entity, 0, MsgAction.Action.LeaveMap));
            }

            FloorItem Item = (Object as FloorItem);
            if (Item != null)
            {
                if (FloorItems.ContainsKey(Item.UniqId))
                {
                    FloorItem Tmp = null;
                    FloorItems.TryRemove(Item.UniqId, out Tmp);
                }

                if (Send)
                    Owner.Send(MsgMapItem.Create(Item, MsgMapItem.Action.Delete));
            }
        }

        public void Clear(Boolean Send)
        {
            if (Send)
            {
                foreach (Entity Entity in Entities.Values)
                {
                    if (Entity.IsPlayer())
                        (Entity as Player).Screen.Remove(Owner, true);

                    Remove(Entity, true);
                }
            }
            Entities.Clear();


            if (Send)
            {
                foreach (FloorItem Item in FloorItems.Values)
                {
                    Remove(Item, true);
                }
            }
            FloorItems.Clear();
        }

        public void ChangeMap()
        {
            Clear(true);
            Check();
        }

        public void Reset(Boolean Send)
        {
            Clear(Send);
            Check();
        }

        public void Move(Byte[] Buffer)
        {
            Check();

            foreach (Entity Entity in Entities.Values)
            {
                if (Entity.IsPlayer())
                    (Entity as Player).Send(Buffer);

                if (!MyMath.CanSee(Owner.X, Owner.Y, Entity.X, Entity.Y, 17))
                {
                    Remove(Entity, true);
                    if (Entity.IsPlayer())
                        (Entity as Player).Screen.Remove(Owner, true);
                }
            }

            foreach (FloorItem Item in FloorItems.Values)
            {
                if (!MyMath.CanSee(Owner.X, Owner.Y, Item.X, Item.Y, 17))
                    Remove(Item, true);
            }
        }

        private void Check()
        {
            Map Map = null;
            if (!World.AllMaps.TryGetValue(Owner.Map, out Map))
                return;

            foreach (Object Object in Map.Entities.Values)
            {
                Entity Entity = (Object as Entity);
                if (Entity.UniqId == Owner.UniqId)
                    continue;

                if (Entity.IsMonster())
                    if (MyMath.CanSee(Owner.X, Owner.Y, Entity.X, Entity.Y, (Entity as Monster).ViewRange))
                        (Entity as Monster).Brain.Awake();

                if (Contains(Entity.UniqId))
                    continue;

                if (MyMath.CanSee(Owner.X, Owner.Y, Entity.X, Entity.Y, 17))
                {
                    Add(Entity, true);
                    if (Entity.IsPlayer())
                        (Entity as Player).Screen.Add(Owner, true);
                }
            }

            foreach (FloorItem Item in Map.FloorItems.Values)
            {
                if (Contains(Item.UniqId))
                    continue;

                if (MyMath.CanSee(Owner.X, Owner.Y, Item.X, Item.Y, 17))
                    Add(Item, true);
            }
        }

        public void Update()
        {
            foreach (Entity Entity in Entities.Values)
            {
                if (Entity.IsPlayer())
                {
                    (Entity as Player).Send(MsgAction.Create(Owner, 0, MsgAction.Action.LeaveMap));
                    (Entity as Player).Send(MsgPlayer.Create(Owner));
                }
            }
        }
    }
}
