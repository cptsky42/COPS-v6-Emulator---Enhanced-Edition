using System;
using System.IO;
using System.Text;
using CO2_CORE_DLL;

namespace Logik.Script
{
    public class KCOSS : KCS
    {
        public KCOSS(String File)
            : base()
        {
            try
            {
                Ini Reader = new Ini(File);

                m_Header = new Header();
                m_Header.UniqId = Reader.ReadInt32("Header", "UniqId");
                m_Header.Name = Reader.ReadValue("Header", "Name");
                m_Header.Face = Reader.ReadInt16("Header", "Face");

                for (byte i = 0; i < 255; i++)
                {
                    if (!Reader.KeyExist("Page" + i.ToString(), "Count"))
                    {
                        m_Binders = new Binder[i];
                        break;
                    }
                }

                for (byte i = 0; i < m_Binders.Length; i++)
                {
                    m_Binders[i].Pages = new Page[Reader.ReadUInt8("Page" + i.ToString(), "Count")];
                    for (byte x = 0; x < m_Binders[i].Pages.Length; x++)
                    {
                        Page Page = new Page();
                        string[] Options;

                        if (Reader.KeyExist("Page" + i.ToString(), "Req" + x.ToString()))
                        {
                            string ReqLine = Reader.ReadValue("Page" + i.ToString(), "Req" + x.ToString());
                            string[] Reqs = ReqLine.Split('&');
                            Page.Requirements = new Requirement[Reqs.Length];
                            for (int y = 0; y < Reqs.Length; y++)
                                Page.Requirements[y] = ConvertReq(Reqs[y]);
                        }
                        else
                            Page.Requirements = new Requirement[0];

                        if (Reader.KeyExist("Page" + i.ToString(), "Rew" + x.ToString()))
                        {
                            string RewLine = Reader.ReadValue("Page" + i.ToString(), "Rew" + x.ToString());
                            string[] Rews = RewLine.Split('&');
                            Page.Rewards = new Reward[Rews.Length];
                            for (int y = 0; y < Rews.Length; y++)
                                Page.Rewards[y] = ConvertRew(Rews[y]);
                        }
                        else
                            Page.Rewards = new Reward[0];

                        if (Reader.KeyExist("Page" + i.ToString(), "Text" + x.ToString()))
                        {
                            Page.Text = Reader.ReadValue("Page" + i.ToString(), "Text" + x.ToString());
                            if (Page.Text.Length > 2000)
                                throw new Exception("Page[" + i.ToString() + "], Text[" + x.ToString() + "] : Too long!");
                        }
                        else
                            Page.Text = null;

                        if (Reader.KeyExist("Page" + i.ToString(), "Opt" + x.ToString()))
                        {
                            Options = GetOptions(Reader.ReadValue("Page" + i.ToString(), "Opt" + x.ToString()));
                            byte OptC = 0;
                            foreach (string Option in Options)
                            {
                                if (Option != null)
                                    OptC++;
                            }
                            Page.Options = new Option[OptC];
                            byte Pos = 0;
                            for (int y = 0; y < Options.Length; y++)
                            {
                                if (Options[y] != null)
                                {
                                    Page.Options[Pos] = new Option();
                                    Page.Options[Pos].UniqId = (byte)y;
                                    Page.Options[Pos].Text = Options[y];

                                    if (Page.Options[Pos].Text.Length > 250)
                                        throw new Exception("Page[" + i.ToString() + "], Opt[" + x.ToString() + "], UID[" + y + "] : Too long!");

                                    Pos++;
                                }
                            }
                        }
                        else
                            Page.Options = new Option[0];
                        m_Binders[i].Pages[x] = Page;
                    }
                }
            }
            catch (Exception Exc) { Console.WriteLine(Exc.ToString()); }
        }

