using System;
using System.Linq;
using System.Text;
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
using Dalamud.Game.Text.SeStringHandling.Payloads;

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
        
        var messageBuilder = new SeStringBuilder();
        foreach (var splitMessage in Regex.Split(message.TextValue, "(:\\w+:)"))
        {
            if (splitMessage.StartsWith(":"))
            {
                messageBuilder.AddIcon(Enum.Parse<BitmapFontIcon>(splitMessage.Trim(':')));
                continue;
            }
            
            messageBuilder.AddText(splitMessage);
        }
        message = messageBuilder.BuiltString;
        
        var stringBuilder = new SeStringBuilder();
        var senderHw = (sender.Payloads.ElementAtOrDefault(0) as PlayerPayload)?.World.RowId ?? CookieHelper.Lp.HomeWorld.Id;
        if (type is XivChatType.Party or XivChatType.CrossParty && Configuration.ShowPtRoleIcon)
        {
            var pName = (sender.Payloads.ElementAtOrDefault(1) as TextPayload)?.Text ?? sender.TextValue;
            stringBuilder.AddText(pName[..1]);
            stringBuilder.AddIcon(CookieHelper.GetMemberRoleIcon(pName.Remove(0, 1)));
            stringBuilder.Add(new PlayerPayload(pName.Remove(0, 1), senderHw));
            sender = stringBuilder.BuiltString;
            return;
        }

        var senderName = sender.TextValue;
        var player = Configuration.Senders.Find(x => $"{x.FirstName} {x.FamilyName}" == senderName);
        if(player == null)
            return;

        stringBuilder.AddIcon(CookieHelper.Menu[player.Genre][player.MarkIndex]);
        stringBuilder.Add(new PlayerPayload(senderName, senderHw));

        sender = stringBuilder.BuiltString;
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
