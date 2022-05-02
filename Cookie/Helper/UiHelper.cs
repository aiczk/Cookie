using System;
using System.Collections.Generic;
using System.Linq;
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

    public static void Icon(FontAwesomeIcon fontAwesomeIcon)
    {
        ImGui.PushFont(UiBuilder.IconFont);
        ImGui.Text(fontAwesomeIcon.ToIconString());
        ImGui.PopFont();
    }
}