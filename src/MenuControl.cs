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

    [Export]
    HBoxContainer characterSelectors;
    [Export]
    HBoxContainer AICharacterSelectors;


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

        UpdateNumberOfCharacterSelectors();
        UpdateNumberOfAICharacterSelectors();

        numberOfAIs.MaxValue = value - 1.0f;

        numberOfAIsLabel.Text = "[center] Number of AIs: " + numberOfAIs.Value.ToString() + "[/center]";


    }



    public void NumberOfAIsChanged(float value)
    {
        numberOfAIsLabel.Text = "[center] Number of AIs: " + value + "[/center]";

        UpdateNumberOfCharacterSelectors();
        UpdateNumberOfAICharacterSelectors();

       
    }

    public void StartGame()
    {
        //initialises the list of tuples as the length of the total player count
        GameManager.gameManager.players = new (GameManager.RockPaperScissors playerType, bool isAI)[(int)totalPlayers.Value];

        int i = 0;
        //no need for checks as to length if done correctly there is no way for these values to be innacurate
        foreach (OptionButton child in characterSelectors.GetChildren())
        {
            GameManager.gameManager.players[i] = ((GameManager.RockPaperScissors)child.Selected, false);
            i += 1;
        }

        foreach (OptionButton child in AICharacterSelectors.GetChildren())
        {
            GameManager.gameManager.players[i] = ((GameManager.RockPaperScissors)child.Selected, true);
            i += 1;
        }

        GD.Print(GameManager.gameManager.players[0].ToString(),GameManager.gameManager.players[1].ToString());


        GetTree().ChangeSceneToFile("res://scenes/main.tscn");
    }





    private void UpdateNumberOfCharacterSelectors()
    {
        //a positive value means a new selector should be added and a negative value means it should be removed
        int numberOfSelectors = (int)totalPlayers.Value - (int)numberOfAIs.Value;
        PackedScene packedSelector = GD.Load<PackedScene>("res://scene_objects/CharacterSelector.tscn");

        foreach (Node child in characterSelectors.GetChildren())
        {
            characterSelectors.RemoveChild(child);
        }

        for (int i = 0; i < numberOfSelectors; i++)
        {
            Node characterSelector = packedSelector.Instantiate();
            characterSelectors.AddChild(characterSelector);
        }


    }
    
    private void UpdateNumberOfAICharacterSelectors()
    {
        int numberOfSelectors = (int)numberOfAIs.Value;

        PackedScene packedSelector = GD.Load<PackedScene>("res://scene_objects/CharacterSelector.tscn");

        foreach (Node child in AICharacterSelectors.GetChildren())
        {
            AICharacterSelectors.RemoveChild(child);
        }

        for(int i = 0;i<numberOfSelectors;i++){
            Node characterSelector = packedSelector.Instantiate();
            AICharacterSelectors.AddChild(characterSelector);
        }

    }
}

