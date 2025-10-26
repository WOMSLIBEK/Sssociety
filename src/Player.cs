using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    Sprite2D gorgon;

    //default is none of the main presets
    int[] deckComposition = new int[3] { 4, 4, 4 };

    int[] bitePreset = new int[3] { 6, 4, 2 };
    int[] hissPreset = new int[3] { 2, 6, 4 };
    int[] constrictPreset = new int[3] {4,2,6 };
    
    public void InitialisePlayer(GameManager.RockPaperScissors presetChoice,bool AI)
    {
        if (presetChoice == GameManager.RockPaperScissors.Bite)
        {
            deckComposition = bitePreset;
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon3.png");
        }
        if (presetChoice == GameManager.RockPaperScissors.Hiss)
        {
            deckComposition = hissPreset;
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon2.png");

        }
        if (presetChoice == GameManager.RockPaperScissors.Constrict)
        {
            deckComposition = constrictPreset;
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon1.png");
        }

        

    } 

    Deck playerDeck;

    public override void _Ready()
    {
        playerDeck = new Deck(deckComposition[0], deckComposition[1], deckComposition[2]);
        playerDeck.Name = "Deck";
        AddChild(playerDeck);
    }

    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if(GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            gorgon.Position = GetViewport().GetVisibleRect().Size - .5f * gorgon.Texture.GetSize() * gorgon.Scale; ;
        }
    }



    

}
