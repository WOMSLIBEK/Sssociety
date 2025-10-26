using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    Sprite2D gorgon;

    //default is none of the main presets
    int[] deckComposition = new int[3] { 4, 4, 4 };

//    int[] bitePreset = new int[3] { 6, 4, 2 };
//    int[] hissPreset = new int[3] { 2, 6, 4 };
//    int[] constrictPreset = new int[3] {4,2,6 };
    Deck playerDeck;

    public void InitialisePlayer(GameManager.RockPaperScissors presetChoice, bool AI)
    {
        if (presetChoice == GameManager.RockPaperScissors.Bite)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon3.png");
        }
        if (presetChoice == GameManager.RockPaperScissors.Hiss)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon2.png");

        }
        if (presetChoice == GameManager.RockPaperScissors.Constrict)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon1.png");
        }
        playerDeck = new Deck(presetChoice);
        playerDeck.Name = "Deck";
        AddChild(playerDeck);



    } 
    
    public void StartTurn()
    {
        Position = Vector2.Zero;
        playerDeck.StartTurn();
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
