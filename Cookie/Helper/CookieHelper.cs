using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.Text.SeStringHandling;

namespace Cookie.Helper;

public static class CookieHelper
{
    public static PlayerCharacter Player => DalamudContainer.ClientState.LocalPlayer!;

    public static readonly Dictionary<string, BitmapFontIcon[]> Menu = new()
    {
        {
            "Regions", new []
            {
                BitmapFontIcon.LaNoscea,
                BitmapFontIcon.BlackShroud,
                BitmapFontIcon.Thanalan,
                BitmapFontIcon.Ishgard,
                BitmapFontIcon.FarEast,
                BitmapFontIcon.GyrAbania,
                BitmapFontIcon.Crystarium,
                BitmapFontIcon.Sharlayan,
                BitmapFontIcon.Ilsabard,
                BitmapFontIcon.Garlemald,
            }
        },
        {
            "Status", new []
            {
                BitmapFontIcon.NewAdventurer,
                BitmapFontIcon.Returner,
                BitmapFontIcon.Mentor,
                BitmapFontIcon.MentorPvE,
                BitmapFontIcon.MentorPvP,
                BitmapFontIcon.MentorCrafting,
                BitmapFontIcon.MentorProblem,
                BitmapFontIcon.CrossWorld,
            }
        },
        {
            "Role", new []
            {
                BitmapFontIcon.Tank,
                BitmapFontIcon.Healer,
                BitmapFontIcon.DPS,
                BitmapFontIcon.Crafter,
                BitmapFontIcon.Gatherer,
                BitmapFontIcon.AnyClass,
            }
        },
        {
            "Fate", new []
            {
                BitmapFontIcon.FateBoss,
                BitmapFontIcon.FateCrafting,
                BitmapFontIcon.FateDefend,
                BitmapFontIcon.FateEscort,
                BitmapFontIcon.FateGather,
                BitmapFontIcon.FateSlay,
                BitmapFontIcon.FateSpecial1,
                BitmapFontIcon.FateSpecial2,
                BitmapFontIcon.FateUnknownGold
            }
        },
        {
            "Element", new []
            {
                BitmapFontIcon.ElementEarth,
                BitmapFontIcon.ElementFire,
                BitmapFontIcon.ElementIce,
                BitmapFontIcon.ElementIce,
                BitmapFontIcon.ElementLightning,
                BitmapFontIcon.ElementWater,
                BitmapFontIcon.ElementWind
            }
        },
        {
            "Other",new []
            {
                BitmapFontIcon.AutoTranslateBegin,
                BitmapFontIcon.AutoTranslateEnd,
                BitmapFontIcon.LevelSync,
                BitmapFontIcon.Warning,
                BitmapFontIcon.Aetheryte,
                BitmapFontIcon.Aethernet,
                BitmapFontIcon.GoldStar,
                BitmapFontIcon.SilverStar,
                BitmapFontIcon.SwordUnsheathed,
                BitmapFontIcon.SwordSheathed,
                BitmapFontIcon.Dice,
                BitmapFontIcon.FlyZone,
                BitmapFontIcon.FlyZoneLocked,
                BitmapFontIcon.NoCircle,
                BitmapFontIcon.PriorityWorld,
                BitmapFontIcon.ElementalLevel,
                BitmapFontIcon.ExclamationRectangle,
                BitmapFontIcon.NotoriousMonster,
                BitmapFontIcon.Recording,
                BitmapFontIcon.Alarm,
                BitmapFontIcon.ArrowUp,
                BitmapFontIcon.ArrowDown,
                BitmapFontIcon.OrangeDiamond,
                BitmapFontIcon.FanFestival,

            }
        },
        {
            "Default", new []
            {
                BitmapFontIcon.None
            }
        }
    };

    public static BitmapFontIcon GetMemberRoleIcon(string senderName)
    {
        return DalamudContainer.PartyList.First(x => senderName.Contains(x.Name.TextValue)).ClassJob.GameData!.Role switch
        {
            1 => BitmapFontIcon.Tank,
            4 => BitmapFontIcon.Healer,
            2 => BitmapFontIcon.DPS,
            3 => BitmapFontIcon.DPS,
            _ => BitmapFontIcon.AnyClass
        };
    }

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