using System;
using System.IO;
using System.Collections.Generic;
using Logik.Script;

namespace COServer.Script
{
    public partial class ScriptHandler
    {
        public static Dictionary<Int32, KCS>[] AllScripts = new Dictionary<Int32, KCS>[2];

        public static void GetAllScripts()
        {
            try
            {
                String[] Lang = new String[] { "Fr", "En" };

                for (Int32 x = 0; x < Lang.Length; x++)
                {
                    String[] ScriptFiles = Directory.GetFiles(Program.RootPath + "\\Scripts\\" + Lang[x] + "\\KCS\\", "*.kcs");
                    Dictionary<Int32, Int64> Dates = new Dictionary<Int32, Int64>();

                    AllScripts[x] = new Dictionary<Int32, KCS>(ScriptFiles.Length);
                    for (Int32 i = 0; i < ScriptFiles.Length; i++)
                    {
                        try
                        {
                            Int32 UniqId = 0;
                            Int64 Ticks = File.GetLastWriteTimeUtc(ScriptFiles[i]).Ticks;
                            KCS Script = null;

                            Script = new KCS(ScriptFiles[i], Program.Encoding);
                            if (!Int32.TryParse(ScriptFiles[i].Replace(Program.RootPath + "\\Scripts\\" + Lang[x] + "\\KCS\\", "").Replace(".kcs", ""), out UniqId))
                                continue;

                            Script.OnCheckRequirement = new KCS.CheckRequirement(ScriptHandler.CheckRequirement);
                            Script.OnGetReward = new KCS.GetReward(ScriptHandler.GetReward);

                            Script.OnSendText = new KCS.SendText(ScriptHandler.SendText);
                            Script.OnSendOption = new KCS.SendOption(ScriptHandler.SendOption);
                            Script.OnSendFace = new KCS.SendFace(ScriptHandler.SendFace);
                            Script.OnSendEnd = new KCS.SendEnd(ScriptHandler.SendEnd);
                            Script.OnSendData = new KCS.SendData(ScriptHandler.SendData);

                            if (!AllScripts[x].ContainsKey(UniqId))
                            {
                                AllScripts[x].Add(UniqId, Script);
                                Dates.Add(UniqId, Ticks);
                            }
                        }
                        catch (Exception Exc) { Program.WriteLine(Exc.ToString()); }
                    }

                    ScriptFiles = Directory.GetFiles(Program.RootPath + "\\Scripts\\" + Lang[x] + "\\KCOSS\\", "*.ini");
                    for (Int32 i = 0; i < ScriptFiles.Length; i++)
                    {
                        try
                        {
                            Int32 UniqId = 0;
                            Int64 Ticks = File.GetLastWriteTimeUtc(ScriptFiles[i]).Ticks;
                            KCS Script = null;

                            Script = new KCOSS(ScriptFiles[i]);
                            if (!Int32.TryParse(ScriptFiles[i].Replace(Program.RootPath + "\\Scripts\\" + Lang[x] + "\\KCOSS\\", "").Replace(".ini", ""), out UniqId))
                                continue;

                            if (AllScripts[x].ContainsKey(UniqId))
                            {
                                if (Dates[UniqId] < Ticks)
                                    AllScripts[x].Remove(UniqId);
                                else
                                    continue;
                            }

                            Script.OnCheckRequirement = new KCS.CheckRequirement(ScriptHandler.CheckRequirement);
                            Script.OnGetReward = new KCS.GetReward(ScriptHandler.GetReward);

                            Script.OnSendText = new KCS.SendText(ScriptHandler.SendText);
                            Script.OnSendOption = new KCS.SendOption(ScriptHandler.SendOption);
                            Script.OnSendFace = new KCS.SendFace(ScriptHandler.SendFace);
                            Script.OnSendEnd = new KCS.SendEnd(ScriptHandler.SendEnd);
                            Script.OnSendData = new KCS.SendData(ScriptHandler.SendData);

                            if (!AllScripts[x].ContainsKey(UniqId))
                            {
                                AllScripts[x].Add(UniqId, Script);
                                Script.Export(ScriptFiles[i].Replace("KCOSS", "KCS").Replace(".ini", ".kcs"), Program.Encoding);
                            }
                        }
                        catch (Exception Exc) { Program.WriteLine(Exc.ToString()); }
                    }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc.ToString()); }
        }
    }
}