        private Requirement ConvertReq(string Req)
        {
            Requirement Requirement = new Requirement();
            string[] ReqPart = Req.TrimStart(' ').Split(' ');
            if (ReqPart.Length >= 3)
            {
                #region Type
                switch (ReqPart[0])
                {
                    case "CurHP":
                        Requirement.Type = (byte)RequirementType.CurHP;
                        break;
                    case "MaxHP":
                        Requirement.Type = (byte)RequirementType.MaxHP;
                        break;
                    case "CurMP":
                        Requirement.Type = (byte)RequirementType.CurMP;
                        break;
                    case "MaxMP":
                        Requirement.Type = (byte)RequirementType.MaxMP;
                        break;
                    case "Money":
                        Requirement.Type = (byte)RequirementType.Money;
                        break;
                    case "Exp":
                        Requirement.Type = (byte)RequirementType.Exp;
                        break;
                    case "PKPoint":
                        Requirement.Type = (byte)RequirementType.PkPoints;
                        break;
                    case "Job":
                        Requirement.Type = (byte)RequirementType.Profession;
                        break;
                    case "StatP":
                        Requirement.Type = (byte)RequirementType.AddPoints;
                        break;
                    case "Model":
                        Requirement.Type = (byte)RequirementType.Look;
                        break;
                    case "Level":
                        Requirement.Type = (byte)RequirementType.Level;
                        break;
                    case "Spirit":
                        Requirement.Type = (byte)RequirementType.Spirit;
                        break;
                    case "Vitality":
                        Requirement.Type = (byte)RequirementType.Vitality;
                        break;
                    case "Strength":
                        Requirement.Type = (byte)RequirementType.Strength;
                        break;
                    case "Agility":
                        Requirement.Type = (byte)RequirementType.Agility;
                        break;
                    case "Hair":
                        Requirement.Type = (byte)RequirementType.Hair;
                        break;
                    case "CPs":
                        Requirement.Type = (byte)RequirementType.CPs;
                        break;
                    case "VPs":
                        Requirement.Type = (byte)RequirementType.VPs;
                        break;
                    case "InvContains":
                        Requirement.Type = (byte)RequirementType.InvContains;
                        break;
                    case "InvCount":
                        Requirement.Type = (byte)RequirementType.InvCount;
                        break;
                    case "SkillContains":
                        Requirement.Type = (byte)RequirementType.SkillContains;
                        break;
                    case "ProfContains":
                        Requirement.Type = (byte)RequirementType.ProfContains;
                        break;
                }
                #endregion
                #region Operator
                switch (ReqPart[1])
                {
                    case "<":
                        Requirement.Operator = (byte)Operator.LessThan;
                        break;
                    case ">":
                        Requirement.Operator = (byte)Operator.GreaterThan;
                        break;
                    case "<=":
                        Requirement.Operator = (byte)Operator.LessThanOrEqual;
                        break;
                    case ">=":
                        Requirement.Operator = (byte)Operator.GreaterThanOrEqual;
                        break;
                    case "==":
                        Requirement.Operator = (byte)Operator.Equal;
                        break;
                    case "!=":
                        Requirement.Operator = (byte)Operator.Inequal;
                        break;
                }
                #endregion
                #region Value
                if (!Int32.TryParse(ReqPart[2], out Requirement.IntValue))
                    Requirement.StrValue = ReqPart[2];
                #endregion
            }

            return Requirement;
        }

