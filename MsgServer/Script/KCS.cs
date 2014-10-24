using System;
using System.IO;
using System.Text;
using CO2_CORE_DLL;

namespace Logik.Script
{
    public class KCS
    {
        #region Structures
        public struct Header
        {
            public Int32 UniqId;
            public String Name;
            public Int16 Face;
        }

        public struct Binder
        {
            public Page[] Pages;
        }

        public struct Page
        {
            public Requirement[] Requirements;
            public Reward[] Rewards;
            public String Text;
            public Option[] Options;
        }

        public struct Requirement
        {
            public Byte Type;
            public Byte Operator;
            public Int32 IntValue;
            public String StrValue;
        }

        public struct Reward
        {
            public Byte Type;
            public Byte Operator;
            public Int32 IntValue;
            public String StrValue;
        }

        public struct Option
        {
            public Byte UniqId;
            public String Text;
        }
        #endregion
        #region Enumerations
        public enum Operator : byte
        {
            Addition = 0,           // +
            Subtraction = 1,        // -
            Multiplication = 2,     // *
            Divison = 3,            // /
            LessThan = 10,          // <
            GreaterThan = 11,       // >
            LessThanOrEqual = 12,   // <=
            GreaterThanOrEqual = 13,// >=
            Equal = 20,             // ==
            Inequal = 21,           // !=
            None = 255,             // =
        }

        public enum RequirementType : byte
        {
            CurHP = 0,
            MaxHP = 1,
            CurMP = 2,
            MaxMP = 3,
            Money = 4,
            Exp = 5,
            PkPoints = 6,
            Profession = 7,
            AddPoints = 11,
            Look = 12,
            Level = 13,
            Spirit = 14,
            Vitality = 15,
            Strength = 16,
            Agility = 17,
            Hair = 27,
            CPs = 30,
            VPs = 50,
            InvContains = 100,
            InvCount = 101,
            SkillContains = 102,
            ProfContains = 103,
        }

        public enum RewardType : byte
        {
            CurHP = 0,
            MaxHP = 1,
            CurMP = 2,
            MaxMP = 3,
            Money = 4,
            Exp = 5,
            PkPoints = 6,
            Profession = 7,
            AddPoints = 11,
            Look = 12,
            Level = 13,
            Spirit = 14,
            Vitality = 15,
            Strength = 16,
            Agility = 17,
            Hair = 27,
            CPs = 30,
            VPs = 50,
            AddItem = 100,
            DelItem = 101,
            Teleport = 102,
            AddSkill = 103,
            DelSkill = 104,
            AddProf = 105,
            DelProf = 106,
            RemoteControl = 116,
            RandOpt = 120,
        }
        #endregion
        #region Private Members
        protected Header m_Header = new Header();
        protected Binder[] m_Binders = new Binder[0];
        #endregion

        public delegate void GetReward(Reward Reward, Object Obj);
        public delegate Boolean CheckRequirement(Requirement Requirement, Object Obj);

        public delegate Int32 SendText(String Text, Object Obj, ref Byte[] Buffer, Int32 Position);
        public delegate Int32 SendOption(Byte UniqId, String Text, Object Obj, ref Byte[] Buffer, Int32 Position);
        public delegate Int32 SendFace(Int16 Face, Object Obj, ref Byte[] Buffer, Int32 Position);
        public delegate Int32 SendEnd(Object Obj, ref Byte[] Buffer, Int32 Position);
        public delegate void SendData(Object Obj, Byte[] Data, Int32 Length);

        public delegate void GetError(String Error);

        public GetReward OnGetReward;
        public CheckRequirement OnCheckRequirement;

        public SendText OnSendText;
        public SendOption OnSendOption;
        public SendFace OnSendFace;
        public SendEnd OnSendEnd;
        public SendData OnSendData;

        public GetError OnGetError;

        public KCS() { }
        public KCS(String File) { RImport(File, Encoding.Default); }
        public KCS(String File, Encoding Encoding) { RImport(File, Encoding); }

        public void Import(String File) { RImport(File, Encoding.Default); }
        public void Import(String File, Encoding Encoding) { RImport(File, Encoding); }

