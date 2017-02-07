using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace Client_Updater
{
    public class ClientUpdater : Constans
    {
        private readonly string domain;
        private readonly Label lb;

        private string orginalFilename;
        private string[] files;
        private string client1 = "client-1";

        public ClientUpdater(string domain, Label toUpdate)
        {
            this.lb = toUpdate;
            this.domain = domain;
        }

        public void UpdateClient()
        {
            Decompile();
            ExportXmls();
            ExportPacketIds();
            ReplaceHttpsWithHttp();
            ReplaceDomain();
            ReplaceRSA();
            ChangeCopyright();
            FixEntityClass();
            EnableRemoteTexture();
            Recompile();
            DeleteFolders();
            UpdateLabel("Client done!");
        }

        private string GetPacketIdName(int slotid)
        {
            switch (slotid)
            {
                case 1: return "FAILURE";
                case 2: return "CREATE_SUCCESS";
                case 3: return "CREATE";
                case 4: return "PLAYERSHOOT";
                case 5: return "MOVE";
                case 6: return "PLAYERTEXT";
                case 7: return "TEXT";
                case 8: return "SERVERPLAYERSHOOT";
                case 9: return "DAMAGE";
                case 10: return "UPDATE";
                case 11: return "UPDATEACK";
                case 12: return "NOTIFICATION";
                case 13: return "NEWTICK";
                case 14: return "INVSWAP";
                case 15: return "USEITEM";
                case 16: return "SHOWEFFECT";
                case 17: return "HELLO";
                case 18: return "GOTO";
                case 19: return "INVDROP";
                case 20: return "INVRESULT";
                case 21: return "RECONNECT";
                case 22: return "PING";
                case 23: return "PONG";
                case 24: return "MAPINFO";
                case 25: return "LOAD";
                case 26: return "PIC";
                case 27: return "SETCONDITION";
                case 28: return "TELEPORT";
                case 29: return "USEPORTAL";
                case 30: return "DEATH";
                case 31: return "BUY";
                case 32: return "BUYRESULT";
                case 33: return "AOE";
                case 34: return "GROUNDDAMAGE";
                case 35: return "PLAYERHIT";
                case 36: return "ENEMYHIT";
                case 37: return "AOEACK";
                case 38: return "SHOOTACK";
                case 39: return "OTHERHIT";
                case 40: return "SQUAREHIT";
                case 41: return "GOTOACK";
                case 42: return "EDITACCOUNTLIST";
                case 43: return "ACCOUNTLIST";
                case 44: return "QUESTOBJID";
                case 45: return "CHOOSENAME";
                case 46: return "NAMERESULT";
                case 47: return "CREATEGUILD";
                case 48: return "GUILDRESULT";
                case 49: return "GUILDREMOVE";
                case 50: return "GUILDINVITE";
                case 51: return "ALLYSHOOT";
                case 52: return "ENEMYSHOOT";
                case 53: return "REQUESTTRADE";
                case 54: return "TRADEREQUESTED";
                case 55: return "TRADESTART";
                case 56: return "CHANGETRADE";
                case 57: return "TRADECHANGED";
                case 58: return "ACCEPTTRADE";
                case 59: return "CANCELTRADE";
                case 60: return "TRADEDONE";
                case 61: return "TRADEACCEPTED";
                case 62: return "CLIENTSTAT";
                case 63: return "CHECKCREDITS";
                case 64: return "ESCAPE";
                case 65: return "FILE";
                case 66: return "INVITEDTOGUILD";
                case 67: return "JOINGUILD";
                case 68: return "CHANGEGUILDRANK";
                case 69: return "PLAYSOUND";
                case 70: return "GLOBAL_NOTIFICATION";
                case 71: return "RESKIN";
                case 72: return "PETUPGRADEREQUEST";
                case 73: return "ACTIVE_PET_UPDATE_REQUEST";
                case 74: return "ACTIVEPETUPDATE";
                case 75: return "NEW_ABILITY";
                case 76: return "PETYARDUPDATE";
                case 77: return "EVOLVE_PET";
                case 78: return "DELETE_PET";
                case 79: return "HATCH_PET";
                case 80: return "ENTER_ARENA";
                case 81: return "IMMINENT_ARENA_WAVE";
                case 82: return "ARENA_DEATH";
                case 83: return "ACCEPT_ARENA_DEATH";
                case 84: return "VERIFY_EMAIL";
                case 85: return "RESKIN_UNLOCK";
                case 86: return "PASSWORD_PROMPT";
                case 87: return "QUEST_FETCH_ASK";
                case 88: return "QUEST_REDEEM";
                case 89: return "QUEST_FETCH_RESPONSE";
                case 90: return "QUEST_REDEEM_RESPONSE";
                case 91: return "PET_CHANGE_FORM_MSG";
                case 92: return "KEY_INFO_REQUEST";
                case 93: return "KEY_INFO_RESPONSE";
            }
            return null;
        }

        private void ExportPacketIds()
        {
            UpdateLabel("Searching for packet ids");
            string targetFile = String.Empty;
            string packetIdText =
@"//This file was generated by ossimc82's automatic ""Prod-Client to PServer-Client"" builder!
namespace wServer
{
    public enum PacketID : byte
    {";
            Dictionary<int, KeyValuePair<string, int>> PacketIdCollection = new Dictionary<int, KeyValuePair<string, int>>();
            foreach (var file in files)
            {
                using (StreamReader rdr = new StreamReader(file))
                {
                    string text = rdr.ReadToEnd();
                    if (text.Contains(PACKETID_FAILURE) && text.Contains(PACKETID_CREATESUCCESS))
                    {
                        targetFile = file;
                        break;
                    }
                }
            }
            if (!String.IsNullOrWhiteSpace(targetFile))
            {
                using (StreamReader rdr = new StreamReader(targetFile))
                {
                    bool started = false;
                    while (true)
                    {
                        if (rdr.EndOfStream) break;
                        string line = rdr.ReadLine();
                        if (line.Contains("FAILURE") && line.Contains("trait const"))
                            started = true;

                        if (started)
                        {
                            if (line.Contains("trait const"))
                            {
                                int slotId = int.Parse((line[line.IndexOf("slotid") + "slotid".Length + 1].ToString() + line[line.IndexOf("slotid") + "slotid".Length + 2].ToString()).Trim());
                                int packetId = int.Parse((line[line.IndexOf("Integer(") + "Integer(".Length].ToString() + line[line.IndexOf("Integer(") + "Integer(".Length + 1].ToString()).Trim().Replace(")", String.Empty));
                                string s = line.Replace(" trait const QName(PackageNamespace(\"\", \"#0\"), \"", String.Empty);
                                string name = s.Remove(s.IndexOf("\") slotid"));

                                PacketIdCollection.Add(slotId, new KeyValuePair<string, int>(name, packetId));
                            }
                        }
                    }
                }

                foreach (var i in PacketIdCollection)
                {
                    string realName = GetPacketIdName(i.Key) ?? i.Value.Key;

                    packetIdText += Environment.NewLine;
                    packetIdText += "       " + realName + " = " + i.Value.Value + ", //slotid: " + i.Key;
                }
                packetIdText = packetIdText.Remove(packetIdText.LastIndexOf(','), 1);
                packetIdText +=
    @"
    }
}";

                using (StreamWriter wtr = new StreamWriter(Environment.CurrentDirectory + "\\PacketIds.cs", false))
                    wtr.Write(packetIdText);
            }

            UpdateLabel("PacketIds exported");
        }

        private void DeleteFolders()
        {
            UpdateLabel("Deleting temp files");
            foreach (var file in files)
                File.Delete(file);
            RunProcess("cmd.exe", $"/c rmdir /S /Q \"{Environment.CurrentDirectory}\\{client1}\"");
            if (File.Exists(Environment.CurrentDirectory + @"\client-1.abc"))
                File.Delete(Environment.CurrentDirectory + @"\client-1.abc");
            if (File.Exists(Environment.CurrentDirectory + @"\client-0.abc"))
                File.Delete(Environment.CurrentDirectory + @"\client-0.abc");
            string[] bins = Directory.GetFiles(Environment.CurrentDirectory, "client-*.bin");
            foreach (var bin in bins)
                File.Delete(bin);
        }

        static Tuple<bool, string> RunProcess(string exe, string args = "")
        {
            var p = new Process()
            {
                StartInfo = new ProcessStartInfo(exe, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            // hookup the eventhandlers to capture the data that is received
            var sb = new StringBuilder();
            p.OutputDataReceived += (sender, args1) => sb.AppendLine(args1.Data);
            p.ErrorDataReceived += (sender, args2) => sb.AppendLine(args2.Data);

            // start the process
            p.Start();

            // start our event pumps
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            // until we are done
            p.WaitForExit();

            var stdout = sb.ToString();
            var success = p.ExitCode == 0;
            if(!success)
            {
                MessageBox.Show(stdout);
            }
            return new Tuple<bool, string>(success, stdout);
        }

        private void Decompile()
        {
            orginalFilename = "client-orginal(" + DateTime.Now.Ticks + ").swf";
            File.Copy(Environment.CurrentDirectory + @"\client.swf", Environment.CurrentDirectory + "\\" + orginalFilename);
            //swfdecompress client.swf
            //abcexport client.swf
            //rabcdasm client-1.abc
            UpdateLabel("Decompiling");

            var p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\swfdecompress.exe", "client.swf");
            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\abcexport.exe", "client.swf");

            //new version only has client-0.abc
            if (!File.Exists("client-1.abc") && File.Exists("client-0.abc"))
                client1 = "client-0";

            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\rabcdasm.exe", $"{client1}.abc");
            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\swfbinexport.exe", "client.swf");

            this.files = Directory.GetFiles(Environment.CurrentDirectory + $@"\{client1}", "*.class.asasm", SearchOption.AllDirectories);
        }

        private void Recompile()
        {
            //rabcasm client-1\client-1.main.asasm
            //abcreplace client.swf 1 client-1\client-1.main.abc
            UpdateLabel("Recompiling");

            var p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\rabcasm.exe", $"{client1}\\{client1}.main.asasm");

            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\abcreplace.exe", $"client.swf {client1.Last()} {client1}\\{client1}.main.abc");
            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\swflzmacompress.exe", "client.swf");
            File.Move(Environment.CurrentDirectory + "\\client.swf", Environment.CurrentDirectory + "\\client-release.swf");

            /*var op = Process.Start(Environment.CurrentDirectory + "\\Orape.exe");
            MessageBox.Show("Now Build your client with orape and press ok after orape is done to compress the client");

            while (!op.HasExited)
                MessageBox.Show("Orape is still running, close it before you continue");

            File.Copy(Environment.CurrentDirectory + "\\" + orginalFilename, Environment.CurrentDirectory + "\\client.swf", true);
            File.Delete(Environment.CurrentDirectory + "\\" + orginalFilename);

            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\swflzmacompress.exe", "client.swf");
            p = RunProcess(Environment.CurrentDirectory + @"\rabcdasm\swflzmacompress.exe", "client-mod.swf");

            File.Copy(Environment.CurrentDirectory + "\\client-mod.swf", Environment.CurrentDirectory + "\\client-release.swf", true);
            File.Delete(Environment.CurrentDirectory + "\\client-mod.swf");*/
        }

        private void EnableRemoteTexture()
        {
            UpdateLabel("searching for texture class");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains("Texture")
                        && filetext.Contains("AnimatedTexture")
                        && filetext.Contains("RemoteTexture")
                        && filetext.Contains("RandomTexture")
                        && filetext.Contains("AltTexture")
                        && filetext.Contains("Mask")
                        && filetext.Contains("Effect")
                        && filetext.Contains("trait method QName(PackageNamespace(\"\", \"#0\"), \"getTexture\") flag OVERRIDE")
                        && filetext.Contains("trait method QName(PackageNamespace(\"\", \"#0\"), \"getAltTextureData\") flag OVERRIDE"))
                    {
                        UpdateLabel("texture class found!");
                        files.Add(path, filetext);
                    }
                }
            }
            var superCalled = false;
            foreach (var file in files)
            {
                UpdateLabel("doing texture class stuff");
                filetext = String.Empty;
                using (StreamReader rdr = new StreamReader(File.Open(file.Key, FileMode.Open)))
                {
                    while (!rdr.EndOfStream)
                    {
                        var line = rdr.ReadLine();
                        if (line.Contains("constructsuper"))
                            superCalled = true;

                        if (superCalled && line.Contains("callproperty"))
                        {
                            line = "     pushtrue";
                            superCalled = false;
                        }

                        filetext += line + "\n";
                    }
                }
                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("texture class: Done!");
        }

        private void FixEntityClass()
        {
            UpdateLabel("searching for entity class");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains("toString") && filetext.Contains("\"objectType_: \"") && filetext.Contains("\" status_: \"") && filetext.Contains("readShort") && filetext.Contains("parseFromInput"))
                    {
                        UpdateLabel("entity class found!");
                        files.Add(path, filetext);
                    }
                }
            }

            foreach (var file in files)
            {
                UpdateLabel("replacing short with unsigned short");
                filetext = file.Value.Replace("readShort", "readUnsignedShort");
                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("Entity class: Done!");
        }

        private void ChangeCopyright()
        {
            UpdateLabel("searching for rotmg version display string");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains(ROTMG_VERSION_TEXT))
                    {
                        UpdateLabel("request found!");
                        files.Add(path, filetext);
                    }
                }
            }

            foreach (var file in files)
            {
                UpdateLabel("replacing version text");
                filetext = file.Value.Replace(ROTMG_VERSION_TEXT, "<font color='#00CCFF'>Fabiano Swagger of Doom</font> #{VERSION}.{MINORVERSION}");
                filetext = filetext.Replace("{MINOR}", "{MINORVERSION}");
                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("Version Display: Done!");
        }

        private void ReplaceHttpsWithHttp()
        {
            UpdateLabel("searching for https request string");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains(HTTPS_STRING) && filetext.Contains(ROTMG_VERSION_TEXT))
                    {
                        UpdateLabel("request found!");
                        files.Add(path, filetext);
                    }
                }
            }

            foreach (var file in files)
            {
                UpdateLabel("replacing https with http");
                filetext = file.Value.Replace(HTTPS_STRING, HTTP_STRING);
                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("HTTPS: Done!");
        }

        private void ReplaceDomain()
        {
            UpdateLabel("searching for domains");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains(PRODAPPSPOT_DOMAIN) || filetext.Contains(PROD_DOMAIN) || filetext.Contains(PRODAPPSPOT_DOMAIN_WWW) || filetext.Contains(PROD_DOMAIN_WWW) || filetext.Contains(PRODAPPSPOTHRD_DOMAIN_WWW) || filetext.Contains(PRODAPPSPOTHRD_DOMAIN))
                    {
                        UpdateLabel("domain found!");
                        files.Add(path, filetext);
                    }
                }
            }

            foreach (var file in files)
            {
                UpdateLabel("replacing domains");
                filetext = file.Value.Replace(PRODAPPSPOT_DOMAIN_WWW, domain);
                filetext = filetext.Replace(PROD_DOMAIN_WWW, domain);
                filetext = filetext.Replace(PRODAPPSPOT_DOMAIN, domain);
                filetext = filetext.Replace(PROD_DOMAIN, domain);
                filetext = filetext.Replace(PRODAPPSPOTHRD_DOMAIN_WWW, domain);
                filetext = filetext.Replace(PRODAPPSPOTHRD_DOMAIN, domain);

                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("Domains: Done!");
        }

        private void ReplaceRSA()
        {
            UpdateLabel("searching for rsa key");
            Dictionary<string, string> files = new Dictionary<string, string>();
            string filetext = String.Empty;

            foreach (string path in this.files)
            {
                using (StreamReader rdr = new StreamReader(File.Open(path, FileMode.Open)))
                {
                    filetext = rdr.ReadToEnd();
                    if (filetext.Contains(PROD_RSA_PUBKEY_1) || filetext.Contains(PROD_RSA_PUBKEY_2) || filetext.Contains(PROD_RSA_PUBKEY_3) || filetext.Contains(PROD_RSA_PUBKEY_4))
                    {
                        UpdateLabel("rsa key found!");
                        files.Add(path, filetext);
                    }
                }
            }

            foreach (var file in files)
            {
                UpdateLabel("replacing rsa key");
                filetext = file.Value.Replace(PROD_RSA_PUBKEY_1, PRIV_RSA_PUBKEY_1);
                filetext = filetext.Replace(PROD_RSA_PUBKEY_2, PRIV_RSA_PUBKEY_2);
                filetext = filetext.Replace(PROD_RSA_PUBKEY_3, PRIV_RSA_PUBKEY_3);
                filetext = filetext.Replace(PROD_RSA_PUBKEY_4, PRIV_RSA_PUBKEY_4);

                using (StreamWriter wtr = new StreamWriter(file.Key, false))
                    wtr.Write(filetext);
            }
            UpdateLabel("RSA Keys: Done!");
        }

        private void UpdateLabel(string message)
        {
            lb.Text = String.Format("Status: {0}", message);
            lb.Update();
        }

        private void ExportXmls()
        {
            if (File.Exists(Environment.CurrentDirectory + @"\dat0.xml")) File.Delete(Environment.CurrentDirectory + @"\dat0.xml");
            if (File.Exists(Environment.CurrentDirectory + @"\dat1.xml")) File.Delete(Environment.CurrentDirectory + @"\dat1.xml");
            if (File.Exists(Environment.CurrentDirectory + @"\EquipmentSets.xml")) File.Delete(Environment.CurrentDirectory + @"\EquipmentSets.xml");
            string[] bins = Directory.GetFiles(Environment.CurrentDirectory, "*.bin");
            string groundxml = "<GroundTypes>";
            string equipentsetsxml = "<EquipmentSets>";
            string objectxml = "<Objects>";

            foreach (var bin in bins)
            {
                try
                {
                    unsafe
                    {
                        int type;
                        string xml = CreateXML(File.ReadAllText(bin), &type);

                        switch (type)
                        {
                            case 0:
                                objectxml += xml;
                                break;
                            case 1:
                                groundxml += xml;
                                break;
                            case 2:
                                equipentsetsxml += xml;
                                break;
                        }

                        using (StreamWriter fullxml = new StreamWriter("dat1.xml"))
                            fullxml.WriteLine(objectxml + "</Objects>");

                        using (StreamWriter fullxml = new StreamWriter("dat0.xml"))
                            fullxml.WriteLine(groundxml + "</GroundTypes>");

                        using (StreamWriter fullxml = new StreamWriter("EquipmentSets.xml"))
                            fullxml.WriteLine(equipentsetsxml + "</EquipmentSets>");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            UpdateLabel("Xml Export Done");
        }

        private unsafe string CreateXML(string input, int* type)
        {
            try
            {
                if (input.Contains("<Objects>"))
                {
                    input = input.Replace("<Objects>", "");
                    input = input.Replace("</Objects>", ""); //<?xml version="1.0" encoding="ISO-8859-1"?>
                    input = input.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>", "");
                    *type = 0;

                    return input;
                }

                else if (input.Contains("<GroundTypes>"))
                {
                    input = input.Replace("<GroundTypes>", "");
                    input = input.Replace("</GroundTypes>", "");
                    input = input.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>", "");
                    *type = 1;

                    return input;
                }
                else if (input.Contains("<EquipmentSets>"))
                {
                    input = input.Replace("<EquipmentSets>", "");
                    input = input.Replace("</EquipmentSets>", ""); //<?xml version="1.0" encoding="ISO-8859-1"?>
                    input = input.Replace("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>", "");
                    *type = 2;

                    return input;
                }
            }
            catch
            {
            }
            *type = -1;
            return "";
        }
    }
}