        private Reward ConvertRew(string Rew)
        {
            Reward Reward = new Reward();
            string[] RewPart = Rew.TrimStart(' ').Replace("]", "").Split('[');
            if (RewPart.Length >= 2)
            {
                #region Type
                switch (RewPart[0])
                {
                    case "CurHP":
                        Reward.Type = (byte)RewardType.CurHP;
                        break;
                    case "MaxHP":
                        Reward.Type = (byte)RewardType.MaxHP;
                        break;
                    case "CurMP":
                        Reward.Type = (byte)RewardType.CurMP;
                        break;
                    case "MaxMP":
                        Reward.Type = (byte)RewardType.MaxMP;
                        break;
                    case "Money":
                        Reward.Type = (byte)RewardType.Money;
                        break;
                    case "Exp":
                        Reward.Type = (byte)RewardType.Exp;
                        break;
                    case "PKPoint":
                        Reward.Type = (byte)RewardType.PkPoints;
                        break;
                    case "Job":
                        Reward.Type = (byte)RewardType.Profession;
                        break;
                    case "StatP":
                        Reward.Type = (byte)RewardType.AddPoints;
                        break;
                    case "Model":
                        Reward.Type = (byte)RewardType.Look;
                        break;
                    case "Level":
                        Reward.Type = (byte)RewardType.Level;
                        break;
                    case "Spirit":
                        Reward.Type = (byte)RewardType.Spirit;
                        break;
                    case "Vitality":
                        Reward.Type = (byte)RewardType.Vitality;
                        break;
                    case "Strength":
                        Reward.Type = (byte)RewardType.Strength;
                        break;
                    case "Agility":
                        Reward.Type = (byte)RewardType.Agility;
                        break;
                    case "Hair":
                        Reward.Type = (byte)RewardType.Hair;
                        break;
                    case "CPs":
                        Reward.Type = (byte)RewardType.CPs;
                        break;
                    case "VPs":
                        Reward.Type = (byte)RewardType.VPs;
                        break;
                    case "AddItem":
                        Reward.Type = (byte)RewardType.AddItem;
                        break;
                    case "DelItem":
                        Reward.Type = (byte)RewardType.DelItem;
                        break;
                    case "Teleport":
                        Reward.Type = (byte)RewardType.Teleport;
                        break;
                    case "AddSkill":
                        Reward.Type = (byte)RewardType.AddSkill;
                        break;
                    case "DelSkill":
                        Reward.Type = (byte)RewardType.DelSkill;
                        break;
                    case "AddProf":
                        Reward.Type = (byte)RewardType.AddProf;
                        break;
                    case "DelProf":
                        Reward.Type = (byte)RewardType.DelProf;
                        break;
                    case "RemoteControl":
                        Reward.Type = (byte)RewardType.RemoteControl;
                        break;
                    case "RandOpt":
                        Reward.Type = (byte)RewardType.RandOpt;
                        break;
                }
                #endregion
                #region Operator
                if (RewPart[1].StartsWith("+"))
                {
                    Reward.Operator = (byte)Operator.Addition;
                    RewPart[1] = RewPart[1].Replace("+", "");
                }
                else if (RewPart[1].StartsWith("-"))
                {
                    Reward.Operator = (byte)Operator.Subtraction;
                    RewPart[1] = RewPart[1].Replace("-", "");
                }
                else if (RewPart[1].StartsWith("*"))
                {
                    Reward.Operator = (byte)Operator.Multiplication;
                    RewPart[1] = RewPart[1].Replace("*", "");
                }
                else if (RewPart[1].StartsWith("/"))
                {
                    Reward.Operator = (byte)Operator.Divison;
                    RewPart[1] = RewPart[1].Replace("/", "");
                }
                else
                    Reward.Operator = (byte)0xFF;
                #endregion
                #region Value
                if (!Int32.TryParse(RewPart[1], out Reward.IntValue))
                    Reward.StrValue = RewPart[1];
                #endregion
            }

            return Reward;
        }

        private string[] GetOptions(string AllOptions)
        {
            try
            {
                string[] Options = new string[0x100];
                string[] OptInfo = AllOptions.Split(':');

                for (byte i = 0; i < OptInfo.Length; i++)
                {
                    string[] Opt = OptInfo[i].Split('-');
                    if (Opt.Length > 2)
                    {
                        for (byte x = 0; x < (Opt.Length - 1); x++)
                            Options[byte.Parse(Opt[Opt.Length - 1])] += Opt[x].TrimStart(' ');
                    }
                    else
                        Options[byte.Parse(Opt[1])] = Opt[0].TrimStart(' ');
                }
                return Options;
            }
            catch { return new string[0x100]; }
        }

        ~KCOSS()
        {
            m_Binders = null;
        }
    }
}