        private void RImport(String File, Encoding Encoding)
        {
            try
            {
                using (FileStream FStream = new FileStream(File, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryReader BReader = new BinaryReader(FStream);

                    if (Encoding.ASCII.GetString(BReader.ReadBytes(0x03)) == "KCS")
                    {
                        m_Header = new Header()
                        {
                            UniqId = BReader.ReadInt32(),
                            Name = Encoding.Default.GetString(BReader.ReadBytes(32)).TrimEnd((Char)0x00),
                            Face = BReader.ReadInt16()
                        };

                        m_Binders = new Binder[BReader.ReadByte()];
                        for (Byte i = 0; i < m_Binders.Length; i++)
                        {
                            m_Binders[i] = new Binder()
                            {
                                Pages = new Page[BReader.ReadByte()]
                            };

                            for (Byte x = 0; x < m_Binders[i].Pages.Length; x++)
                            {
                                m_Binders[i].Pages[x] = new Page()
                                {
                                    Requirements = new Requirement[BReader.ReadByte()],
                                    Rewards = new Reward[BReader.ReadByte()],
                                    Options = new Option[BReader.ReadByte()],
                                    Text = null
                                };

                                for (Byte y = 0; y < m_Binders[i].Pages[x].Requirements.Length; y++)
                                {
                                    m_Binders[i].Pages[x].Requirements[y] = new Requirement()
                                    {
                                        Type = BReader.ReadByte(),
                                        Operator = BReader.ReadByte()
                                    };
                                    if (BReader.ReadBoolean())
                                        m_Binders[i].Pages[x].Requirements[y].StrValue = Encoding.Default.GetString(BReader.ReadBytes(32)).TrimEnd((Char)0x00);
                                    else
                                        m_Binders[i].Pages[x].Requirements[y].IntValue = BReader.ReadInt32();
                                }

                                for (Byte y = 0; y < m_Binders[i].Pages[x].Rewards.Length; y++)
                                {
                                    m_Binders[i].Pages[x].Rewards[y] = new Reward()
                                    {
                                        Type = BReader.ReadByte(),
                                        Operator = BReader.ReadByte()
                                    };
                                    if (BReader.ReadBoolean())
                                        m_Binders[i].Pages[x].Rewards[y].StrValue = Encoding.Default.GetString(BReader.ReadBytes(32)).TrimEnd((Char)0x00);
                                    else
                                        m_Binders[i].Pages[x].Rewards[y].IntValue = BReader.ReadInt32();
                                }

                                if (BReader.ReadBoolean())
                                    m_Binders[i].Pages[x].Text = Encoding.Default.GetString(BReader.ReadBytes(BReader.ReadInt16())).TrimEnd((Char)0x00);

                                for (Byte y = 0; y < m_Binders[i].Pages[x].Options.Length; y++)
                                {
                                    m_Binders[i].Pages[x].Options[y] = new Option()
                                    {
                                        UniqId = BReader.ReadByte(),
                                        Text = Encoding.Default.GetString(BReader.ReadBytes(BReader.ReadByte())).TrimEnd((Char)0x00)
                                    };
                                }
                            }
                        }
                    }
                    BReader.Close();
                }
            }
            catch (Exception Exc) { Console.WriteLine(Exc.ToString()); }
        }

        public void Export(String File) { RExport(File, Encoding.Default); }
        public void Export(String File, Encoding Encoding) { RExport(File, Encoding); }

        private void RExport(String File, Encoding Encoding)
        {
            try
            {
                using (FileStream FStream = new FileStream(File, FileMode.Create, FileAccess.Write))
                {
                    BinaryWriter BWriter = new BinaryWriter(FStream, Encoding);

                    //Identifier
                    BWriter.Write((Byte)'K'); //Logik.
                    BWriter.Write((Byte)'C'); //Conquer
                    BWriter.Write((Byte)'S'); //Script

                    //Header
                    BWriter.Write(m_Header.UniqId);
                    for (SByte i = 0; i < 32; i++)
                    {
                        if (i < m_Header.Name.Length)
                            BWriter.Write((Byte)m_Header.Name[i]);
                        else
                            BWriter.Write((Byte)0x00);
                    }
                    BWriter.Write(m_Header.Face);

                    //Binders
                    BWriter.Write((Byte)m_Binders.Length);
                    foreach (Binder Binder in m_Binders)
                    {
                        BWriter.Write((Byte)Binder.Pages.Length);
                        foreach (Page Page in Binder.Pages)
                        {
                            BWriter.Write((Byte)Page.Requirements.Length);
                            BWriter.Write((Byte)Page.Rewards.Length);
                            BWriter.Write((Byte)Page.Options.Length);

                            foreach (Requirement Requirement in Page.Requirements)
                            {
                                BWriter.Write(Requirement.Type);
                                BWriter.Write(Requirement.Operator);
                                BWriter.Write(Requirement.StrValue != null ? true : false);
                                if (Requirement.StrValue != null)
                                {
                                    for (SByte i = 0; i < 32; i++)
                                    {
                                        if (i < Requirement.StrValue.Length)
                                            BWriter.Write((Byte)Requirement.StrValue[i]);
                                        else
                                            BWriter.Write((Byte)0x00);
                                    }
                                }
                                else
                                    BWriter.Write(Requirement.IntValue);
                            }

                            foreach (Reward Reward in Page.Rewards)
                            {
                                BWriter.Write(Reward.Type);
                                BWriter.Write(Reward.Operator);
                                BWriter.Write(Reward.StrValue != null ? true : false);
                                if (Reward.StrValue != null)
                                {
                                    for (SByte i = 0; i < 32; i++)
                                    {
                                        if (i < Reward.StrValue.Length)
                                            BWriter.Write((Byte)Reward.StrValue[i]);
                                        else
                                            BWriter.Write((Byte)0x00);
                                    }
                                }
                                else
                                    BWriter.Write(Reward.IntValue);
                            }

                            BWriter.Write(Page.Text != null ? true : false);
                            if (Page.Text != null)
                            {
                                BWriter.Write((UInt16)Page.Text.Length);
                                for (UInt16 i = 0; i < Page.Text.Length; i++)
                                    BWriter.Write((Byte)Page.Text[i]);
                            }

                            foreach (Option Option in Page.Options)
                            {
                                BWriter.Write(Option.UniqId);
                                BWriter.Write((Byte)Option.Text.Length);
                                for (Byte i = 0; i < Option.Text.Length; i++)
                                    BWriter.Write((Byte)Option.Text[i]);
                            }
                        }
                    }

                    BWriter.Close();
                }
            }
            catch (Exception Exc) { Console.WriteLine(Exc.ToString()); Console.Read(); }
        }

        ~KCS()
        {
            m_Binders = null;
        }

        public void Execute(Byte BinderId, Object Obj)
        {
            try
            {
                Byte[] Buffer = new Byte[Kernel.MAX_BUFFER_SIZE];
                Int32 Position = 0;

                if (m_Binders.Length <= BinderId)
                    return;

                for (Byte i = 0; i < m_Binders[BinderId].Pages.Length; i++)
                {
                    Page Page = m_Binders[BinderId].Pages[i];

                    bool ChangePage = false;
                    foreach (Requirement Requirement in Page.Requirements)
                    {
                        if (!OnCheckRequirement(Requirement, Obj))
                        {
                            ChangePage = true;
                            break;
                        }
                    }
                    if (ChangePage)
                        continue;

                    foreach (Reward Reward in Page.Rewards)
                        OnGetReward(Reward, Obj);

                    if (Page.Text != null)
                    {
                        Position += OnSendText(Page.Text, Obj, ref Buffer, Position);

                        foreach (Option Option in Page.Options)
                            Position += OnSendOption(Option.UniqId, Option.Text, Obj, ref Buffer, Position);

                        Position += OnSendFace(m_Header.Face, Obj, ref Buffer, Position);
                        Position += OnSendEnd(Obj, ref Buffer, Position);

                        OnSendData(Obj, Buffer, Position);
                    }

                    Buffer = null;
                    return;
                }
            }
            catch (Exception Exc) { Console.WriteLine(Exc.ToString()); }
        }
    }
}