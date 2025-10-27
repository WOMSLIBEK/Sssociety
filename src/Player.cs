using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    Sprite2D gorgon;
    [Export]
    public RichTextLabel eggCounter;

    //default is none of the main presets
    int[] deckComposition = new int[3] { 4, 4, 4 };

    Deck playerDeck;


    int numberOfEggs = 0;

    public int NumberOfEggs {
        get
        {
            return numberOfEggs;
        }
        set
        {
            numberOfEggs = value;
            UpdateEggCounter();
        } 
    }

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

    private void UpdateEggCounter()
    {
        eggCounter.Text = "Number of [color=#a4ea88]EGGS[/color]: " + numberOfEggs.ToString();
        
        
    }

    public void StartTurn()
    {

        playerDeck.StartTurn();
    }
    
    public void StartRound()
    {
        playerDeck.ResetHand();
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
