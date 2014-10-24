// * Created by Jean-Philippe Boivin
// * Copyright © 2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Script;
using COServer.Entities;
using CO2_CORE_DLL;

namespace COServer.Network
{
    public unsafe class MsgNpc : Msg
    {
        public const Int16 Id = _MSG_NPC;

        public enum Action
        {
            BeActived = 0,	    			// to server		// ´¥·¢
            AddNpc = 1,						// no use
            LeaveMap = 2,					// to client		// É¾³ý
            DelNpc = 3,						// to server
            ChangePos = 4,					// to client/server
            LayNpc = 5,						// to client(id=region,data=lookface), answer MsgNpcInfo(CMsgPlayer for statuary)
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 UniqId;
            public Int32 Param;
            public Int16 Action;
            public Int16 Type;
        };

        public static Byte[] Create(Entity Entity, Int32 Param, Action Action, Int16 Type)
        {
            try
            {
                MsgInfo* pMsg = (MsgInfo*)Marshal.AllocHGlobal(sizeof(MsgInfo)).ToPointer();
                pMsg->Header.Length = (Int16)sizeof(MsgInfo);
                pMsg->Header.Type = Id;

                pMsg->UniqId = Entity.UniqId;
                pMsg->Param = Param;
                pMsg->Action = (Int16)Action;
                pMsg->Type = Type;

                Byte[] Out = new Byte[pMsg->Header.Length];
                Marshal.Copy((IntPtr)pMsg, Out, 0, Out.Length);
                Marshal.FreeHGlobal((IntPtr)pMsg);

                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                if (Client == null || Buffer == null || Client.User == null)
                    return;

                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 UniqId = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Int32 Param = (Int32)((Buffer[0x0B] << 24) + (Buffer[0x0A] << 16) + (Buffer[0x09] << 8) + Buffer[0x08]);
                Action Action = (Action)((Buffer[0x0D] << 8) + Buffer[0x0C]);
                Int16 Type = (Int16)((Buffer[0x0F] << 8) + Buffer[0x0E]);

                Player Player = Client.User;

                switch (Action)
                {
                    case Action.BeActived:
                        {
                            if (!Player.IsAlive())
                            {
                                Player.SendSysMsg(Client.GetStr("STR_DIE"));
                                return;
                            }

                            Entity Npc = null;
                            if (World.AllNPCs.ContainsKey(UniqId))
                                Npc = World.AllNPCs[UniqId];

                            if (World.AllTerrainNPCs.ContainsKey(UniqId))
                                Npc = World.AllTerrainNPCs[UniqId];

                            if (Npc == null)
                                return;

                            if (Player.Map != Npc.Map)
                                return;

                            if (!MyMath.CanSee(Player.X, Player.Y, Npc.X, Npc.Y, 17))
                                return;

                            Client.NpcUID = UniqId;
                            if (Program.Debug)
                                Player.SendSysMsg("Msg[" + MsgId + "], Param0[" + UniqId + "], Param1[" + Param + "], Action[" + (Int16)Action + "]");

                            if (Npc as NPC != null && (Npc as NPC).IsStorageNpc())
                            {
                                Player.Send(MsgAction.Create(Player, 4, MsgAction.Action.OpenDialog));
                                return;
                            }

                            Byte[] Data = new Byte[Kernel.MAX_BUFFER_SIZE];
                            Int32 Position = 0;

                            //GC
                            if (UniqId >= 815 && UniqId <= 818)
                            {
                                if (World.AllMaps[Npc.Map].InWar)
                                    return;
                            }

                            if (UniqId == 110113 || UniqId == 110203 || UniqId == 110003 || UniqId == 110153)
                            {
                                if (World.AllMaps[Npc.Map].InWar)
                                    return;
                            }
                            //GC

                            //Poles
                            if (Client.NpcUID == 110110 ||
                                Client.NpcUID == 110200 ||
                                Client.NpcUID == 110000 ||
                                Client.NpcUID == 110150 ||
                                Client.NpcUID == 110380)
                            {
                                TerrainNPC Pole = World.AllTerrainNPCs[Client.NpcUID] as TerrainNPC;
                                Int32 Money = 0;

                                if (Pole != null)
                                    Money = Pole.MaxHP - Pole.CurHP;

                                Position += ScriptHandler.SendText("Voulez-vous réparer le pole? Vous aurez besoin de " + Money + " des capitaux de guilde.", Client, ref Data, Position);
                                Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                ScriptHandler.SendData(Client, Data, Position);
                            }
                            //Poles

                            //Gates
                            if (UniqId == 110111 || UniqId == 110112 ||
                                UniqId == 110201 || UniqId == 110202 ||
                                UniqId == 110001 || UniqId == 110002 ||
                                UniqId == 110151 || UniqId == 110152 ||
                                UniqId == 110381 || UniqId == 110382)
                            {
                                Position += ScriptHandler.SendText("Que voulez-vous faire?", Client, ref Data, Position);
                                Position += ScriptHandler.SendOption(1, "Ouvrir/Fermer la porte.", Client, ref Data, Position);
                                Position += ScriptHandler.SendOption(2, "Entrer dans le château.", Client, ref Data, Position);
                                Position += ScriptHandler.SendFace(47, Client, ref Data, Position);
                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                ScriptHandler.SendData(Client, Data, Position);
                            }
                            //Gates

                            if (ScriptHandler.AllScripts[(Byte)Client.Language].ContainsKey(UniqId))
                            {
                                ScriptHandler.AllScripts[(Byte)Client.Language][UniqId].Execute(0, Client);
                                return;
                            }
                            else
                            {
                                if (Client.Language == Language.En)
                                {
                                    switch (Client.NpcUID)
                                    {
                                        case 422: //MaçonVieux
                                            {
                                                Position += ScriptHandler.SendText("Do you want to participate at the tournament? For each round, two players will fight to win. The winner will get 250 CPs et he will go to the next round. The final winner will get 1000 CPs", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Yes, I want.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "No, thanks.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(54, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                return;
                                            }
                                    }
                                }
                                else
                                {
                                    switch (Client.NpcUID)
                                    {
                                        case 422: //MaçonVieux
                                            {
                                                Position += ScriptHandler.SendText("Voulez-vous participer au tournoi? À chaque tour, deux joueurs se combattent pour gagner. Le vainqueur gagne 250 CPs et passe au prochain tour. Le vainqueur final gagne 1000 CPs de plus.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Oui, je veux bien.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(54, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                return;
                                            }
                                        case 923: //DameBonheur
                                            {
                                                Position += ScriptHandler.SendText("Bienvenues au Centre de Loterie! Il y a beaucoup de BoîteSuprises qui ont des trésors innombrables tel que Gemmes Supers,objet de deux trou, 500.000.000 argents etc. dedans. Si vous êtes Niveau 70 ou plus, vous pouvez y entrer et essayer votre chance dix fois tout au plus un jour. Et je vais vous changer justement 54 CPs pour chaque admission.Venez, essayez votre chance tout de suite!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Wow, Bonne idée.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(2, "Je peux savoir les règle d'abord?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ça ne m'intéresse pas.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(1, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                return;
                                            }
                                    }
                                }

                                switch (UniqId)
                                {
                                    case 41: //ArtisantOu
                                        {
                                            Position += ScriptHandler.SendText("Pendant des années, j'ai étudié les armes. J'ai finalement trouvé le moyen d'utilisé les pouvoirs des Perles de Dragon pour faire des trous dans les armes. Voulez-vous que je vous aide?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Aidez moi à créer un trou dans mon arme.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 45: //GérantDeMarché
                                        {
                                            Position += ScriptHandler.SendText("Voulez-vous partir?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(1, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 140: //GardienDePrison
                                        {
                                            String[][] Costs = new String[2][] { 
                                                new String[] { "1000", "2500", "5000" },
                                                new String[] { "2.50", "5.00", "7.50" } };

                                            if (Player.JailC > 0 && Player.JailC <= 3)
                                                Position += ScriptHandler.SendText("Je suis le gardien de cette prison. Vous avez utilisé un logiciel illégal ou vous avez abusé d'un bug." + 
                                                                                    " Maintenant, vous devez payer " + Costs[0][Player.JailC - 1] + " CPs pour sortir." +
                                                                                    " Si vous n'avez pas cette somme, vous devez communiquer avec l'équipe (info@copserver.com) pour payer " + Costs[1][Player.JailC - 1] + "$ par OneBip ou PayPal. Voulez-vous sortir?", Client, ref Data, Position);
                                            else if (Player.JailC > 3)
                                            {
                                                Position += ScriptHandler.SendText("Vous ne pouvez plus sortir.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(75, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                                break;
                                            }
                                            else
                                                Position += ScriptHandler.SendText("Je suis le gardien de cette prison. Voulez-vous sortir?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(75, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 380: //GérantDeGuilde
                                        {
                                            Position += ScriptHandler.SendText("Voulez-vous entrer sur la carte du chateau de guilde?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);

                                            //Position += ScriptHandler.SendText("J'ai le mandat de gérer les guerres. Depuis la dernière guerre qui a dévasté ce monde, nous avons décidé de controler le plus possible ces guerres.", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendText(" Les chefs des guildes peuvent déclarer une guerre contre la guilde qui controle une ville. Pendant les 5 heures suivant la déclaration, toutes les guildes", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendText(" existante pourront se battre pour prendre possession de la ville. À la fin, la guilde qui a réussi à assiéger la ville sera maitre de celle-ci.", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendText(" La guilde possédant une région aura des redevances. Quelle région voudriez-vous prendre?", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendOption(1, "La Foret", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendOption(2, "Le Canyon", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendOption(3, "Le Désert", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendOption(4, "Les Iles", Client, ref Data, Position);
                                            //Position += ScriptHandler.SendFace(123, Client, ref Data, Position);
                                            //Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            //ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 390: //RocherDeL'Amour
                                        {
                                            Position += ScriptHandler.SendText("On croit toujours que le destin relie les amoureux. Je souhaite que tout le monde se marie et vive une vie heureuse. Que voulez-vous faire?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Je veux me marier.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 925: //BoîteSurprise 
                                    case 926: //BoîteSurprise 
                                    case 927: //BoîteSurprise 
                                    case 928: //BoîteSurprise 
                                    case 929: //BoîteSurprise 
                                    case 930: //BoîteSurprise 
                                    case 931: //BoîteSurprise 
                                    case 932: //BoîteSurprise 
                                    case 933: //BoîteSurprise 
                                    case 934: //BoîteSurprise 
                                    case 935: //BoîteSurprise 
                                    case 936: //BoîteSurprise 
                                    case 937: //BoîteSurprise 
                                    case 938: //BoîteSurprise 
                                    case 939: //BoîteSurprise 
                                    case 940: //BoîteSurprise 
                                    case 942: //BoîteSurprise 
                                    case 943: //BoîteSurprise 
                                    case 944: //BoîteSurprise 
                                    case 945: //BoîteSurprise 
                                        {
                                            Position += ScriptHandler.SendText("Voulez-vous vraiment choisir cette boîte?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(102, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 941: //DieuD'aptitude
                                        {
                                            Position += ScriptHandler.SendText("En utilisant l'énergie des Balles Exp, je peux vous aider à améliorer vos compétences d'armes. Vous devez avant tout avoir un talent de niveau 1. Que voulez-vous faire?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Compétence une main.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Compétence deux mains.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Compétence autre.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(42, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 1550: //ForgeronLee
                                        {
                                            Position += ScriptHandler.SendText("Je peux vous aider à faire des trous dans vos objets. Il me faut 15 perle de dragon pour le premier trou et une foreuse de diamant pour le second.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendText(" Si vous avez 7 foreuses d'étoile, je peux faire un second trou. Quel objet voulez-vous améliorer?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Casque/Boucles", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Collier/Sac", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Armure/Robe", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(5, "Bouclier", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(6, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(7, "Bottes", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(102, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 3216: //ChevalierLibre
                                        {
                                            Position += ScriptHandler.SendText("Je peux vous coiffer de différente manière. Vous devez me donner une Amulette-Chance.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui, S.V.P.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(74, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            Client.NpcAccept = false;
                                            break;
                                        }
                                    case 3836: //Administrateur
                                        {
                                            Client.Send(MsgAction.Create(Player, (Byte)MsgAction.Dialog.OfflineTG, MsgAction.Action.OpenDialog));
                                            break;
                                        }
                                    case 7050: //MaitreD'arme
                                        {
                                            Position += ScriptHandler.SendText("Lorsque le niveau de l'équipement est trop élevé, l'ArtisanMagique n'est plus en mesure de l'améliorer. Je suis ici pour rendre ça possible. Je charge seulement une PerleDeDragon", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Casque/Boucles", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Collier/Sac", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Armure/Robe", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(4, "Arme (Main droite)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(5, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(6, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(7, "Bottes", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 10002: //Coiffeur
                                        {
                                            Position += ScriptHandler.SendText("Je peux vous coiffer de différente manière. Vous n'avez qu'à me payer 500 argents.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Nouveaux styles.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Styles nostalgiques.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non, merci.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(134, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            Client.NpcAccept = false;
                                            break;
                                        }
                                    case 10003: //Mandarin
                                        {
                                            Position += ScriptHandler.SendText("Je m'occupe des guildes de la région. Lorsque vous avez des questions concernant les guildes, vous pouvez me le demander.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Créer une guilde.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Dégrouper la guilde.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Donation de guilde.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(4, "Chef abdique.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(5, "Commettre à sous-chef.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(6, "Décharger.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(7, "Consulter une guilde.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(8, "Autres options", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(7, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 10062: //ArtisantMagique
                                        {
                                            Position += ScriptHandler.SendText("L'ArtisantDuVent de la Ville Dragon est très doué, mais il ne réussi pas toujours à améliorer votre équipement. Au fil du temps, j'ai acquis les connaissances nécessaire. Il faut plus de Météores ou de Perle de Dragon, mais je ne rate jamais.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Améliorer la qualité.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Améliorer le niveau.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            Client.NpcAccept = false;
                                            break;
                                        }
                                    case 10064: //Tinturier
                                        {
                                            Position += ScriptHandler.SendText("Je peux changer la couleur de votre équipement. Quel objet voulez-vous changer de couleur?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Armure", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Casque", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Bouclier", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            Client.NpcAccept = false;
                                            break;
                                        }
                                    case 10065: //ArtisantDuCiel
                                        {
                                            Position += ScriptHandler.SendText("J'ai acquis le pouvoir de réparer votre sac! Beaucoup de personne semble me dire qu'après un certain temps, leur sac devient plus faible et ne peut plus contenir autant d'objet. Est-ce votre cas?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 30000: //Célébrant
                                        {
                                            Position += ScriptHandler.SendText("On croit toujours que le destin relie les amoureux. Je souhaite que tout le monde se marie et vive une vie heureuse. Que voulez-vous faire?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Je veux me marier.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 35015: //Éthéré
                                        {
                                            Position += ScriptHandler.SendText("La gemme Mythique est une rareté antique. Elle peut réduire les dommage, mais aussi améliorer l'équipement béni. Si nous la combinons avec l'équipement béni, elle révèlera sa puissance éternelle.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Améliorer les attributs de bénédiction.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Vous avez un four inhabituel!", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Comment composer +n des objets?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 35016: //WuxingFour
                                        {
                                            Position += ScriptHandler.SendText("Un bon équipement peut vous aider dans vos batailles. Que puis-je faire pour vous?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Enchanter mon équipement.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Composer mon équipement. (+1~+9)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(4, "Composer mon équipement. (+10~+12)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Composer des objets.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(67, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 35501: //ArtisantRow
                                        {
                                            Position += ScriptHandler.SendText("Lorsque le niveau de l'équipement est trop élevé, le MaitreD'arme n'est plus en mesure de l'améliorer. Je charge un morceau de Citrine pour l'équipement 130+, un Oeil de Tigre pour l'équipement 135+ et 3 Citrines, 3 Oeil de Tigre pour l'équipement 140.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Casque/Boucles", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Collier/Sac", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(3, "Armure/Robe", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(4, "Arme (Main droite)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(5, "Arme/Bouclier (Main gauche)", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(6, "Bague/Anneau/Bracelets", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(7, "Bottes", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(96, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 300000: //ShanLee
                                        {
                                            Position += ScriptHandler.SendText("Vous avez " + Player.VPs + " points de virtue. Que voulez-vous faire?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Échanger mes points.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(2, "Qu'est-ce que des points de virtue?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Rien.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(29, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 300500: //TaoisteDuDestin
                                        {
                                            if (Player.Metempsychosis > 0)
                                            {
                                                Position += ScriptHandler.SendText("Vous avez déjà accompli la première renaissance.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else
                                            {
                                                Position += ScriptHandler.SendText("J'ai étudié l'univers toute ma vie et j'ai enfin découvert les secrets de notre monde.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Parlez-moi.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Ok, je vois.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 350050: //Saint-Taoiste
                                        {
                                            Position += ScriptHandler.SendText("Les joueurs (70+) ayant accompli la première renaissance peuvent redistribuer leurs points d'attributs en utilisant une Perle de Dragon.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Redistribuer mes points.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Je veux réfléchir encore.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 600055: //Astre
                                        {
                                            if (Player.Spouse == "Non")
                                            {
                                                Position += ScriptHandler.SendText("Hey! Vous n'êtes pas marié!", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je sais.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else
                                            {
                                                Position += ScriptHandler.SendText("Les étoiles dans le ciel représentent la vrai amour...", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText(" Malheureusement certaines étoiles ne brillent pas assez et nous devons les détruire.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText(" Le mariage représente c'est étoile. Je peux vous divorcer avec votre femme si votre", Client, ref Data, Position);
                                                Position += ScriptHandler.SendText(" amour n'est plus assez grande.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Je veux divorcer.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je suis heureux.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(27, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 600075: //BoxeurHuang
                                        {
                                            Position += ScriptHandler.SendText("Voulez-vous partir?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Oui.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Non.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(1, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 3215: //TaoïsteDeMer
                                        {
                                            if (World.DisCity && Player.Level > 109)
                                            {
                                                Position += ScriptHandler.SendText("Dis City est commencé et vous êtes assez fort pour m'aider! Êtes-vous prèt à déloger la vermine?", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(1, "Je veux y aller.", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Au revoir...", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else if (World.DisCity && Player.Level < 110)
                                            {
                                                Position += ScriptHandler.SendText("Revenez lorsque vous aurez un niveau supérieur à 110...", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je vois", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            else
                                            {
                                                Position += ScriptHandler.SendText("Il n'est pas encore l'heure...", Client, ref Data, Position);
                                                Position += ScriptHandler.SendOption(255, "Je vois", Client, ref Data, Position);
                                                Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                                Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                                ScriptHandler.SendData(Client, Data, Position);
                                            }
                                            break;
                                        }
                                    case 600401: //TaoïsteDeMer
                                        {
                                            Position += ScriptHandler.SendText("Avez-vous les 5 Jades d'enfer? Si oui vous devez aller à la deuxième carte.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Je veux y aller.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Au revoir...", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 600402: //TaoïsteDeMer
                                        {
                                            Position += ScriptHandler.SendText("Vous-avez tué " + Player.DisKO + " monstres. Voulez-vous entrer dans la troisième carte?", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(1, "Je veux y aller.", Client, ref Data, Position);
                                            Position += ScriptHandler.SendOption(255, "Au revoir...", Client, ref Data, Position);
                                            Position += ScriptHandler.SendFace(6, Client, ref Data, Position);
                                            Position += ScriptHandler.SendEnd(Client, ref Data, Position);
                                            ScriptHandler.SendData(Client, Data, Position);
                                            break;
                                        }
                                    case 600403: //TaoïsteDeMer
                                        {
                                            if (Player.InventoryContains(723088, 1))
                                            {
                                                Player.DelItem(723088, 1, true);

                                                UInt64 Exp = Player.CalcExpBall((Byte)Player.Level, Player.Exp, 16);
                                                Player.AddExp(Exp, false);

                                                Byte Dress = (Byte)MyMath.Generate(1, 3);
                                                if (Dress == 1)
                                                    Player.AddItem(Item.Create(0, 0, 137320, 0, 0, 0, 0, 0, 2, 0, 2999, 2999), true);
                                                else if (Dress == 2)
                                                    Player.AddItem(Item.Create(0, 0, 137330, 0, 0, 0, 0, 0, 2, 0, 2999, 2999), true);
                                                else if (Dress == 3)
                                                    Player.AddItem(Item.Create(0, 0, 137340, 0, 0, 0, 0, 0, 2, 0, 2999, 2999), true);

                                                Player.Move(1002, 400, 400);
                                                World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Player.Name + " a détruit PlutoFinal et a obtenu SeptEtoile!", MsgTalk.Channel.GM, 0xFFFFFF));
                                            }
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Action[{1}] not implemented yet!", MsgId, (Int16)Action);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}
