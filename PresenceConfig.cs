using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace DiscordStatus
{
    internal class PresenceConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [Label("Testing Config (Do Not Touch it :3)")]
        [Tooltip("Just debugging conifg hehe")]
        public bool Testing;
    }
}
