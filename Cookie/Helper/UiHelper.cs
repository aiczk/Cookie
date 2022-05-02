using System;
using System.Collections.Generic;
using System.Linq;
using Cookie.Model;
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
    
    public static void MenuItem(string menuName, Func<string, bool> imGuiFunc, Action<string, MenuItem> action)
    {
        if (!ImGui.BeginPopup(menuName))
            return;
        
        foreach (var genre in CookieHelper.MenuDict.Keys.Where(ImGui.BeginMenu))
        {
            foreach (var label in CookieHelper.MenuDict[genre])
            {
                if (!imGuiFunc(label.Label))
                    continue;

                action(genre, label);
            }

            ImGui.EndMenu();
        }
        ImGui.EndPopup();
    }
}