using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Terraria;
using Terraria.ModLoader;
using DiscordRPC.Logging;
using log4net.Repository.Hierarchy;
using CalamityMod;
using System.Security.Policy;
using CalamityMod.Items.Accessories;
// just a joke presence logic is like re-logic :D

namespace DiscordStatus
{
    internal class PresenceLogic : ModSystem
    {
        public static string CurrentPlayerUse = "PlaceHolderName";
        public static string CurrentItemHold = "BrokenCopperSword";
        public static string CurrentBiome = "Forest";
        public static List<string> CurrentBoss = new List<string>();
        public static string CurrentProgresState = "pre-harmode";
        public static string CurrentEvent = "";
        public static string CurrentWeather = "Clear";
        public static int CurrentPillars = 0;
        public static string[] CurrentBossDefeated = {};
        int StatusPage = 0;
        int BossPage = -1;
        static bool TaskReady = false;
        static Player p = Main.LocalPlayer;

        public override void PostUpdateEverything()
        {
            base.PostUpdateEverything();
            UpdateInfoRPC();
            if (TaskReady) { return; }
            TaskReady = true;
            Task.Run(async () =>
            {
                await Task.Delay(15000);
                try
                {
                    if (!Main.gameMenu)
                    {
                        StatusPage++;
                        BossPage++;

                        // make sure the boss page doesnt out of range
                        if (BossPage > CurrentBoss.Count - 1) { BossPage =  0; }

                        // this boss include calamity bosses because calamity added bosses to the vanilla terraria (maybe :p).
                        if (CurrentBoss.Count > 0)
                        {
                            DiscordStatus.State = "State : Fighting " + CurrentBoss[BossPage] + " (" + BossPage +"/" + CurrentBoss.Count +")";
                        }
                        else if (Main.invasionType > 0)
                        {
                            DiscordStatus.State = "State : " + CurrentEvent + " Event";
                        }
                        else
                        {
                            DiscordStatus.State = "State : " + CurrentBiome + " Biome";
                        }


                        if (StatusPage == 1)
                        {
                            DiscordStatus.Details = "Profile Use : " + CurrentPlayerUse;
                        }
                        else if (StatusPage == 2)
                        {
                            DiscordStatus.Details = $"Item Holding : " + CurrentItemHold;
                        }
                        else if (StatusPage == 3)
                        {
                            DiscordStatus.Details = $"Current Progress : " + CurrentProgresState;
                        }

                        if (StatusPage > 3)
                        {
                            StatusPage = 0;
                        }
                    }
                    else
                    {
                        DiscordStatus.Details = "Playing TModLoader";
                        DiscordStatus.State = "State : In The Main Menu";
                    }

                    DiscordStatus.UpdatePresence();
                }
                catch (Exception ex)
                {
                    StatusPage--;
                    BossPage = 0;
                    Mod.Logger.Warn("An error occurred while updating Discord Presence: " + ex.Message);
                }
                finally
                {
                    TaskReady = false;
                }
            });
        }

        public override void OnWorldUnload()
        {
            DiscordStatus.Details = "Playing TModLoader with " + ModLoader.Mods.Length + " mods!";
            DiscordStatus.State = "In The Main Menu";
            DiscordStatus.UpdatePresence();
        }

        [JITWhenModsEnabled("CalamityMod")]
        private static string CalamityBiomeChecker()
        {
            if (p.Calamity().ZoneAbyssLayer4)
            {
                return "Abyss Layer 4";
            }
            else if (p.Calamity().ZoneAbyssLayer3)
            {
                return "Abyss Layer 3";
            }
            else if (p.Calamity().ZoneAbyssLayer2)
            {
                return "Abyss Layer 2";
            }
            else if (p.Calamity().ZoneAbyssLayer1)
            {
                return "Abyss Layer 1";
            }
            else if (p.Calamity().ZoneAbyss)
            {
                return "The Abyss";
            }
            else if (p.Calamity().ZoneAstral)
            {
                return "Astral Infection";
            }
            else if (p.Calamity().ZoneSulphur)
            {
                return "Sulphur Sea";
            }
            else if (p.Calamity().ZoneSunkenSea)
            {
                return "Sunken Sea";
            }
            else if (p.Calamity().ZoneAstral)
            {
                return "Astral Infection";
            }

            return "";
        }

