﻿using System;
using System.Linq;
using System.Numerics;
using Cookie.Helper;
using Cookie.Model;
using Dalamud.Game.Gui.ContextMenus;
using Dalamud.Interface;
using ImGuiNET;

namespace Cookie.UI;

internal class CookieUi
{
    private bool visible, settingsVisible;
    public bool Visible
    {
        get => visible;
        set => visible = value;
    }

    public bool SettingsVisible
    {
        get => settingsVisible;
        set => settingsVisible = value;
    }
    
    private string firstName = "", familyName = "";
    private readonly Configuration configuration;
    
    public CookieUi(Configuration configuration) => this.configuration = configuration;

    public void Draw()
    {
        DrawMainWindow();
        DrawSettingsWindow();
    }

    public void DrawMainWindow()
    {
        if (!Visible)
            return;

        if (!ImGui.Begin("Cookie", ref visible, ImGuiWindowFlags.NoResize))
            return;
        
        ImGui.SetNextItemWidth(255); ImGui.InputText("##FirstName", ref firstName, CookieHelper.NameLength(familyName));
        ImGui.SetNextItemWidth(255); ImGui.SameLine(); ImGui.InputText("##FamilyName", ref familyName, CookieHelper.NameLength(firstName));
        
        ImGui.SameLine(); UiHelper.Button(FontAwesomeIcon.UserPlus, "Add", AddPlayer);
        ImGui.SameLine(); UiHelper.Button(FontAwesomeIcon.CompressArrowsAlt, "Target", SetTargetName);
        ImGui.SameLine(); UiHelper.Button(FontAwesomeIcon.Save, "Save", () => configuration.Save());
        ImGui.SameLine(); UiHelper.Button(FontAwesomeIcon.Toolbox, "Open config window", () => settingsVisible = true);
        ImGui.SameLine(); UiHelper.Button(FontAwesomeIcon.CookieBite, "Emojis", () => ImGui.OpenPopup("##EmojiPopup"));
        ImGui.Separator();

        UiHelper.MenuItem("##EmojiPopup", ImGui.MenuItem, (_, x) => ImGui.SetClipboardText($":{x.Label.ToLower()}:"));
        
        if(!ImGui.BeginTable("##List", 4, ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollY | ImGuiTableFlags.SizingFixedFit))
            return;
        
        for (var row = 0; row < configuration.Senders.Count; row++)
        {
            ImGui.TableNextRow();
            var player = configuration.Senders[row];
            ImGui.TableNextColumn(); ImGui.SetNextItemWidth(255); ImGui.InputText($"##FirstName{row}", ref player.FirstName, CookieHelper.NameLength(player.FamilyName));
            ImGui.TableNextColumn(); ImGui.SetNextItemWidth(255); ImGui.InputText($"##FamilyName{row}", ref player.FamilyName, CookieHelper.NameLength(player.FirstName));
            ImGui.TableNextColumn(); if(ImGui.Button($"{CookieHelper.MenuDict[player.Genre][player.MarkIndex].Label}##Icons{row}", new Vector2(115, 22))) ImGui.OpenPopup($"##IconPopup{row}");
            UiHelper.MenuItem
            (
                $"##IconPopup{row}",
                label => ImGui.MenuItem(label, null, label == CookieHelper.MenuDict[player.Genre][player.MarkIndex].Label),
                (genre, label) =>
                {
                    player.Genre = genre;
                    player.MarkIndex = Array.IndexOf(CookieHelper.MenuDict[genre], label);
                    configuration.Save();
                }
            );
            ImGui.PushFont(UiBuilder.IconFont);
            ImGui.TableNextColumn(); if (ImGui.Button($"{FontAwesomeIcon.TrashAlt.ToIconString()}##Delete{row}")) DeletePlayer(row);
            ImGui.PopFont();
            ImGui.Separator();
        }
        ImGui.EndTable();
        ImGui.End();
    }

    private void AddPlayer()
    {
        if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(familyName))
            return;
            
        if(configuration.Senders.Exists(x => $"{x.FirstName} {x.FamilyName}" == $"{firstName} {familyName}"))
            return;
        
        configuration.Senders.Add(new Sender(firstName, familyName, "Default", 0));
        configuration.Save();
    }

    private void DeletePlayer(int row)
    {
        configuration.Senders.RemoveAt(row);
        configuration.Save();
    }

    private void SetTargetName()
    {
        var (first, family) = CookieHelper.GetTargetName();
        firstName = first ?? firstName;
        familyName = family ?? familyName;
    }

    private void DrawSettingsWindow()
    {
        if (!SettingsVisible)
            return;

        ImGui.SetNextWindowSize(new Vector2(400, 100), ImGuiCond.FirstUseEver);
        if (!ImGui.Begin("Cookie Config Window", ref settingsVisible, ImGuiWindowFlags.AlwaysAutoResize))
            return;
        
        ImGui.Text("If you would like to request a icon name change, please let us know through issues on Github.");
        ImGui.Separator();

        if (ImGui.Checkbox("Show icons for party member roles", ref configuration.ShowPtRoleIcon))
            configuration.Save();

        ImGui.End();
    }
}