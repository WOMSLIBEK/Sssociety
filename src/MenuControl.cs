using Godot;
using System;

public partial class MenuControl : Control
{
    [Export]
    VBoxContainer main;
    [Export]
    VBoxContainer settings;
    [Export]
    VBoxContainer gameSetup;
    [Export]
    RichTextLabel totalPlayersLabel;
    [Export]
    HScrollBar totalPlayers;
    [Export]
    RichTextLabel numberOfAIsLabel;
    [Export]
    HScrollBar numberOfAIs;


    public void PlayPressed()
    {
        main.Visible = false;
        settings.Visible = false;
        gameSetup.Visible = true;

    }

    public void SettingsPressed()
    {
        main.Visible = false;
        settings.Visible = true;
        gameSetup.Visible = false;
    }

    public void Back()
    {
        main.Visible = true;
        settings.Visible = false;
        gameSetup.Visible = false;
    }

    public void ExitPressed()
    {
        GetTree().Quit();
    }

    public void PlayerAmountChanged(float value)
    {
        totalPlayersLabel.Text = "[center] Total Players: " + value.ToString() + "[/center]";

        numberOfAIs.MaxValue = value - 1.0f;

        numberOfAIsLabel.Text = "[center] Number of AIs: " + numberOfAIs.Value.ToString() + "[/center]";


    }

    public void NumberOfAIsChanged(float value)
    {
        numberOfAIsLabel.Text = "[center] Number of AIs: " + value + "[/center]";
    }

    public void StartGame()
    {
        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }
}

