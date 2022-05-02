using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;

namespace Cookie
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;
        public List<Player> Players;
        public bool ShowIconsForPartyMemberRoles;

        [NonSerialized]
        private DalamudPluginInterface pluginInterface;

        public void Initialize(DalamudPluginInterface pluginInterface) => this.pluginInterface = pluginInterface;
        public void Save() => pluginInterface.SavePluginConfig(this);
    }

    public class Player
    {
        public string FirstName, FamilyName, Genre;
        public int MarkIndex;

        public Player(string firstName, string familyName, string genre, int markIndex)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Genre = genre;
            MarkIndex = markIndex;
        }
    }

    public class MenuLabel
    {
        public readonly string LabelName;
        public readonly char Ascii;

        public MenuLabel(string labelName, char ascii)
        {
            LabelName = labelName;
            Ascii = ascii;
        }
    }
}