        private static string VanillaBiomeChecker()
        {
            if (p.ZoneDungeon)
            {
                return "Dungeon";
            }
            else if (p.ZoneUnderworldHeight)
            {
                return "Underworld";
            }
            else if (p.ZoneGlowshroom)
            {
                return "Mushroom Biome";
            }
            else if (p.ZoneJungle)
            {
                return "Jungle";
            }
            else if (p.ZoneBeach)
            {
                return "Beach";
            }
            else if (p.ZoneSnow)
            {
                return "Snow";
            }
            else if (p.ZoneDesert)
            {
                return "Desert";
            }
            else if (p.ZoneCorrupt)
            {
                return "Corruption";
            }
            else if (p.ZoneCrimson)
            {
                return "Crimson";
            }
            else if (p.ZoneHallow)
            {
                return "The Hallow";
            }
            else if (p.ZoneMeteor)
            {
                return "Meteor Site";
            }
            else if (p.ZoneSkyHeight)
            {
                return "Space";
            }
            else if (p.ZoneOverworldHeight)
            {
                return "Forest";
            }
            else if (p.ZoneDirtLayerHeight || p.ZoneRockLayerHeight)
            {
                return "Underground";
            }
            else
            {
                return "Exploring";
            }

        }

        private static string BiomeChecker()
        {
            if (ModLoader.HasMod("CalamityMod"))
            {
               string CalamityBiome = CalamityBiomeChecker();
                if (CalamityBiome != "")
                {
                    return CalamityBiome;
                }
            }

            return VanillaBiomeChecker();
        }

        [JITWhenModsEnabled("CalamityMod")]
        private static string CalamityBossChecker()
        {
            if (CalamityMod.DownedBossSystem.downedCalamitas)
            {
                return "Post-SupremeCalamitas";
            }
            else if (CalamityMod.DownedBossSystem.downedYharon)
            {
                return "Post-Yharon/Pre-SupremeCalamitas";
            }
            else if (CalamityMod.DownedBossSystem.downedDoG)
            {
                return "Post-DevourOfGods/Pre-Yharon";
            }
            else if (CalamityMod.DownedBossSystem.downedPolterghast)
            {
                return "Post-Polterghast/Pre-DevourOfGods";
            }
            else if (CalamityMod.DownedBossSystem.downedProvidence)
            {
                return  "Post-Providence/PrePolterghast";
            }
            else if (NPC.downedMoonlord && !CalamityMod.DownedBossSystem.downedProvidence)
            {
                return "Post-MoonLoard/Pre-Providence";
            }
            return "";
        }

        private static void UpdateInfoRPC()
        {
            CurrentPlayerUse = Main.LocalPlayer.name;
            Item item = Main.LocalPlayer.HeldItem;

            // Biome Checker
            CurrentBiome = BiomeChecker();

            if (!item.IsAir)
            {
                CurrentItemHold = item.Name;
            }
            else
            {
                CurrentItemHold = "Hand";
            }

            switch (Main.invasionType)
            {
                case 1: CurrentEvent = "Goblin Army"; break;
                case 2: CurrentEvent = "Frost Legion"; break;
                case 3: CurrentEvent = "Pirate Invasion"; break;
                case 4: CurrentEvent = "Martian Madness"; break;
            }

            if (Main.bloodMoon)
            {
                CurrentEvent = "Blood Moon";
            }
            else if (Main.eclipse)
            {
                CurrentEvent = "Solar Eclipse";
            }
            else if (Main.slimeRain)
            {
                CurrentEvent = "Slime Rain";
            }
            else if (Main.snowMoon)
            {
                CurrentEvent = "Frost Moon";
            }
            else if (Main.pumpkinMoon)
            {
                CurrentEvent = "Pumpkin Moon";
            }


            CurrentBoss.Clear();

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && npc.boss)
                {
                    CurrentBoss.Add(npc.FullName);
                }

            }

            if (NPC.downedMoonlord)
            {
                CurrentProgresState = "Post-MoonLord";
            }
            else if (NPC.downedPlantBoss)
            {
                CurrentProgresState = "Post-Plantera/Pre-MoonLord";
            }
            else if (Main.hardMode)
            {
                CurrentProgresState = "Post-HardMode/Pre-Plantera";
            }
            else
            {
                CurrentProgresState = "Pre-HardMode";
            };

            if (ModLoader.HasMod("CalamityMod"))
            {
                CurrentProgresState = CalamityBossChecker();
            }
        }
    }
}
