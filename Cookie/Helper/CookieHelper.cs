using System;
using System.Collections.Generic;
using System.Linq;
using Cookie.Model;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.Text.SeStringHandling;

namespace Cookie.Helper;

public static class CookieHelper
{
    private const char AsciiDefaultValue = ' ';

    public static readonly Dictionary<string, MenuItem[]> MenuDict = new()
    {
        {
            "Nations", new []
            {
                new MenuItem("Limsa Lominsa", '4'),
                new MenuItem("Gridania", '5'),
                new MenuItem("Ul'dah", '6'),
                new MenuItem("Ishgard", 'A'),
                new MenuItem("Doma", 'a'),
                new MenuItem("Ala Mhigo", 'b'),
                new MenuItem("Crystarium", 'b'),
                new MenuItem("Sharlayan", 'r'),
                new MenuItem("Radz-at-Han", 's'),
                new MenuItem("Garlean Empire", 't'),
            }
        },
        {
            "Status", new []
            {
                new MenuItem("Sprouts", 'N'),
                new MenuItem("Returner", '`'),
                new MenuItem("Mentor", 'O'),
                new MenuItem("Battle Mentor", 'P'),
                new MenuItem("Crafter Mentor", 'Q'),
                new MenuItem("Mentor", 'R'),
                new MenuItem("Cross World", 'Y'),
            }
        },
        {
            "Role", new []
            {
                new MenuItem("Tank", 'S'),
                new MenuItem("Healer", 'T'),
                new MenuItem("DPS", 'U'),
                new MenuItem("Crafter", 'V'),
                new MenuItem("Gatherer", 'W'),
                new MenuItem("Other", 'X'),
            }
        },
        {
            "Fate", new []
            {
                new MenuItem("Kill enemies", 'Z'),
                new MenuItem("Kill boss", '['),
                new MenuItem("Collect item", '\\'),
                new MenuItem("Defend npc", ']'),
                new MenuItem("Escort npc", '^'),
                new MenuItem("Fate 6", '_'),
                new MenuItem("Fate 7", 'c'),
                new MenuItem("Fête", 'n'),
                new MenuItem("Fate 9", 'g'),
            }
        },
        {
            "Type", new []
            {
                new MenuItem("Fire", '9'),
                new MenuItem("Ice", ':'),
                new MenuItem("Wind", ';'),
                new MenuItem("Ground", '<'),
                new MenuItem("Lightning", '='),
                new MenuItem("Water", '>'),
            }
        },
        {
            "Other", new []
            {
                new MenuItem("Up arrow", 'j'),
                new MenuItem("Down arrow", 'k'),
                new MenuItem("Green arrow", '7'),
                new MenuItem("Red arrow", '8'),
                new MenuItem("Colorful flower", 'e'),
                new MenuItem("Exclamation mark", 'f'),
                new MenuItem("Contents replay", 'h'),
                new MenuItem("Aetheryte", 'B'),
                new MenuItem("Mini aetheryte", 'C'),
                new MenuItem("Plus star", 'd'),
                new MenuItem("Filled star", 'D'),
                new MenuItem("Unfilled star", 'E'),
                new MenuItem("Filled aether", 'K'),
                new MenuItem("Unfilled aether", 'L'),
                new MenuItem("Drawn sword", 'H'),
                new MenuItem("Sheathed sword", 'I'),
                new MenuItem("Auction", 'p'),
                new MenuItem("Meteor", 'q'),
                new MenuItem("Bell", 'i'),
                new MenuItem("Dice", 'J'),
                new MenuItem("Ban", 'M'),
                new MenuItem("Warning", '@'),
                new MenuItem("Sync", '?'),
            }
        },
        {
            "Default", new []{ new MenuItem("Nothing", AsciiDefaultValue) }
        }
    };

    public static char GetMemberRoleAscii(string senderName)
    {
        return DalamudContainer.PartyList.First(x => senderName.Contains(x.Name.TextValue)).ClassJob.GameData!.Role switch
        {
            0 => 'X',
            1 => 'S',
            2 => 'U',
            3 => 'U',
            4 => 'T',
            _ => AsciiDefaultValue
        };
    }

    public static SeString BuildName(char ascii, string name) => ascii == AsciiDefaultValue ? name : $"{ascii}{name}";

    public static (string, string) GetTargetName()
    {
        var target = DalamudContainer.TargetManager.Target;

        if (target == null || target.ObjectKind != ObjectKind.Player)
            return (null, null);
        
        var split = target.Name.TextValue.Split(" ");
        return (split[0], split[1]);
    }

    public static uint NameLength(string firstName) => (uint)(20 - firstName.Length);
}