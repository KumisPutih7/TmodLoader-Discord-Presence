# Discord Rich Presence/Terraria Mod (WIP)
I just creating a DRPC for Tmodloader, What the features? it showing Current Profile Use, Current Item Holding, Current Progres State (Like "Pre-Harmode"), Current Boss Fighting, Current Invasion, and Current Location Biome.

My DRPC is using page, it has 3 pages in details currently and current boss pages spawn.

the status pages order :
- Details : Current Profile Use > Current Item Holding > Current Progres State.
- State : IsHasBoss = (Showing Bosses "State : BossPage/MaxBossShow") else if IsHasInvasion = (Showing Invasion) Else Current Location Biome.

Example Text :
- "Current Proggress : Post-HardMode/Pre-MoonLord"
- "State : Fighting MoonLord (1/3)"

Example Image :

<img width="442" height="158" alt="Screenshot 2026-03-10 130826" src="https://github.com/user-attachments/assets/c6ba90b8-ad83-4dd1-a803-a67a229ec780" />


NOTE : 
- I added calamity Biomes too if the players has calamitymod in it :D.
- Because the bosses is detect by using all npc if the bosses has 2 npc so it contains as 2 bosses
- My english is bad, pls pull a request if i said wrong.

# ⚠ Warning ⚠
I didnt continue the project because mostly likely not interest again :3, Maybe sometimes but i dunno.

If you want to install the project you need to have install nuget for Discord C# rich presence in your project and calamityMod too.

if you need help create an issues or pull request.
