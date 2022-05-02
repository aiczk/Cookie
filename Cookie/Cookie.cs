﻿using System;
using System.Text.RegularExpressions;
using Dalamud.Game.Command;
using Dalamud.Plugin;
using Dalamud.IoC;
using Cookie.Helper;
using Cookie.UI;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.Gui.ContextMenus;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;

namespace Cookie;

public class Cookie : IDalamudPlugin
{
    public string Name => "Cookie";
    private const string CommandName = "/cookie";

    private Configuration Configuration { get; }
    private CookieUi Ui { get; }

    public Cookie([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
    {
        DalamudContainer.Initialize(pluginInterface);
        Configuration = DalamudContainer.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
        Configuration.Initialize(DalamudContainer.PluginInterface);
        
        Ui = new CookieUi(Configuration);
            
        DalamudContainer.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = "Open config window for Cookie."
        });

        DalamudContainer.PluginInterface.UiBuilder.Draw += DrawUi;
        DalamudContainer.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUi;
        DalamudContainer.ChatGui.ChatMessage += ApplyMark;
    }
    
    private void ApplyMark(XivChatType type, uint id, ref SeString sender, ref SeString message, ref bool handled)
    {
        if(sender == null)
            return;
        
        var senderName = sender.TextValue;
        if (type is XivChatType.Party or XivChatType.CrossParty && Configuration.ShowIconsForPartyMemberRoles)
        {
            sender = $"{senderName[..1]}{CookieHelper.BuildName(CookieHelper.GetMemberRoleAscii(senderName), senderName.Remove(0, 1))}";
            return;
        }

        var player = Configuration.Players.Find(x => $"{x.FirstName} {x.FamilyName}" == senderName);
        if(player == null)
            return;
        
        sender = CookieHelper.BuildName(CookieHelper.MenuDict[player.Genre][player.MarkIndex].Ascii, senderName);
    }

    void IDisposable.Dispose()
    {
        DalamudContainer.ChatGui.ChatMessage -= ApplyMark;
        DalamudContainer.CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args) => Ui.Visible = true;
    private void DrawUi() => Ui.Draw();
    private void DrawConfigUi() => Ui.SettingsVisible = true;
}