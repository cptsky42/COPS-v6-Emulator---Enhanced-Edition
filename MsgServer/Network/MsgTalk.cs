// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using COServer.Entities;

[assembly: InternalsVisibleTo("COServer.Network.Msg")]

namespace COServer.Network
{
    public class MsgTalk : Msg
    {
        /// <summary>
        /// This is a "constant" that the child must override.
        /// It is the type of the message as specified in NetworkDef.cs file.
        /// </summary>
        protected override UInt16 _TYPE { get { return MSG_TALK; } }

        //--------------- Internal Members ---------------
        private Color __Color = 0;
        private Channel __Channel = 0;
        private UInt16 __Style = 0;
        private Int32 __Timestamp = 0;
        private StringPacker __StrPacker = null;
        //------------------------------------------------

        public Color Color
        {
            get { return __Color; }
            set { __Color = value; WriteUInt32(4, (UInt32)value); }
        }

        public Channel Channel
        {
            get { return __Channel; }
            set { __Channel = value; WriteUInt16(8, (UInt16)value); }
        }

        public UInt16 Style
        {
            get { return __Style; }
            set { __Style = value; WriteUInt16(10, value); }
        }

        public Int32 Timestamp
        {
            get { return __Timestamp; }
            set { __Timestamp = value; WriteInt32(12, value); }
        }

        public String Speaker
        {
            get { String speaker = ""; __StrPacker.GetString(out speaker, 0); return speaker; }
            set { __StrPacker.AddString(value); }
        }

        public String Hearer
        {
            get { String hearer = ""; __StrPacker.GetString(out hearer, 1); return hearer; }
            set { __StrPacker.AddString(value); }
        }

        public String Emotion
        {
            get { String emotion = ""; __StrPacker.GetString(out emotion, 2); return emotion; }
            set { __StrPacker.AddString(value); }
        }

        public String Words
        {
            get { String words = ""; __StrPacker.GetString(out words, 3); return words; }
            set { __StrPacker.AddString(value); }
        }

        /// <summary>
        /// Create a message object from the specified buffer.
        /// </summary>
        /// <param name="aBuf">The buffer containing the message.</param>
        /// <param name="aIndex">The index where the message is starting in the buffer.</param>
        /// <param name="aLength">The length of the message.</param>
        internal MsgTalk(Byte[] aBuf, int aIndex, int aLength)
            : base(aBuf, aIndex, aLength)
        {
            __Color = (Color)BitConverter.ToUInt32(mBuf, 4);
            __Channel = (Channel)BitConverter.ToUInt16(mBuf, 8);
            __Style = BitConverter.ToUInt16(mBuf, 10);
            __Timestamp = BitConverter.ToInt32(mBuf, 12);

            __StrPacker = new StringPacker(this, 16);
        }

        public MsgTalk(String aSpeaker, String aHearer, String aWords, Channel aChannel, Color aColor)
            : base((UInt16)(21 + aSpeaker.Length + aHearer.Length + aWords.Length))
        {
            if (aSpeaker.Length > MAX_NAME_SIZE)
                throw new ArgumentException("The name of the speaker is too long.");

            if (aHearer.Length > MAX_NAME_SIZE)
                throw new ArgumentException("The name of the hearer is too long.");

            if (aWords.Length > MAX_WORDS_SIZE)
                throw new ArgumentException("The words are too long.");

            Color = aColor;
            Channel = aChannel;
            Style = (UInt16)WordsStyle.None;
            Timestamp = Environment.TickCount;

            __StrPacker = new StringPacker(this, 16);
            __StrPacker.AddString(aSpeaker);
            __StrPacker.AddString(aHearer);
            __StrPacker.AddString(""); // Emotion
            __StrPacker.AddString(aWords);
        }

        public MsgTalk(Player aSpeaker, String aHearer, String aWords, Channel aChannel, Color aColor)
            : this(aSpeaker.Name, aHearer, aWords, aChannel, aColor)
        {

        }

