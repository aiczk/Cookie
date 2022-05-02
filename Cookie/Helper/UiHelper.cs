using System;
using System.Collections.Generic;
using System.Linq;
using Cookie.Model;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Interface;
using ImGuiNET;

namespace Cookie.Helper;

public static class UiHelper
{
    public static void Button(FontAwesomeIcon fontAwesomeIcon, string tooltip, Action action)
    {
        ImGui.PushFont(UiBuilder.IconFont);
        if (ImGui.Button(fontAwesomeIcon.ToIconString())) action();
        ImGui.PopFont();
        
        if(!ImGui.IsItemHovered())
            return;
        
        ImGui.SetTooltip(tooltip);
    }
    
    public static void MenuItem(string menuName, Func<string, bool> imGuiFunc, Action<string, BitmapFontIcon> action)
    {
        if (!ImGui.BeginPopup(menuName))
            return;
        
        foreach (var genre in CookieHelper.Menu.Keys.Where(ImGui.BeginMenu))
        {
            foreach (var label in CookieHelper.Menu[genre])
            {
                if (!imGuiFunc(label.ToString()))
                    continue;

                action(genre, label);
            }

            ImGui.EndMenu();
        }
        ImGui.EndPopup();
    }
}