using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using Cookie.Model;

namespace Cookie
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;
        public List<Sender> Senders;
        public bool ShowPtRoleIcon;

        [NonSerialized]
        private DalamudPluginInterface pluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface) => this.pluginInterface = pluginInterface;
        public void Save() => pluginInterface.SavePluginConfig(this);
    }
}