        /// <summary>
        /// Process the message for the specified client.
        /// </summary>
        /// <param name="aClient">The client who sent the message.</param>
        public override void Process(Client aClient)
        {
            try
            {
                Player player = aClient.Player;

                if (Words.StartsWith("/"))
                {
                    String[] Parts = Words.Split(' ');

                    switch (Parts[0]) // tmp
                    {
                        case "/mm": //Teleport the player to the specified location
                            {
                                const string USAGE = "USAGE: /mm ${mapid} ${x} ${y}";

                                if (Parts.Length != 4)
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                UInt32 mapId = 0;
                                UInt16 x = 0, y = 0;

                                if (!UInt32.TryParse(Parts[1], out mapId) || !UInt16.TryParse(Parts[2], out x) || !UInt16.TryParse(Parts[3], out y))
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                player.Move(UInt32.Parse(Parts[1]), UInt16.Parse(Parts[2]), UInt16.Parse(Parts[3]));
                                return;
                            }
                        case "/scroll": //Teleport the player to the specified map
                            {
                                const string USAGE = "USAGE: /scroll {tc,am,dc,mc,bi,pc,ma,arena}";

                                if (Parts.Length != 2)
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                switch (Parts[1])
                                {
                                    case "tc":
                                        player.Move(1002, 431, 379);
                                        break;
                                    case "am":
                                        player.Move(1020, 567, 576);
                                        break;
                                    case "dc":
                                        player.Move(1000, 500, 650);
                                        break;
                                    case "mc":
                                        player.Move(1001, 316, 642);
                                        break;
                                    case "bi":
                                        player.Move(1015, 723, 573);
                                        break;
                                    case "pc":
                                        player.Move(1011, 190, 271);
                                        break;
                                    case "ma":
                                        player.Move(1036, 292, 236);
                                        break;
                                    case "arena":
                                        player.Move(1005, 52, 69);
                                        break;
                                }
                                return;
                            }
                        case "/lvl":
                            {
                                const string USAGE = "USAGE: /level ${level}";

                                if (Parts.Length != 2)
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                Byte level = 0;
                                if (!Byte.TryParse(Parts[1], out level))
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                if (level == 0 || level > (Database.AllLevExp.Count + 1))
                                    return;

                                player.Level = level;
                                player.Exp = 0;
                                MyMath.GetLevelStats(player);

                                player.Send(new MsgUserAttrib(player, player.Level, MsgUserAttrib.AttributeType.Level));
                                player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                player.Send(new MsgAction(player, 0, MsgAction.Action.UpLev));
                                return;
                            }
                        case "/job":
                            {
                                const string USAGE = "USAGE: /job ${jobid}";

                                if (Parts.Length != 2)
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                Byte jobid = 0;
                                if (!Byte.TryParse(Parts[1], out jobid))
                                {
                                    player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                    return;
                                }

                                if (jobid == 0 || jobid % 10 > 5)
                                    return;

                                player.Profession = jobid;
                                MyMath.GetLevelStats(player);

                                player.Send(new MsgUserAttrib(player, player.Profession, MsgUserAttrib.AttributeType.Profession));
                                player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                return;
                            }
                        case "/reallot":
                            {
                                MyMath.GetLevelStats(player);
                                player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                return;
                            }
                        case "/money":
                            {
                                UInt32 money = UInt32.Parse(Parts[1]);
                                if (money < 0)
                                    money = 0;

                                if (money > Player._MAX_MONEYLIMIT)
                                    money = Player._MAX_MONEYLIMIT;

                                player.Money = money;
                                player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                                return;
                            }
                        case "/item":
                            {
                                if (player.ItemInInventory() > 39)
                                    return;

                                Int32 itemId = -1;
                                String name = Parts[1].Replace("'", "`");

                                foreach (Item.Info info in Database.AllItems.Values)
                                {
                                    if (info.Name.ToLower() == name.ToLower())
                                    {
                                        itemId = info.ID;
                                        break;
                                    }
                                }

                                if (itemId < 0)
                                    return;

                                itemId = (itemId - (itemId % 10)) + Byte.Parse(Parts[2]);

                                Item item = Item.Create(player.UniqId, 0, itemId, Byte.Parse(Parts[3]), Byte.Parse(Parts[4]), Byte.Parse(Parts[5]), Byte.Parse(Parts[6]), Byte.Parse(Parts[7]), 2, 0, ItemHandler.GetMaxDura(itemId), ItemHandler.GetMaxDura(itemId));
                                player.AddItem(item, true);
                                return;
                            }
                        case "/prof":
                            {
                                UInt16 Type = UInt16.Parse(Parts[1]);
                                SByte Level = SByte.Parse(Parts[2]);
                                UInt32 Exp = 0;
                                WeaponSkill WeaponSkill = null;

                                if (Level > Database.AllWeaponSkillExp.Count)
                                    Level = (SByte)Database.AllWeaponSkillExp.Count;

                                if (Level < -1)
                                    Level = -1;

                                if (Level == -1)
                                {
                                    WeaponSkill = player.GetWeaponSkillByType(Type);
                                    if (WeaponSkill != null)
                                        player.DropSkill(WeaponSkill, true);
                                }
                                else
                                {
                                    WeaponSkill = player.GetWeaponSkillByType(Type);
                                    if (WeaponSkill != null)
                                        player.DropSkill(WeaponSkill, true);

                                    WeaponSkill = WeaponSkill.Create(player, Type, (Byte)Level);
                                    if (WeaponSkill != null)
                                        player.AwardSkill(WeaponSkill, true);
                                }
                                return;
                            }
                        case "/skill":
                            {
                                UInt16 Type = UInt16.Parse(Parts[1]);
                                SByte Level = SByte.Parse(Parts[2]);
                                Magic Magic = null;

                                if (Level > 9)
                                    Level = 9;

                                if (Level < -1)
                                    Level = -1;

                                if (Level == -1)
                                {
                                    Magic = player.GetMagicByType(Type);
                                    if (Magic != null)
                                        player.DropMagic(Magic, true);
                                }
                                else
                                {
                                    if (!Database.AllMagics.ContainsKey((Type * 10) + Level))
                                        return;

                                    Magic = player.GetMagicByType(Type);
                                    if (Magic != null)
                                        player.DropMagic(Magic, true);

                                    Magic = Magic.Create(player, Type, (Byte)Level);
                                    if (Magic != null)
                                        player.AwardMagic(Magic, true);
                                }
                                return;
                            }
                    }


                    #region Game Master
                    if (player.IsGM || player.IsPM)
                    {
                        switch (Parts[0])
                        {
                            case "/ban": //Ban the specified character
                                {
                                    const string USAGE = "USAGE: /ban ${name}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    if (Database.Ban(Parts[1]))
                                    {
                                        Program.Log("[CRIME] " + Parts[1] + " has been banned by " + player.Name + "!");
                                        World.BroadcastMsg(new MsgTalk("SYSTEM", "ALLUSERS", Parts[1] + " has been banned!", Channel.GM, Color.White));

                                        String name = Parts[1];
                                        Player target = null;

                                        if (World.AllPlayerNames.TryGetValue(name, out target))
                                            target.Disconnect();
                                    }
                                    return;
                                }
                            case "/jail": // Jail the specified character
                                {
                                    const string USAGE = "USAGE: /jail ${name}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    String name = Parts[1];
                                    if (Database.SendPlayerToJail(name))
                                        Program.Log("[CRIME] {0} has been sent to jail by {1}!", name, player.Name);

                                    return;
                                }
                            case "/clearinv": //Delete all items in inventory
                                {
                                    Item[] items = null;

                                    lock (player.Items)
                                    {
                                        items = new Item[player.Items.Count];
                                        player.Items.Values.CopyTo(items, 0);
                                    }

                                    foreach (Item item in items)
                                    {
                                        if (item.Position != 0)
                                            continue;

                                        player.DelItem(item.Id, true);
                                    }
                                    return;
                                }
                            case "/rez": //Help someone to reborn...
                                {
                                    const string USAGE = "USAGE: /rez $[name]";

                                    if (Parts.Length < 1 || Parts.Length > 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    if (Parts.Length == 1)
                                    {
                                        if (!player.IsAlive())
                                            player.Reborn(false);
                                        return;
                                    }

                                    String name = Parts[1];
                                    Player target = null;

                                    if (World.AllPlayerNames.TryGetValue(name, out target))
                                    {
                                        if (!target.IsAlive())
                                            target.Reborn(false);

                                        break;
                                    }
                                    return;
                                }
                            case "/recall": //Move someone...
                                {
                                    const string USAGE = "USAGE: /recall ${name}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    String name = Parts[1];
                                    Player target = null;

                                    if (World.AllPlayerNames.TryGetValue(name, out target))
                                        target.Move(player.Map.Id, (UInt16)(player.X + 1), (UInt16)(player.Y + 1));

                                    return;
                                }
                            case "/goto": //Go to someone...
                                {
                                    const string USAGE = "USAGE: /goto ${name}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    String name = Parts[1];
                                    Player target = null;

                                    if (World.AllPlayerNames.TryGetValue(name, out target))
                                        player.Move(target.Map.Id, (UInt16)(target.X + 1), (UInt16)(target.Y + 1));

                                    return;
                                }
                            case "/hp": //Refill the HP to maximum
                                {
                                    player.CurHP = player.MaxHP;
                                    player.Send(new MsgUserAttrib(player, player.CurHP, MsgUserAttrib.AttributeType.Life));
                                    return; 
                                }
                            case "/mp": //Refill the MP to maximum
                                {
                                    player.CurMP = player.MaxMP;
                                    player.Send(new MsgUserAttrib(player, player.CurMP, MsgUserAttrib.AttributeType.Mana));
                                    return;
                                }
                            case "/mm": //Teleport the player to the specified location
                                {
                                    const string USAGE = "USAGE: /mm ${mapid} ${x} ${y}";

                                    if (Parts.Length != 4)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    UInt32 mapId = 0;
                                    UInt16 x = 0, y = 0;

                                    if (!UInt32.TryParse(Parts[1], out mapId) || !UInt16.TryParse(Parts[2], out x) || !UInt16.TryParse(Parts[3], out y))
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    player.Move(UInt32.Parse(Parts[1]), UInt16.Parse(Parts[2]), UInt16.Parse(Parts[3]));
                                    return;
                                }
                            case "/gm": //Talk in the GM channel...
                                {
                                    World.BroadcastMsg(player, new MsgTalk(Speaker, "ALLUSERS", Words.Remove(0, 4), Channel.GM, Color.Lime), true);
                                    return;
                                }
                            case "/scroll": //Teleport the player to the specified map
                                {
                                    const string USAGE = "USAGE: /scroll {tc,am,dc,mc,bi,pc,ma,arena}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    switch (Parts[1])
                                    {
                                        case "tc":
                                            player.Move(1002, 431, 379);
                                            break;
                                        case "am":
                                            player.Move(1020, 567, 576);
                                            break;
                                        case "dc":
                                            player.Move(1000, 500, 650);
                                            break;
                                        case "mc":
                                            player.Move(1001, 316, 642);
                                            break;
                                        case "bi":
                                            player.Move(1015, 723, 573);
                                            break;
                                        case "pc":
                                            player.Move(1011, 190, 271);
                                            break;
                                        case "ma":
                                            player.Move(1036, 292, 236);
                                            break;
                                        case "arena":
                                            player.Move(1005, 52, 69);
                                            break;
                                    }
                                    return;
                                }
                        }
                    }
                    #endregion
                    #region Project Master
                    if (player.IsPM)
                    {
                        switch (Parts[0])
                        {
                            case "/restart": //Restart the emulator...
                                {
                                    Server.Restart();
                                    return;
                                }
                            case "/kill":
                                {
                                    const string USAGE = "USAGE: /kill ${name}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    String name = Parts[1];
                                    Player target = null;

                                    if (World.AllPlayerNames.TryGetValue(name, out target))
                                    {
                                        if (target.IsAlive())
                                            target.Die(null);
                                    }
                                    return;
                                }
                            case "/lvl":
                                {
                                    const string USAGE = "USAGE: /level ${level}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    Byte level = 0;
                                    if (!Byte.TryParse(Parts[1], out level))
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    if (level == 0 || level > (Database.AllLevExp.Count + 1))
                                        return;

                                    player.Level = level;
                                    player.Exp = 0;
                                    MyMath.GetLevelStats(player);

                                    player.Send(new MsgUserAttrib(player, player.Level, MsgUserAttrib.AttributeType.Level));
                                    player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                    player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                    player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                    player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                    player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                    player.Send(new MsgAction(player, 0, MsgAction.Action.UpLev));
                                    return;
                                }
                            case "/job":
                                {
                                    const string USAGE = "USAGE: /job ${jobid}";

                                    if (Parts.Length != 2)
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    Byte jobid = 0;
                                    if (!Byte.TryParse(Parts[1], out jobid))
                                    {
                                        player.Send(new MsgTalk("SYSTEM", "ALLUSERS", USAGE, Channel.Talk, Color.Red));
                                        return;
                                    }

                                    if (jobid == 0 || jobid % 10 > 5)
                                        return;

                                    player.Profession = jobid;
                                    MyMath.GetLevelStats(player);

                                    player.Send(new MsgUserAttrib(player, player.Profession, MsgUserAttrib.AttributeType.Profession));
                                    player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                    player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                    player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                    player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                    player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                    return;
                                }
                            case "/reallot":
                                {
                                    MyMath.GetLevelStats(player);
                                    player.Send(new MsgUserAttrib(player, player.Strength, MsgUserAttrib.AttributeType.Strength));
                                    player.Send(new MsgUserAttrib(player, player.Agility, MsgUserAttrib.AttributeType.Agility));
                                    player.Send(new MsgUserAttrib(player, player.Vitality, MsgUserAttrib.AttributeType.Vitality));
                                    player.Send(new MsgUserAttrib(player, player.Spirit, MsgUserAttrib.AttributeType.Spirit));
                                    player.Send(new MsgUserAttrib(player, player.AddPoints, MsgUserAttrib.AttributeType.AddPoints));
                                    return;
                                }
                            case "/xp":
                                {
                                    player.XP = 99;
                                    return;
                                }
                            case "/money":
                                {
                                    UInt32 money = UInt32.Parse(Parts[1]);
                                    if (money < 0)
                                        money = 0;

                                    if (money > Player._MAX_MONEYLIMIT)
                                        money = Player._MAX_MONEYLIMIT;

                                    player.Money = money;
                                    player.Send(new MsgUserAttrib(player, player.Money, MsgUserAttrib.AttributeType.Money));
                                    return;
                                }
                            case "/item":
                                {
                                    if (player.ItemInInventory() > 39)
                                        return;

                                    Int32 itemId = -1;
                                    String name = Parts[1].Replace("'", "`");

                                    foreach (Item.Info info in Database.AllItems.Values)
                                    {
                                        if (info.Name.ToLower() == name.ToLower())
                                        {
                                            itemId = info.ID;
                                            break;
                                        }
                                    }

                                    if (itemId < 0)
                                        return;

                                    itemId = (itemId - (itemId % 10)) + Byte.Parse(Parts[2]);
     
                                    Item item = Item.Create(player.UniqId, 0, itemId, Byte.Parse(Parts[3]), Byte.Parse(Parts[4]), Byte.Parse(Parts[5]), Byte.Parse(Parts[6]), Byte.Parse(Parts[7]), 2, 0, ItemHandler.GetMaxDura(itemId), ItemHandler.GetMaxDura(itemId));
                                    player.AddItem(item, true);
                                    return;
                                }
                            case "/prof":
                                {
                                    UInt16 Type = UInt16.Parse(Parts[1]);
                                    SByte Level = SByte.Parse(Parts[2]);
                                    UInt32 Exp = 0;
                                    WeaponSkill WeaponSkill = null;

                                    if (Level > Database.AllWeaponSkillExp.Count)
                                        Level = (SByte)Database.AllWeaponSkillExp.Count;

                                    if (Level < -1)
                                        Level = -1;

                                    if (Level == -1)
                                    {
                                        WeaponSkill = player.GetWeaponSkillByType(Type);
                                        if (WeaponSkill != null)
                                            player.DropSkill(WeaponSkill, true);
                                    }
                                    else
                                    {
                                        WeaponSkill = player.GetWeaponSkillByType(Type);
                                        if (WeaponSkill != null)
                                            player.DropSkill(WeaponSkill, true);

                                        WeaponSkill = WeaponSkill.Create(player, Type, (Byte)Level);
                                        if (WeaponSkill != null)
                                            player.AwardSkill(WeaponSkill, true);
                                    }
                                    return;
                                }
                            case "/skill":
                                {
                                    UInt16 Type = UInt16.Parse(Parts[1]);
                                    SByte Level = SByte.Parse(Parts[2]);
                                    Magic Magic = null;

                                    if (Level > 9)
                                        Level = 9;

                                    if (Level < -1)
                                        Level = -1;

                                    if (Level == -1)
                                    {
                                        Magic = player.GetMagicByType(Type);
                                        if (Magic != null)
                                            player.DropMagic(Magic, true);
                                    }
                                    else
                                    {
                                        if (!Database.AllMagics.ContainsKey((Type * 10) + Level))
                                            return;

                                        Magic = player.GetMagicByType(Type);
                                        if (Magic != null)
                                            player.DropMagic(Magic, true);

                                        Magic = Magic.Create(player, Type, (Byte)Level);
                                        if (Magic != null)
                                            player.AwardMagic(Magic, true);
                                    }
                                    return;
                                }
                        }
                    }
                    #endregion
                    player.SendSysMsg(StrRes.STR_COMMAND_NOT_FOUND);
                    return;
                }

                switch (Channel)
                {
                    case Channel.Normal:
                        {
                            World.BroadcastRoomMsg(player, this, false);
                            break;
                        }
                    case Channel.Private:
                        {
                            if (World.AllPlayerNames.ContainsKey(Hearer))
                                World.AllPlayerNames[Hearer].Send(new MsgTalk(player, Hearer, Words, Channel.Private, Color.White));
                            else
                                player.SendSysMsg(StrRes.STR_NOT_ONLINE);
                            break;
                        }
                    case Channel.Team:
                        {
                            if (player.Team == null)
                                break;

                            World.BroadcastTeamMsg(player, this, false);
                            break;
                        }
                    case Channel.Syndicate:
                        {
                            if (player.Syndicate == null)
                                break;

                            World.BroadcastSynMsg(player, this, false);
                            break;
                        }
                    case Channel.Friend:
                        {
                            World.BroadcastFriendMsg(player, this);
                            break;
                        }
                    case Channel.Ghost:
                        {
                            if (player.Screen == null)
                                return;

                            var players = from entity in player.Screen.mEntities.Values where entity.IsPlayer() select (Player)entity;
                            foreach (Player target in players)
                            {
                                if (!target.IsAlive() || (target.Profession >= 132 && target.Profession <= 135))
                                    target.Send(this);
                            }
                            break;
                        }
                    case Channel.Serve:
                        {
                            var players = from entity in World.AllPlayers.Values where (entity.IsGM || entity.IsPM) select entity;
                            foreach (Player target in players)
                                target.Send(this);
                            break;
                        }
                    case Channel.CryOut:
                        {
                            if (player.Booth == null)
                                break;

                            player.Booth.SetCryOut(Words);
                            break;
                        }
                    case Channel.SynAnnounce:
                        {
                            if (player.Syndicate == null)
                                break;

                            if (player.Syndicate.Leader.Id != player.UniqId)
                                break;

                            player.Syndicate.Announce = Words;
                            World.BroadcastSynMsg(player.Syndicate, this);
                            break;
                        }
                    case Channel.MsgTrade:
                        {
                            MessageBoard.MessageInfo message =
                                World.TradeBoard.GetMsgInfoByAuthor(player.Name);

                            World.TradeBoard.Delete(message);
                            World.TradeBoard.Add(player.Name, Words);
                            break;
                        }
                    case Channel.MsgFriend:
                        {
                            MessageBoard.MessageInfo message =
                                World.FriendBoard.GetMsgInfoByAuthor(player.Name);

                            World.FriendBoard.Delete(message);
                            World.FriendBoard.Add(player.Name, Words);
                            break;
                        }
                    case Channel.MsgTeam:
                        {
                            MessageBoard.MessageInfo message =
                                World.TeamBoard.GetMsgInfoByAuthor(player.Name);

                            World.TeamBoard.Delete(message);
                            World.TeamBoard.Add(player.Name, Words);
                            break;
                        }
                    case Channel.MsgSyn:
                        {
                            MessageBoard.MessageInfo message =
                                World.SynBoard.GetMsgInfoByAuthor(player.Name);

                            World.SynBoard.Delete(message);
                            World.SynBoard.Add(player.Name, Words);
                            break;
                        }
                    case Channel.MsgOther:
                        {
                            MessageBoard.MessageInfo message =
                                World.OtherBoard.GetMsgInfoByAuthor(player.Name);

                            World.OtherBoard.Delete(message);
                            World.OtherBoard.Add(player.Name, Words);
                            break;
                        }
                    case Channel.MsgSystem:
                        {
                            MessageBoard.MessageInfo message =
                                World.SystemBoard.GetMsgInfoByAuthor(player.Name);

                            World.SystemBoard.Delete(message);
                            World.SystemBoard.Add(player.Name, Words);
                            break;
                        }
                    default:
                        {
                            sLogger.Error("Channel {0} is not implemented for MsgTalk.", (UInt16)Channel);
                            break;
                        }
                }
            }
            catch (Exception exc) { sLogger.Error(exc); }
        }
    }
}
