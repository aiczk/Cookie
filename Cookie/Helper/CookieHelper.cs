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

    public static readonly Dictionary<string, MenuLabel[]> MenuDict = new()
    {
        {
            "City", new []
            {
                new MenuLabel("Limsa Lominsa", '4'),
                new MenuLabel("Gridania", '5'),
                new MenuLabel("Ul'dah", '6'),
                new MenuLabel("Ishgard", 'A'),
            }
        },
        {
            "Status", new []
            {
                new MenuLabel("Sprouts", 'N'),
                new MenuLabel("Returner", '`'),
                new MenuLabel("Mentor", 'O'),
                new MenuLabel("Battle Mentor", 'P'),
                new MenuLabel("Crafter Mentor", 'Q'),
                new MenuLabel("Mentor (PT)", 'R'),
                new MenuLabel("Cross World", 'Y'),
            }
        },
        {
            "Role", new []
            {
                new MenuLabel("Tank", 'S'),
                new MenuLabel("Healer", 'T'),
                new MenuLabel("DPS", 'U'),
                new MenuLabel("Crafter", 'V'),
                new MenuLabel("Gatherer", 'W'),
                new MenuLabel("Other", 'X'),
            }
        },
        {
            "Fate", new []
            {
                new MenuLabel("Subjugation", 'Z'),
                new MenuLabel("Boss", '['),
                new MenuLabel("Induction", '\\'),
                new MenuLabel("Defense", ']'),
                new MenuLabel("Delivery", '^'),
                new MenuLabel("Special", '_'),
            }
        },
        {
            "Type", new []
            {
                new MenuLabel("Fire", '9'),
                new MenuLabel("Ice", ':'),
                new MenuLabel("Wind", ';'),
                new MenuLabel("Ground", '<'),
                new MenuLabel("Lightning", '='),
                new MenuLabel("Water", '>'),
            }
        },
        {
            "Other", new []
            {
                new MenuLabel("Arrow (Green)", '7'),
                new MenuLabel("Arrow (Red)", '8'),
                new MenuLabel("Aetheryte", 'B'),
                new MenuLabel("Mini Aetheryte", 'C'),
                new MenuLabel("Star (Filled)", 'D'),
                new MenuLabel("Star (Unfilled)", 'E'),
                new MenuLabel("Aether (Filled)", 'K'),
                new MenuLabel("Aether (Unfilled)", 'L'),
                new MenuLabel("Drawn Sword", 'H'),
                new MenuLabel("Sheathed Sword", 'I'),
                new MenuLabel("Dice", 'J'),
                new MenuLabel("Ban", 'M'),
                new MenuLabel("Warning", '@'),
                new MenuLabel("Sync", '?'),
            }
        },
        {
            "Default", new []{ new MenuLabel("Nothing", AsciiDefaultValue) }
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