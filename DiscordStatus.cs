using DiscordRPC;
using DiscordRPC.Logging;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DiscordStatus
{
    // i dont want to remove this, this is master piece dialogue :3
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	
    public class DiscordStatus : Mod
	{
        public static DiscordRpcClient client;
        public static string Details = "Playing TModLoader";
        public static string State = ""; // current Biome
        public static string[] DAssets = { "https://forums.terraria.org/index.php?attachments/icon-png.280655/", "Terraria", "https://forums.terraria.org/index.php?attachments/icon-png.280655/" };
        public static string[] DButtons = { "Chat", "https://kaout.my.id" };
		// Btw the buttons didnt show up idk why so ill just use as placeholder
        // D means Discord, its like D = Discord Assets, why? because it can override the assets mods variable :D

        public override void Load()
        {
            base.Load();
            if (Main.dedServ) return;
            //424087019149328395 -- change to my own app id
            client = new DiscordRpcClient("1479607747820388453");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();
            client.OnReady += (sender, e) =>
            {
                Logger.Info("Discord Status of " + client.CurrentUser +" is ready!");
            };
            State = "Loading the Mods...";
            UpdatePresence();
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();
            Details = "Playing TModLoader with " + ModLoader.Mods.Length + " mods!";
            State = "In the Main Menu";
            UpdatePresence();
        }

        public static void UpdatePresence() 
        { 
            if (client == null) return;

            client.SetPresence(new RichPresence(){
                Details = Details,
                State = State,
                Assets = new Assets()
                {
                    LargeImageKey = DAssets[0],
                    LargeImageText = DAssets[1],
                    SmallImageKey = DAssets[2]
                },
                Buttons = new Button[]
                {
                    new Button() { Label = DButtons[0], Url = DButtons[1] }
                }
            });
            // Details: All stuff about the current world (e.g. difficulty, corruption/crimson, etc.)
            // State: Current Biome
            // Assets : All images of biomes and other stuff
            // Buttons : to show ip in the terraria just in case if the player is hosting a multiplayer world, You can disable it in the config 
        }

        public override void Unload()
        {
            client?.Dispose();
            base.Unload();
        }
    }
}

