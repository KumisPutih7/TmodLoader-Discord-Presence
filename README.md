# Discord Rich Presence/Terraria Mod (WIP)
I just creating a DRPC for Tmodloader, What the features? it showing Current Profile Use, Current Item Holding, Current Progres State (Like "Pre-Harmode"), Current Boss Fighting, Current Invasion, and Current Location Biome.

My DRPC is using page, it has 3 pages in details currently and current boss pages spawn.

the status pages order :
- Details : Current Profile Use > Current Item Holding > Current Progres State > Current Profile Use > and so on
- State : IsHasBoss = (Showing Bosses "State : BossPage/MaxBossShow") else if IsHasInvasion = (Showing Invasion) Else Current Location Biome.

Example Text :
- "Current Proggress : Post-HardMode/Pre-MoonLord"
- "State : Fighting MoonLord (1/3)"

Example Image :

<img width="442" height="158" alt="Screenshot 2026-03-10 130826" src="https://github.com/user-attachments/assets/c6ba90b8-ad83-4dd1-a803-a67a229ec780" />

NOTE : 
- I added calamity Biomes too if the players has calamitymod in it :D.
- Because the bosses is detect by using all npc if the bosses has 2 npc so it contains as 2 bosses
- btw i dont have terraria, i just using my friends library (family sharing) thats why i upload it here.
- My english is bad, pls pull a request if i said wrong.

# Dependencies
- https://github.com/Lachee/discord-rpc-csharp
- CalamityMod From Workshop(doesnt install if you dont want to.)

# Credit
All external libraries and game assets (e.g., DiscordRPC by Lachee, Calamity Mod, and Terraria) are the intellectual property of their original creators. No copyright infringement is intended.

# ⚠ Warning ⚠
I didnt continue the project because mostly likely not interest again :3, Maybe sometimes but i dunno.

if you want to install to your own project install the Dependecies and put it in lib folder, add folder lib and OtherMods if it doesnt has.


if you need help create an issues or pull request.
